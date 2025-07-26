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
}
