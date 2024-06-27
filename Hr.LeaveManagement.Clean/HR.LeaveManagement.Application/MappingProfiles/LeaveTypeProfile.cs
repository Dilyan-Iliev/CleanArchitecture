using AutoMapper;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveTypeProfile : Profile
    {
        public LeaveTypeProfile()
        {
            //CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
            //from LeaveTypeDto to LeaveType and from LeaveType to LeaveTypeDto with this .ReverseMap()
        }
    }
}
