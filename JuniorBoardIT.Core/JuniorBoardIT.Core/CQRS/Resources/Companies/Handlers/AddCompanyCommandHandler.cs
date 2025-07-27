using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Commands;
using JuniorBoardIT.Core.Exceptions.Companies;

namespace JuniorBoardIT.Core.CQRS.Resources.Companies.Handlers
{
    public class AddCompanyCommandHandler : ICommandHandler<AddCompanyCommand>
    {
        private readonly IDataBaseContext context;
        public AddCompanyCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(AddCompanyCommand command)
        {
            if (command.Model.CName.Length == 0)
                throw new CompanyNameMin0Exceptions("Nazwa firmy nie może być pusta!");

            if (command.Model.CName.Length > 255)
                throw new CompanyNameMax255Exceptions("Nazwa firmy nie może mieć więcej niż 255 znaków!");

            if (command.Model.CDescription.Length == 0)
                throw new CompanyDescriptionMin0Exceptions("Opis firmy nie może mieć być pusty!");

            if (command.Model.CDescription.Length > 2000)
                throw new CompanyDescriptionMax2000Exceptions("Opis firmy nie może mieć więcej niż 2000 znaków!");

            if (command.Model.CEmail.Length == 0)
                throw new CompanyEmailMin0Exceptions("Email firmy nie może być pusty!");

            if (command.Model.CEmail.Length > 255)
                throw new CompanyEmailMax255Exceptions("Email firmy nie może mieć więcej niż 255 znaków!");

            if (command.Model.CAddress.Length == 0)
                throw new CompanyAddressMin0Exceptions("Adres firmy nie może być pusty!");

            if (command.Model.CAddress.Length > 255)
                throw new CompanyAddressMax255Exceptions("Adres firmy nie może mieć więcej niż 255 znaków!");

            if (command.Model.CCity.Length == 0)
                throw new CompanyCityMin0Exceptions("Miasto firmy nie może być puste!");

            if (command.Model.CCity.Length > 255)
                throw new CompanyCityMax255Exceptions("Miasto firmy nie może mieć więcej niż 255 znaków!");

            if (command.Model.CCountry.Length == 0)
                throw new CompanyCountryMin0Exceptions("Kraj firmy nie może być pusty!");

            if (command.Model.CCountry.Length > 255)
                throw new CompanyCountryMax255Exceptions("Kraj firmy nie może mieć więcej niż 255 znaków!");

            if (command.Model.CPostalCode.Length == 0)
                throw new CompanyPostalCodeMin0Exceptions("Kod pocztowy firmy nie może być pusty!");

            if (command.Model.CPostalCode.Length > 255)
                throw new CompanyPostalCodeMax255Exceptions("Kod pocztowy firmy nie może mieć więcej niż 255 znaków!");

            if (command.Model.CPhoneNumber.Length == 0)
                throw new CompanyPhoneNumberMin0Exceptions("Numer telefonowy firmy nie może być pusta!");

            if (command.Model.CPhoneNumber.Length > 255)
                throw new CompanyPhoneNumberMax255Exceptions("Numer telefonowy firmy nie może mieć więcej niż 255 znaków!");

            if (command.Model.CNIP.Length == 0)
                throw new CompanyNIPMin0Exceptions("NIP firmy nie może być pusty!");

            if (command.Model.CNIP.Length > 255)
                throw new CompanyNIPMax255Exceptions("NIP firmy nie może mieć więcej niż 255 znaków!");

            if (command.Model.CRegon.Length == 0)
                throw new CompanyRegonMin0Exceptions("REGON firmy nie może być pusty!");

            if (command.Model.CRegon.Length > 255)
                throw new CompanyRegonMax255Exceptions("REGON firmy nie może mieć więcej niż 255 znaków!");

            if (command.Model.CKRS.Length == 0)
                throw new CompanyKRSMin0Exceptions("KRS firmy nie może być pusty!");

            if (command.Model.CKRS.Length > 255)
                throw new CompanyKRSMax255Exceptions("KRS firmy nie może mieć więcej niż 255 znaków!");

            if (command.Model.CLI.Length == 0)
                throw new CompanyLinkedInMin0Exceptions("LinkedIn firmy nie może być pusty!");

            if (command.Model.CLI.Length > 255)
                throw new CompanyLinkedInMax255Exceptions("LinkedIn firmy nie może mieć więcej niż 255 znaków!");

            if (command.Model.CFoundedYear < 1000)
                throw new CompanyFoundedYearMinExceptions("Rok założenia firmy nie może być mniejszy niż 1000 rok!");

            if (command.Model.CFoundedYear > DateTime.Now.Year + 1)
                throw new CompanyFoundedYearMinExceptions("Rok założenia firmy nie może być większy niż " + (DateTime.Now.Year + 1) + "!");

            var model = new Entities.Companies()
            {
                CGID = Guid.NewGuid(),
                CName = command.Model.CName,
                CIndustry = command.Model.CIndustry,
                CDescription = command.Model.CDescription,
                CEmail = command.Model.CEmail,
                CAddress = command.Model.CAddress,
                CCity = command.Model.CCity,
                CCountry = command.Model.CCountry,
                CPostalCode = command.Model.CPostalCode,
                CPhoneNumber = command.Model.CPhoneNumber,
                CNIP = command.Model.CNIP,
                CRegon = command.Model.CRegon,
                CKRS = command.Model.CKRS,
                CLI = command.Model.CLI,
                CFoundedYear = command.Model.CFoundedYear,
                CEmployeesNo = command.Model.CEmployeesNo,
                CCreatedAt = DateTime.Now,
                CUpdatedAt = DateTime.Now,
            };

            context.CreateOrUpdate(model);
            context.SaveChanges();
        }
    }
}
