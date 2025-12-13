using JuniorBoardIT.Core.Controllers;
using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Commands;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Queries;
using JuniorBoardIT.Core.Models.Enums.JobOffers;
using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;
using Moq;
using NUnit.Framework;

namespace JuniorBoardIT.UnitTests.Controllers
{
    [TestFixture]
    public class TestJobOffersController
    {
        private Mock<IDispatcher> dispatcher;

        [SetUp]
        public void SetUp() => dispatcher = new Mock<IDispatcher>();

        [Test]
        public void JobOffersController_GetAllJobOffers_ShouldDispatch_GetAllJobOffersQuery()
        {
            //Arrange
            var controller = new JobOffersController(dispatcher.Object);

            //Act
            controller.GetAllJobOffers(0, 0, ExpirenceEnum.Junior, CategoryEnum.FrontEnd, LocationEnum.Remote, EducationEnum.Elementary, EmploymentTypeEnum.UoP, SalaryEnum.Daily, false);

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetAllJobOffersQuery, GetAllJobOffersViewModel>(It.IsAny<GetAllJobOffersQuery>()), Times.Once);
        }

        [Test]
        public void JobOffersController_GetJobOffer_ShouldDispatch_GetJobOfferQuery()
        {
            //Arrange
            var controller = new JobOffersController(dispatcher.Object);

            //Act
            controller.GetJobOffer(new Guid());

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetJobOfferQuery, GetJobOfferViewModel>(It.IsAny<GetJobOfferQuery>()), Times.Once);
        }

        [Test]
        public void JobOffersController_AddJobOffer_ShouldDispatch_AddJobOfferCommand()
        {
            //Arrange
            var controller = new JobOffersController(dispatcher.Object);

            //Act
            controller.AddJobOffer(new JobOfferViewModel());

            //Assert
            dispatcher.Verify(x => x.DispatchCommand(It.IsAny<AddJobOfferCommand>()), Times.Once);
        }

        [Test]
        public void JobOffersController_UpdateJobOffer_ShouldDispatch_UpdateJobOfferCommand()
        {
            //Arrange
            var controller = new JobOffersController(dispatcher.Object);

            //Act
            controller.UpdateJobOffer(new JobOfferViewModel());

            //Assert
            dispatcher.Verify(x => x.DispatchCommand(It.IsAny<UpdateJobOfferCommand>()), Times.Once);
        }

        [Test]
        public void JobOffersController_DeleteJobOffer_ShouldDispatch_DeleteJobOfferCommand()
        {
            //Arrange
            var controller = new JobOffersController(dispatcher.Object);

            //Act
            controller.DeleteJobOffer(new Guid());

            //Assert
            dispatcher.Verify(x => x.DispatchCommand(It.IsAny<DeleteJobOfferCommand>()), Times.Once);
        }
    }
}
