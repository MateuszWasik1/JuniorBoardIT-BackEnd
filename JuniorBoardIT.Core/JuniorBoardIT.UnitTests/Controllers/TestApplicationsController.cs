using JuniorBoardIT.Core.Controllers;
using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.CQRS.Resources.Applications.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Applications.Queries;
using JuniorBoardIT.Core.Models.ViewModels.ApplicationsViewModels;
using Moq;
using NUnit.Framework;

namespace JuniorBoardIT.UnitTests.Controllers
{
    [TestFixture]
    public class TestApplicationsController
    {
        private Mock<IDispatcher> dispatcher;

        [SetUp]
        public void SetUp() => dispatcher = new Mock<IDispatcher>();

        [Test]
        public void ApplicationsController_GetApplications_ShouldDispatch_LoginQuery()
        {
            //Arrange
            var controller = new ApplicationsController(dispatcher.Object);

            //Act
            controller.GetApplications(0, 0, new Guid());

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetApplicationsQuery, ApplicationsViewModel>(It.IsAny<GetApplicationsQuery>()), Times.Once);
        }

        [Test]
        public void ApplicationsController_AddApplication_ShouldDispatch_AddApplicationCommand()
        {
            //Arrange
            var controller = new ApplicationsController(dispatcher.Object);

            //Act
            controller.AddApplication(new AddApplicationViewModel());

            //Assert
            dispatcher.Verify(x => x.DispatchCommand(It.IsAny<AddApplicationCommand>()), Times.Once);
        }


        [Test]
        public void ApplicationsController_DeleteApplication_ShouldDispatch_DeleteApplicationCommand()
        {
            //Arrange
            var controller = new ApplicationsController(dispatcher.Object);

            //Act
            controller.DeleteApplication(new Guid());

            //Assert
            dispatcher.Verify(x => x.DispatchCommand(It.IsAny<DeleteApplicationCommand>()), Times.Once);
        }
    }
}
