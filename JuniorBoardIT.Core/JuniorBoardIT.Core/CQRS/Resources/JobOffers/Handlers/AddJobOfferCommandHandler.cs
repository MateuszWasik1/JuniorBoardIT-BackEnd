using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Commands;
using JuniorBoardIT.Core.Services;

namespace JuniorBoardIT.Core.CQRS.Resources.JobOffers.Handlers
{
    public class AddJobOfferCommandHandler : ICommandHandler<AddJobOfferCommand>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        public AddJobOfferCommandHandler(IDataBaseContext context, IUserContext user)
        {
            this.context = context;
            this.user = user;
        }
        public void Handle(AddJobOfferCommand command)
        {
            var model = new Entities.JobOffers()
            {
                JOGID = Guid.NewGuid(),
                JORGID = Guid.Parse(user.UGID), 
                JOCGID = command.Model.JOCGID, 
                JOTitle = command.Model.JOTitle,
                JOCompanyName = command.Model.JOCompanyName,
                JOLocationType = command.Model.JOLocationType,
                JOOfficeLocation = command.Model.JOOfficeLocation,
                JOEmploymentType = command.Model.JOEmploymentType,
                JOExpirenceLevel = command.Model.JOExpirenceLevel,
                JOExpirenceYears = command.Model.JOExpirenceYears,
                JOCategory = command.Model.JOCategory,
                JOSalaryMin = command.Model.JOSalaryMin,
                JOSalaryMax = command.Model.JOSalaryMax,
                JOCurrency = command.Model.JOCurrency,
                JOSalaryType = command.Model.JOSalaryType,
                JODescription = command.Model.JODescription,
                JORequirements = command.Model.JORequirements,
                JOBenefits = command.Model.JOBenefits,
                JOCreatedAt = DateTime.Now,
                JOPostedAt = command.Model.JOPostedAt,
                JOExpiresAt = command.Model.JOExpiresAt,
                JOStatus = command.Model.JOStatus,
            };

            context.CreateOrUpdate(model);
            context.SaveChanges();
        }
    }
}
