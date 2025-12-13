using JuniorBoardIT.Core.Controllers;
using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.CQRS.Resources.Stats.Queries;
using JuniorBoardIT.Core.Models.ViewModels.StatsViewModels;
using Moq;
using NUnit.Framework;

namespace JuniorBoardIT.UnitTests.Controllers
{
    [TestFixture]
    public class TestStatsController
    {
        private Mock<IDispatcher> dispatcher;

        [SetUp]
        public void SetUp() => dispatcher = new Mock<IDispatcher>();

        [Test]
        public void StatsController_GetNumberOfRecruiterPublishedOfferts_ShouldDispatch_GetNumberOfRecruiterPublishedOffertsQuery()
        {
            //Arrange
            var controller = new StatsController(dispatcher.Object);

            //Act
            controller.GetNumberOfRecruiterPublishedOfferts(new DateTime(), new DateTime());

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetNumberOfRecruiterPublishedOffertsQuery, StatsBarChartViewModel>(It.IsAny<GetNumberOfRecruiterPublishedOffertsQuery>()), Times.Once);
        }

        [Test]
        public void StatsController_GetNumberOfCompanyPublishedOfferts_ShouldDispatch_GetNumberOfCompanyPublishedOffertsQuery()
        {
            //Arrange
            var controller = new StatsController(dispatcher.Object);

            //Act
            controller.GetNumberOfCompanyPublishedOfferts(new DateTime(), new DateTime(), new Guid());

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetNumberOfCompanyPublishedOffertsQuery, StatsBarChartViewModel>(It.IsAny<GetNumberOfCompanyPublishedOffertsQuery>()), Times.Once);
        }

        [Test]
        public void StatsController_GetNumberOfCompaniesPublishedOfferts_ShouldDispatch_GetNumberOfCompaniesPublishedOffertsQuery()
        {
            //Arrange
            var controller = new StatsController(dispatcher.Object);

            //Act
            controller.GetNumberOfCompaniesPublishedOfferts(new DateTime(), new DateTime());

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetNumberOfCompaniesPublishedOffertsQuery, StatsBarChartViewModel>(It.IsAny<GetNumberOfCompaniesPublishedOffertsQuery>()), Times.Once);
        }

        [Test]
        public void StatsController_GetNumberOfActiveCompaniesOfferts_ShouldDispatch_GetNumberOfActiveCompaniesOffertsQuery()
        {
            //Arrange
            var controller = new StatsController(dispatcher.Object);

            //Act
            controller.GetNumberOfActiveCompaniesOfferts(new DateTime(), new Guid());

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetNumberOfActiveCompaniesOffertsQuery, StatsBarChartViewModel>(It.IsAny<GetNumberOfActiveCompaniesOffertsQuery>()), Times.Once);
        }

        [Test]
        public void StatsController_GetNumberOfCompanyRecruiters_ShouldDispatch_GetNumberOfCompanyRecruitersQuery()
        {
            //Arrange
            var controller = new StatsController(dispatcher.Object);

            //Act
            controller.GetNumberOfCompanyRecruiters(new Guid());

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetNumberOfCompanyRecruitersQuery, StatsBarChartViewModel>(It.IsAny<GetNumberOfCompanyRecruitersQuery>()), Times.Once);
        }
    }
}
