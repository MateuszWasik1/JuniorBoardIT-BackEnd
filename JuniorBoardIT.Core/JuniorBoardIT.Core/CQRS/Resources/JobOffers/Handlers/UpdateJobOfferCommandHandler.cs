using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Commands;
using JuniorBoardIT.Core.Exceptions.JobOffers;

namespace JuniorBoardIT.Core.CQRS.Resources.JobOffers.Handlers
{
    public class UpdateJobOfferCommandHandler : ICommandHandler<UpdateJobOfferCommand>
    {
        private readonly IDataBaseContext context;
        public UpdateJobOfferCommandHandler(IDataBaseContext context) => this.context = context;
        
        public void Handle(UpdateJobOfferCommand command)
        {
            var jobOffer = context.JobOffers.FirstOrDefault(x => x.JOGID == command.Model.JOGID);

            if (jobOffer == null)
                throw new JobOfferNotFoundExceptions("Nie znaleziono oferty pracy!");

            jobOffer.JOTitle = command.Model.JOTitle;
            jobOffer.JOCompanyName = command.Model.JOCompanyName;
            jobOffer.JOLocationType = command.Model.JOLocationType;
            jobOffer.JOOfficeLocation = command.Model.JOOfficeLocation;
            jobOffer.JOEmploymentType = command.Model.JOEmploymentType;
            jobOffer.JOExpirenceLevel = command.Model.JOExpirenceLevel;
            jobOffer.JOExpirenceYears = command.Model.JOExpirenceYears;
            jobOffer.JOCategory = command.Model.JOCategory;
            jobOffer.JOSalaryMin = command.Model.JOSalaryMin;
            jobOffer.JOSalaryMax = command.Model.JOSalaryMax;
            jobOffer.JOCurrency = command.Model.JOCurrency;
            jobOffer.JOSalaryType = command.Model.JOSalaryType;
            jobOffer.JODescription = command.Model.JODescription;
            jobOffer.JORequirements = command.Model.JORequirements;
            jobOffer.JOBenefits = command.Model.JOBenefits;
            jobOffer.JOPostedAt = command.Model.JOPostedAt;
            jobOffer.JOExpiresAt = command.Model.JOExpiresAt;
            jobOffer.JOStatus = command.Model.JOStatus;

            context.CreateOrUpdate(jobOffer);
            context.SaveChanges();
        }
    }
}
