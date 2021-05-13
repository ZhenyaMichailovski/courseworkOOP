﻿using AutoMapper;
using RepairCompanyManagement.BusinessLogic.Dtos;
using RepairCompanyManagement.DataAccess.Entities;

namespace RepairCompanyManagement.BusinessLogic.Mapper
{
    public class BusinessLogicMapperProfile : Profile
    {
        public BusinessLogicMapperProfile()
        {
            // here we should have all the rules for mapping
            // between DTOs and simple entities

            CreateMap<Specialization, SpecializationDto>().ReverseMap();

            CreateMap<JobPosition, JobPositionDto>().ReverseMap();

            CreateMap<Brigade, BrigadeDto>().ReverseMap();

            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<Employee, EmployeeDto>().ReverseMap();

            CreateMap<Manager, ManagerDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();

            CreateMap<OrderTask, OrderTaskDto>().ReverseMap();

            CreateMap<Task, TaskDto>().ReverseMap();

            CreateMap<Feedback, FeedbackDto>().ReverseMap();
        }
    }
}