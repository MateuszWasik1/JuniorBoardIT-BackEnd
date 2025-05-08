using AutoMapper;
using JuniorBoardIT.Core.Entities;
using JuniorBoardIT.Core.Models.ViewModels.UserViewModels;

namespace JuniorBoardIT.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<User, UsersAdminViewModel>();
            CreateMap<User, UserAdminViewModel>();
        }
    }
}