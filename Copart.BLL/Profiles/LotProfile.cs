using AutoMapper;
using Copart.BLL.Models.LotModels;
using Copart.Domain.Entities;

namespace Copart.BLL.Profiles
{
    internal class LotProfile : Profile
    {
        public LotProfile()
        {
            CreateMap<LotAddModel, Lot>();
            CreateMap<LotUpdateModel, Lot>();
            CreateMap<LotModel, Lot>();
            CreateMap<Lot, LotModel>();
        }
    }
}
