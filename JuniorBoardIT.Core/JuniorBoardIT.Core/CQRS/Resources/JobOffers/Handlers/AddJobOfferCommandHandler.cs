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
            //if (command.Model.UUserName.Length == 0)
            //    throw new UserNameRequiredException("Nazwa użytkownika nie może być pusta!");

            //if (command.Model.UUserName.Length > 100)
            //    throw new UserNameMax100Exception("Nazwa użytkownika nie może mieć więcej niż 100 znaków!");

            //if (command.Model.UFirstName.Length > 50)
            //    throw new UserFirstNameMax50Exception("Imię użytkownika nie może mieć więcej niż 50 znaków!");

            //if (command.Model.ULastName.Length > 50)
            //    throw new UserLastNameMax50Exception("Nazwisko użytkownika nie może mieć więcej niż 50 znaków!");

            //if (command.Model.UEmail.Length == 0)
            //    throw new UserEmailRequiredException("Email użytkownika nie może być pusty!");

            //if (command.Model.UEmail.Length > 100)
            //    throw new UserEmailMax100Exception("Email użytkownika nie może mieć więcej niż 100 znaków!");

            //if (command.Model.UPhone.Length > 100)
            //    throw new UserPhoneMax100Exception("Telefon użytkownika nie może mieć więcej niż 100 znaków!");

            var model = new Entities.JobOffers()
            {
                JOGID = Guid.NewGuid(),
                JORGID = Guid.Parse(user.UGID), 
                JOTitle = command.Model.JOTitle,
                JOCompanyName = command.Model.JOTitle,
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
