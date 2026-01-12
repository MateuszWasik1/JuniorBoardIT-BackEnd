using JuniorBoardIT.Core.Controllers;
using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Queries;
using JuniorBoardIT.Core.Models.Enums;
using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;
using Moq;
using NUnit.Framework;

namespace JuniorBoardIT.UnitTests.Controllers
{
    [TestFixture]
    public class TestReportsController
    {
        private Mock<IDispatcher> dispatcher;

        [SetUp]
        public void SetUp() => dispatcher = new Mock<IDispatcher>();

        [Test]
        public void ReportsController_GetReport_ShouldDispatch_GetReportQuery()
        {
            //Arrange
            var controller = new ReportsController(dispatcher.Object);

            //Act
            controller.GetReport(new Guid());

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetReportQuery, GetReportViewModel>(It.IsAny<GetReportQuery>()), Times.Once);
        }

        [Test]
        public void ReportsController_GetReports_ShouldDispatch_GetReportsQuery()
        {
            //Arrange
            var controller = new ReportsController(dispatcher.Object);

            //Act
            controller.GetReports(ReportsTypeEnum.New, 0, 0, "");

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetReportsQuery, GetReportsViewModel>(It.IsAny<GetReportsQuery>()), Times.Once);
        }

        [Test]
        public void ReportsController_SaveReport_ShouldDispatch_SaveReportCommand()
        {
            //Arrange
            var controller = new ReportsController(dispatcher.Object);

            //Act
            controller.SaveReport(new ReportViewModel());

            //Assert
            dispatcher.Verify(x => x.DispatchCommand(It.IsAny<SaveReportCommand>()), Times.Once);
        }

        [Test]
        public void ReportsController_ChangeReportStatus_ShouldDispatch_ChangeReportStatusCommand()
        {
            //Arrange
            var controller = new ReportsController(dispatcher.Object);

            //Act
            controller.ChangeReportStatus(new ChangeReportStatusViewModel());

            //Assert
            dispatcher.Verify(x => x.DispatchCommand(It.IsAny<ChangeReportStatusCommand>()), Times.Once);
        }
    }
}
