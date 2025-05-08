using JuniorBoardIT.Core.Controllers;
using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.CQRS.Resources.Accounts.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Accounts.Queries;
using JuniorBoardIT.Core.Models.ViewModels.AccountsViewModel;
using Moq;
using NUnit.Framework;

namespace JuniorBoardIT.UnitTests.Controllers
{
    [TestFixture]
    public class TestAccountsController
    {
        private Mock<IDispatcher> dispatcher;

        [SetUp]
        public void SetUp() => dispatcher = new Mock<IDispatcher>();

        [Test]
        public void TestAccountsController_Register_ShouldDispatch_RegisterUserCommand()
        {
            //Arrange
            var controller = new AccountsController(dispatcher.Object);

            //Act
            controller.Register(new RegisterViewModel());

            //Assert

            dispatcher.Verify(x => x.DispatchCommand(It.IsAny<RegisterUserCommand>()), Times.Once);
        }

        [Test]
        public void TestAccountsController_Login_ShouldDispatch_LoginQuery()
        {
            //Arrange
            var controller = new AccountsController(dispatcher.Object);

            //Act
            controller.Login("", "");

            //Assert

            dispatcher.Verify(x => x.DispatchQuery<LoginQuery, string>(It.IsAny<LoginQuery>()), Times.Once);
        }
    }
}
