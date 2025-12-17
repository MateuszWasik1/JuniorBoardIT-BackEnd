using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.FavoriteJobOffers.Commands;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Handlers;
using JuniorBoardIT.Core.Exceptions.JobOffers;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace JuniorBoardIT.UnitTests.CQRS.CommandHandlers.Applications
{
    [TestFixture]
    public class TestDeleteFavoriteJobOfferCommandHandler
    {
        private Mock<IDataBaseContext> context;

        private List<Core.Entities.FavoriteJobOffers> favoriteJobOffers;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();

            favoriteJobOffers = new List<Core.Entities.FavoriteJobOffers>()
            {
                new Core.Entities.FavoriteJobOffers()
                {
                    FJOID = 1,
                    FJOGID = new Guid("32dd879c-ee2f-11db-8314-0800200c9a66")
                },
                new Core.Entities.FavoriteJobOffers()
                {
                    FJOID = 2,
                    FJOGID = new Guid("33dd879c-ee2f-11db-8314-0800200c9a66")
                },
            };

            context.Setup(x => x.FavoriteJobOffers).Returns(favoriteJobOffers.AsQueryable());

            context.Setup(x => x.DeleteFavoriteJobOffer(It.IsAny<Core.Entities.FavoriteJobOffers>())).Callback<Core.Entities.FavoriteJobOffers>(favoriteJobOffer => favoriteJobOffers.Remove(favoriteJobOffer));
        }

        [Test]
        public void TestDeleteFavoriteJobOfferCommandHandler_FavoriteJobOfferIsNotFound_ShouldThrow_JobOfferNotFoundExceptions()
        {
            //Arrange 
            var command = new DeleteFavoriteJobOfferCommand() { FJOGID = new Guid("31dd879c-ee2f-11db-8314-0800200c9a66") };
            var handler = new DeleteFavoriteJobOfferCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<JobOfferNotFoundExceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestDeleteFavoriteJobOfferCommandHandler_FavoriteJobOfferIsFound_ShouldDelete_GivenFavoriteJobOffer()
        {
            //Arrange 
            var command = new DeleteFavoriteJobOfferCommand() { FJOGID = new Guid("33dd879c-ee2f-11db-8314-0800200c9a66") };
            var handler = new DeleteFavoriteJobOfferCommandHandler(context.Object);

            //Act
            handler.Handle(command);

            //Assert
            ClassicAssert.AreEqual(1, favoriteJobOffers.Count);
            ClassicAssert.AreEqual(1, favoriteJobOffers[0].FJOID);
            ClassicAssert.AreEqual(new Guid("32dd879c-ee2f-11db-8314-0800200c9a66"), favoriteJobOffers[0].FJOGID);
        }
    }
}
