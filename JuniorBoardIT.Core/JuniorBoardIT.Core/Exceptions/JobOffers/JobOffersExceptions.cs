namespace JuniorBoardIT.Core.Exceptions.JobOffers
{
    public class JobOffersExceptions : Exception
    {
        public JobOffersExceptions(string message) : base(message)
        {
        }
    }
    public class JobOfferNotFoundExceptions : JobOffersExceptions
    {
        public JobOfferNotFoundExceptions(string message) : base(message)
        {
        }
    }
}
