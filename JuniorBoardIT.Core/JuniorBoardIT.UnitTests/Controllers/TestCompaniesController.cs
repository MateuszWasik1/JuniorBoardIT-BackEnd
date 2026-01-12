using JuniorBoardIT.Core.Controllers;
using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Queries;
using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;
using Moq;
using NUnit.Framework;

namespace JuniorBoardIT.UnitTests.Controllers
{
    [TestFixture]
    public class TestCompaniesController
    {
        private Mock<IDispatcher> dispatcher;

        [SetUp]
        public void SetUp() => dispatcher = new Mock<IDispatcher>();

        [Test]
        public void CompaniesController_GetCompanies_ShouldDispatch_GetCompaniesQuery()
        {
            //Arrange
            var controller = new CompaniesController(dispatcher.Object);

            //Act
            controller.GetCompanies(0, 0, "");

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetCompaniesQuery, GetCompaniesViewModel>(It.IsAny<GetCompaniesQuery>()), Times.Once);
        }

        [Test]
        public void CompaniesController_GetCompany_ShouldDispatch_GetCompanyQuery()
        {
            //Arrange
            var controller = new CompaniesController(dispatcher.Object);

            //Act
            controller.GetCompany(new Guid());

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetCompanyQuery, CompanyViewModel>(It.IsAny<GetCompanyQuery>()), Times.Once);
        }

        [Test]
        public void CompaniesController_GetCompaniesForUser_ShouldDispatch_GetCompaniesForUserQuery()
        {
            //Arrange
            var controller = new CompaniesController(dispatcher.Object);

            //Act
            controller.GetCompaniesForUser();

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetCompaniesForUserQuery, GetCompaniesForUserViewModel>(It.IsAny<GetCompaniesForUserQuery>()), Times.Once);
        }

        [Test]
        public void CompaniesController_AddCompany_ShouldDispatch_AddCompanyCommand()
        {
            //Arrange
            var controller = new CompaniesController(dispatcher.Object);

            //Act
            controller.AddCompany(new AddCompanyViewModel());

            //Assert
            dispatcher.Verify(x => x.DispatchCommand(It.IsAny<AddCompanyCommand>()), Times.Once);
        }

        [Test]
        public void CompaniesController_UpdateCompany_ShouldDispatch_UpdateCompanyCommand()
        {
            //Arrange
            var controller = new CompaniesController(dispatcher.Object);

            //Act
            controller.UpdateCompany(new CompanyViewModel());

            //Assert
            dispatcher.Verify(x => x.DispatchCommand(It.IsAny<UpdateCompanyCommand>()), Times.Once);
        }

        [Test]
        public void CompaniesController_DeleteCompany_ShouldDispatch_DeleteCompanyCommand()
        {
            //Arrange
            var controller = new CompaniesController(dispatcher.Object);

            //Act
            controller.DeleteCompany(new Guid());

            //Assert
            dispatcher.Verify(x => x.DispatchCommand(It.IsAny<DeleteCompanyCommand>()), Times.Once);
        }
    }
}
