namespace JuniorBoardIT.Core.Exceptions.Applications
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