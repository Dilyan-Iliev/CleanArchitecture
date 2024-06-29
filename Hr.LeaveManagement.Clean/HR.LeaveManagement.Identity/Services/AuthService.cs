using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR.LeaveManagement.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userMan;
        private readonly SignInManager<ApplicationUser> _signInMan;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userMan,
            SignInManager<ApplicationUser> signInMan,
            JwtSettings jwtSettings)
        {
            _userMan = userMan;
            _signInMan = signInMan;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userMan.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new NotFoundException($"User with {request.Email} not found", request.Email);
            }

            var result = await _signInMan.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
            {
                throw new BadRequestException($"Credentials for {request.Email} arent valid");
            }

            string jwtSecurityToken = await GenerateToken(user);
            return new AuthResponse
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Token = jwtSecurityToken
            };
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var user = new ApplicationUser
            {
                Email = request.EmailAddress,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true
            };

            var result = await _userMan.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var sb = new StringBuilder();
                foreach (var err in result.Errors)
                {
                    sb.AppendFormat("-{0}\n", err.Description);
                }
                throw new BadRequestException($"{sb}");
            }

            await _userMan.AddToRoleAsync(user, "Employee");
            return new RegistrationResponse { UserId = user.Id };
        }

        private async Task<string> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userMan.GetClaimsAsync(user);
            var roles = await _userMan.GetRolesAsync(user);

            var roleClaims = roles
                .Select(r => new Claim(ClaimTypes.Role, r))
                .ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signinCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationMinutes),
                signingCredentials: signinCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
