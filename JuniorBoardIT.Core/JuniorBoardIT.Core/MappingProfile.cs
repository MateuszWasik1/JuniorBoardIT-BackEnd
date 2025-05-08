using AutoMapper;
using JuniorBoardIT.Core.Entities;
using JuniorBoardIT.Core.Models.ViewModels.AuthorsViewModels;
using JuniorBoardIT.Core.Models.ViewModels.BooksViewModels;
using JuniorBoardIT.Core.Models.ViewModels.PublishersViewModels;
using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;
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
            CreateMap<Books, BookViewModel>();
            CreateMap<Books, BooksViewModel>();
            CreateMap<Books, BooksListViewModel>();
            CreateMap<Authors, AuthorViewModel>();
            CreateMap<Authors, AuthorsViewModel>();
            CreateMap<Authors, AuthorsListViewModel>();
            CreateMap<Publishers, PublisherViewModel>();
            CreateMap<Publishers, PublishersViewModel>();
            CreateMap<Publishers, PublishersListViewModel>();
            CreateMap<Reports, ReportViewModel>();
            CreateMap<Reports, ReportsViewModel>();
            CreateMap<Reports, ReportsListViewModel>();
        }
    }
}