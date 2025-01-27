﻿using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public class GetLeaveTypeDetailsQueryRequesthandler
        : IRequestHandler<GetLeaveTypeDetailsQueryRequest, LeaveTypeDetailsDto>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public GetLeaveTypeDetailsQueryRequesthandler(IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQueryRequest request,
            CancellationToken cancellationToken)
        {
            //Query DB
            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.Id); //from QueryRequest

            if (leaveType == null)
            {
                throw new NotFoundException(nameof(LeaveType), request.Id);
            }

            //Map
            var data = _mapper.Map<LeaveTypeDetailsDto>(leaveType);

            //Return dto
            return data;
        }
    }
}
