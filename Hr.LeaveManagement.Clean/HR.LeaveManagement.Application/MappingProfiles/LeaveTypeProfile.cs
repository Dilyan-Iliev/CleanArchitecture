using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveTypeProfile : Profile
    {
        public LeaveTypeProfile()
        {
            //from LeaveTypeDto to LeaveType and from LeaveType to LeaveTypeDto with this .ReverseMap()
            CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();

            CreateMap<LeaveType, LeaveTypeDetailsDto>(); //from LeaveType to the DTO

            CreateMap<CreateLeaveTypeCommandRequest, LeaveType>();

            CreateMap<UpdateLeaveTypeCommandRequest, LeaveType>();
        }
    }
}
