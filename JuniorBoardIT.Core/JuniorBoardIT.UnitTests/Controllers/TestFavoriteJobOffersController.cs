using JuniorBoardIT.Core.Controllers;
using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.CQRS.Resources.FavoriteJobOffers.Commands;
using JuniorBoardIT.Core.Models.ViewModels.FavoriteJobOffersViewModel;
using Moq;
using NUnit.Framework;

namespace JuniorBoardIT.UnitTests.Controllers
{
    [TestFixture]
    public class TestFavoriteJobOffersController
    {
        private Mock<IDispatcher> dispatcher;

        [SetUp]
        public void SetUp() => dispatcher = new Mock<IDispatcher>();

        [Test]
        public void FavoriteJobOffersController_AddFavoriteJobOffer_ShouldDispatch_AddFavoriteJobOfferCommand()
        {
            //Arrange
            var controller = new FavoriteJobOffersController(dispatcher.Object);

            //Act
            controller.AddFavoriteJobOffer(new AddFavoriteJobOfferViewModel());

            //Assert
            dispatcher.Verify(x => x.DispatchCommand(It.IsAny<AddFavoriteJobOfferCommand>()), Times.Once);
        }

        [Test]
        public void FavoriteJobOffersController_DeleteFavoriteJobOffer_ShouldDispatch_DeleteFavoriteJobOfferCommand()
        {
            //Arrange
            var controller = new FavoriteJobOffersController(dispatcher.Object);

            //Act
            controller.DeleteFavoriteJobOffer(new Guid());

            //Assert
            dispatcher.Verify(x => x.DispatchCommand(It.IsAny<DeleteFavoriteJobOfferCommand>()), Times.Once);
        }
    }
}
