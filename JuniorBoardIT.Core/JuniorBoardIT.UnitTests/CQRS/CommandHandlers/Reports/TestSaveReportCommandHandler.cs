using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Handlers;
using JuniorBoardIT.Core.Models.Enums;
using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;
using JuniorBoardIT.Core.Services;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace JuniorBoardIT.UnitTests.CQRS.CommandHandlers.Reports
{
    [TestFixture]
    public class TestSaveReportCommandHandler
    {
        private Mock<IDataBaseContext> context;
        private Mock<IUserContext>? user;

        private List<Core.Entities.Reports> reports;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();
            user = new Mock<IUserContext>();

            reports = new List<Core.Entities.Reports>()
            {
                new Core.Entities.Reports()
                {
                    RID = 1,
                },
            };

            context.Setup(x => x.Reports).Returns(reports.AsQueryable());

            context.Setup(x => x.CreateOrUpdate(It.IsAny<Core.Entities.Reports>())).Callback<Core.Entities.Reports>(reports.Add);
        }

        [Test]
        public void TestSaveReportCommandHandler_ReportIsCorrect_ShouldAdd_Report()
        {
            //Arrange 
            var model = new ReportViewModel()
            {
                RJOGID = new Guid("29dd879c-ee2f-11db-8314-0800200c9a66"),
                RReasons = "Resons",
                RText = "Text",
                RStatus = ReportsStatusEnum.Accepted
            };

            var command = new SaveReportCommand() { Model = model };
            var handler = new SaveReportCommandHandler(context.Object, user.Object);

            user.Setup(x => x.UGID).Returns("31dd879c-ee2f-11db-8314-0800200c9a66");

            //Act
            handler.Handle(command);

            //Assert
            ClassicAssert.AreEqual(2, reports.Count);
            ClassicAssert.AreEqual(model.RJOGID, reports[1].RJOGID);
            ClassicAssert.AreEqual(new Guid("31dd879c-ee2f-11db-8314-0800200c9a66"), reports[1].RReporterGID);
            ClassicAssert.AreEqual(model.RReasons, reports[1].RReasons);
            ClassicAssert.AreEqual(model.RText, reports[1].RText);
            ClassicAssert.AreEqual(model.RStatus, reports[1].RStatus);
        }
    }
}
