namespace JuniorBoardIT.Core.Exceptions.Accounts
{
    public class ApplicationsExceptions : Exception
    {
        public ApplicationsExceptions(string message) : base(message)
        {
        }
    }

    public class ApplicationNotFoundExceptions : ApplicationsExceptions
    {
        public ApplicationNotFoundExceptions(string message) : base(message)
        {
        }
    }
}