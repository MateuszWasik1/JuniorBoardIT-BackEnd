using JuniorBoardIT.Core.Models.Enums.JobOffers;

namespace JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels
{
    public class JobOfferViewModel
    {
        public int JOID { get; set; }
        public Guid JOGID { get; set; }
        public Guid JORGID { get; set; }
        public string? JOTitle { get; set; }
        public string? JOCompanyName { get; set; }
        public LocationEnum JOLocationType { get; set; }
        public string? JOOfficeLocation { get; set; }
        public EmploymentTypeEnum JOEmploymentType { get; set; }
        public ExpirenceEnum JOExpirenceLevel { get; set; }
        public double JOExpirenceYears { get; set; }
        public CategoryEnum JOCategory { get; set; }
        public double JOsalaryMin { get; set; }
        public double JOSalaryMax { get; set; }
        public string? JOCurrency { get; set; }
        public SalaryEnum JOSalaryType { get; set; }
        public string? JODescription { get; set; }
        public string? JORequirements { get; set; }
        public string? JOBenefits { get; set; }
        public DateTime? JOCreatedAt { get; set; }
        public DateTime? JOPostedAt { get; set; }
        public DateTime? JOExpiresAt { get; set; }
        public StatusEnum JOStatus { get; set; }
    }
}
