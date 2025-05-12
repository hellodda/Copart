using AutoMapper;
using Copart.BLL.Models.UserModels;
using Copart.Domain.Entities;

namespace Copart.BLL.Profiles
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserAddModel, User>();
            CreateMap<UserModel, User>();
        }
    }
}
