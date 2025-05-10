using AutoMapper;
using Copart.BLL.Models.VehicleModels;
using Copart.Domain.Entities;

namespace Copart.BLL.Profiles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<Vehicle, VehicleModel>();
            CreateMap<Vehicle, VehicleAddModel>();
            CreateMap<VehicleAddModel, Vehicle>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Vin, opt => opt.MapFrom(src => src.Vin))
                .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Make))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Damage, opt => opt.MapFrom(src => src.Damage))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));
            CreateMap<VehicleModel, Vehicle>();
        }
    }
}
