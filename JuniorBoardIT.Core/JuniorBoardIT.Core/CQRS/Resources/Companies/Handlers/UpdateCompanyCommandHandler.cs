using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Commands;
using JuniorBoardIT.Core.Exceptions.Companies;
using JuniorBoardIT.Core.Exceptions.Users;

namespace JuniorBoardIT.Core.CQRS.Resources.Companies.Handlers
{
    public class UpdateCompanyCommandHandler : ICommandHandler<UpdateCompanyCommand>
    {
        private readonly IDataBaseContext context;
        public UpdateCompanyCommandHandler(IDataBaseContext context) => this.context = context;
        
        public void Handle(UpdateCompanyCommand command)
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
                throw new CompanyFoundedYearMinExceptions("Rok założenia firmy nie może być większy niż" + (DateTime.Now.Year + 1) + "!");

            var company = context.Companies.FirstOrDefault(x => x.CGID == command.Model.CGID);

            if (company == null)
                throw new CompanyNotFoundExceptions("Nie znaleziono firmy!");

            company.CName = command.Model.CName;
            company.CIndustry = command.Model.CIndustry;
            company.CDescription = command.Model.CDescription;
            company.CEmail = command.Model.CEmail;
            company.CAddress = command.Model.CAddress;
            company.CCity = command.Model.CCity;
            company.CCountry = command.Model.CCountry;
            company.CPostalCode = command.Model.CPostalCode;
            company.CPhoneNumber = command.Model.CPhoneNumber;
            company.CNIP = command.Model.CNIP;
            company.CRegon = command.Model.CRegon;
            company.CKRS = command.Model.CKRS;
            company.CLI = command.Model.CLI;
            company.CFoundedYear = command.Model.CFoundedYear;
            company.CEmployeesNo = command.Model.CEmployeesNo;
            company.CUpdatedAt = DateTime.Now;

            context.CreateOrUpdate(company);
            context.SaveChanges();
        }
    }
}
