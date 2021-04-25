using AutoMapper;
using RepairCompanyManagement.BusinessLogic.Dtos;
using RepairCompanyManagement.WebUI.Models;

namespace RepairCompanyManagement.WebUI.Mapper
{
    public class WebUIMapperProfile : Profile
    {
        public WebUIMapperProfile()
        {
            CreateMap<SpecializationDto, SpecializationViewModel>().ReverseMap();
            CreateMap<JobPositionDto, JobPositionViewModel>().ReverseMap();
            CreateMap<BrigadeDto, BrigadeViewModel>().ReverseMap();
        }
    }
}