using AutoMapper;
using JuniorBoardIT.Core.Entities;
using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;
using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;
using JuniorBoardIT.Core.Models.ViewModels.UserViewModels;
using JuniorBoardIT.Core.Models.ViewModels.BugsViewModels;
using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;
using JuniorBoardIT.Core.Models.ViewModels.FavoriteJobOffersViewModel;

namespace JuniorBoardIT.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<User, UsersAdminViewModel>();
            CreateMap<User, UserAdminViewModel>();
            CreateMap<JobOffers, GetAllJobOffersViewModel>();
            CreateMap<JobOffers, JobOfferViewModel>();
            CreateMap<Reports, ChangeReportStatusViewModel>();
            CreateMap<Reports, GetReportViewModel>();
            CreateMap<Reports, GetReportsViewModel>();
            CreateMap<Reports, ReportsViewModel>();
            CreateMap<Reports, ReportViewModel>();
            CreateMap<Bugs, BugsViewModel>();
            CreateMap<Bugs, BugViewModel>();
            CreateMap<BugsNotes, BugsNotesViewModel>();
            CreateMap<Companies, CompanyViewModel>();
            CreateMap<Companies, GetCompaniesViewModel>();
            CreateMap<Companies, GetCompaniesForUserViewModel>();
            CreateMap<Companies, GetCompanyForUserViewModel>();
            CreateMap<FavoriteJobOffers, FavoriteJobOfferViewModel>();
            CreateMap<FavoriteJobOffers, FavoriteJobOffersViewModel>();
        }
    }
}