using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;

namespace JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels
{
    public class GetJobOfferViewModel
    {
        public JobOfferViewModel? JobOffer { get; set; }
        public CompanyViewModel? Company { get; set; }
    }
}
