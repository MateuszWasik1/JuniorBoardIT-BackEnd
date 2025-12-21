using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.FavoriteJobOffers.Commands;
using JuniorBoardIT.Core.CQRS.Resources.FavoriteJobOffers.Handlers;
using JuniorBoardIT.Core.Models.ViewModels.FavoriteJobOffersViewModel;
using JuniorBoardIT.Core.Services;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace JuniorBoardIT.UnitTests.CQRS.CommandHandlers.FavoriteJobOffers
{
    [TestFixture]
    public class TestAddFavoriteJobOfferCommandHandler
    {
        private Mock<IDataBaseContext> context;
        private Mock<IUserContext>? user;

        private List<Core.Entities.FavoriteJobOffers> favoriteJobOffers;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();
            user = new Mock<IUserContext>();

            favoriteJobOffers = new List<Core.Entities.FavoriteJobOffers>()
            {
                new Core.Entities.FavoriteJobOffers()
                {
                    FJOID = 1,
                },
            };

            context.Setup(x => x.FavoriteJobOffers).Returns(favoriteJobOffers.AsQueryable());

            context.Setup(x => x.CreateOrUpdate(It.IsAny<Core.Entities.FavoriteJobOffers>())).Callback<Core.Entities.FavoriteJobOffers>(favoriteJobOffers.Add);
        }

        [Test]
        public void TestAddFavoriteJobOfferCommandHandler_FavoriteJobOfferIsCorrect_ShouldAdd_NewFavoriteJobOffer()
        {
            //Arrange 
            var model = new AddFavoriteJobOfferViewModel()
            {
                JOGID = new Guid("33dd879c-ee2f-11db-8314-0800200c9a66"),
            };

            var command = new AddFavoriteJobOfferCommand() { Model = model };
            var handler = new AddFavoriteJobOfferCommandHandler(context.Object, user.Object);

            user.Setup(x => x.UGID).Returns("31dd879c-ee2f-11db-8314-0800200c9a66");

            //Act
            handler.Handle(command);

            //Assert
            ClassicAssert.AreEqual(2, favoriteJobOffers.Count);
            ClassicAssert.AreEqual(new Guid("31dd879c-ee2f-11db-8314-0800200c9a66"), favoriteJobOffers[1].FJOUGID);
            ClassicAssert.AreEqual(new Guid("33dd879c-ee2f-11db-8314-0800200c9a66"), favoriteJobOffers[1].FJOJOGID);
        }
    }
}
