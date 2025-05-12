using AutoMapper;
using Copart.BLL.Models.BidModels;
using Copart.Domain.Entities;

namespace Copart.BLL.Profiles
{
    public class BidProfile : Profile
    {
        public BidProfile()
        {
            CreateMap<Bid, BidModel>();
            CreateMap<BidModel, Bid>();
            CreateMap<BidAddModel, Bid>();
        }
    }
}
