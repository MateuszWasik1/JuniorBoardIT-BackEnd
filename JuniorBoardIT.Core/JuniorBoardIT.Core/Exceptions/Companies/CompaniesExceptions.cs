using JuniorBoardIT.Core.Exceptions.Users;

namespace JuniorBoardIT.Core.Exceptions.Companies
{
    public class CompaniesExceptions : Exception
    {
        public CompaniesExceptions(string message) : base(message)
        {
        }
    }
    public class CompanyNotFoundExceptions : CompaniesExceptions
    {
        public CompanyNotFoundExceptions(string message) : base(message)
        {
        }
    }

    public class CompanyNameMin0Exceptions : CompaniesExceptions
    {
        public CompanyNameMin0Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyNameMax255Exceptions : CompaniesExceptions
    {
        public CompanyNameMax255Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyDescriptionMin0Exceptions : CompaniesExceptions
    {
        public CompanyDescriptionMin0Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyDescriptionMax2000Exceptions : CompaniesExceptions
    {
        public CompanyDescriptionMax2000Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyEmailMin0Exceptions : CompaniesExceptions
    {
        public CompanyEmailMin0Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyEmailMax255Exceptions : CompaniesExceptions
    {
        public CompanyEmailMax255Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyAddressMin0Exceptions : CompaniesExceptions
    {
        public CompanyAddressMin0Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyAddressMax255Exceptions : CompaniesExceptions
    {
        public CompanyAddressMax255Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyCityMin0Exceptions : CompaniesExceptions
    {
        public CompanyCityMin0Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyCityMax255Exceptions : CompaniesExceptions
    {
        public CompanyCityMax255Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyCountryMin0Exceptions : CompaniesExceptions
    {
        public CompanyCountryMin0Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyCountryMax255Exceptions : CompaniesExceptions
    {
        public CompanyCountryMax255Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyPostalCodeMin0Exceptions : CompaniesExceptions
    {
        public CompanyPostalCodeMin0Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyPostalCodeMax255Exceptions : CompaniesExceptions
    {
        public CompanyPostalCodeMax255Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyPhoneNumberMin0Exceptions : CompaniesExceptions
    {
        public CompanyPhoneNumberMin0Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyPhoneNumberMax255Exceptions : CompaniesExceptions
    {
        public CompanyPhoneNumberMax255Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyNIPMin0Exceptions : CompaniesExceptions
    {
        public CompanyNIPMin0Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyNIPMax255Exceptions : CompaniesExceptions
    {
        public CompanyNIPMax255Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyRegonMin0Exceptions : CompaniesExceptions
    {
        public CompanyRegonMin0Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyRegonMax255Exceptions : CompaniesExceptions
    {
        public CompanyRegonMax255Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyKRSMin0Exceptions : CompaniesExceptions
    {
        public CompanyKRSMin0Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyKRSMax255Exceptions : CompaniesExceptions
    {
        public CompanyKRSMax255Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyLinkedInMin0Exceptions : CompaniesExceptions
    {
        public CompanyLinkedInMin0Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyLinkedInMax255Exceptions : CompaniesExceptions
    {
        public CompanyLinkedInMax255Exceptions(string message) : base(message)
        {
        }
    }

    public class CompanyFoundedYearMinExceptions : CompaniesExceptions
    {
        public CompanyFoundedYearMinExceptions(string message) : base(message)
        {
        }
    }

    public class CompanyFoundedYearMaxExceptions : CompaniesExceptions
    {
        public CompanyFoundedYearMaxExceptions(string message) : base(message)
        {
        }
    }
}