using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Handlers;
using JuniorBoardIT.Core.Exceptions;
using JuniorBoardIT.Core.Exceptions.Reports;
using JuniorBoardIT.Core.Exceptions.Users;
using JuniorBoardIT.Core.Models.Enums;
using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;
using JuniorBoardIT.Core.Services;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace JuniorBoardIT.UnitTests.CQRS.CommandHandlers.Reports
{
    [TestFixture]
    public class TestChangeReportStatusCommandHandler
    {
        private Mock<IDataBaseContext> context;
        private Mock<IUserContext>? user;

        private List<Core.Entities.Reports> reports;
        private List<Core.Entities.User> users;

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
                    RGID = new Guid("29dd879c-ee2f-11db-8314-0800200c9a66"),
                    RSupportGID = null
                },
                new Core.Entities.Reports()
                {
                    RID = 2,
                    RGID = new Guid("30dd879c-ee2f-11db-8314-0800200c9a66"),
                    RSupportGID = Guid.Empty
                },
            };

            users = new List<Core.Entities.User>()
            {
                new Core.Entities.User
                {
                    UID = 1,
                    UGID = new Guid("31dd879c-ee2f-11db-8314-0800200c9a66"),
                    URID = (int) RoleEnum.User
                },
                new Core.Entities.User
                {
                    UID = 2,
                    UGID = new Guid("32dd879c-ee2f-11db-8314-0800200c9a66"),
                    URID = (int) RoleEnum.Support
                },
                 new Core.Entities.User
                {
                    UID = 3,
                    UGID = new Guid("33dd879c-ee2f-11db-8314-0800200c9a66"),
                    URID = (int) RoleEnum.Admin
                }
            };

            context.Setup(x => x.Reports).Returns(reports.AsQueryable());
            context.Setup(x => x.User).Returns(users.AsQueryable());

            context.Setup(x => x.CreateOrUpdate(It.IsAny<Core.Entities.Reports>())).Callback<Core.Entities.Reports>(report => reports[reports.FindIndex(x => x.RGID == report.RGID)] = report);
        }

        [Test]
        public void TestChangeReportStatusCommand_ReportnIsNotFound_ShouldThrow_ReportNotFoundExceptions()
        {
            //Arrange 
            var model = new ChangeReportStatusViewModel()
            {
                RGID = Guid.NewGuid(),
                Status = ReportsStatusEnum.Rejected
            };

            var command = new ChangeReportStatusCommand() { Model = model };
            var handler = new ChangeReportStatusCommandHandler(context.Object, user.Object);

            //Act
            //Assert
            Assert.Throws<ReportNotFoundExceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestChangeReportStatusCommand_UserIsNotFound_ShouldThrow_UserNotFoundExceptions()
        {
            //Arrange 
            user.Setup(x => x.UID).Returns(99);

            var model = new ChangeReportStatusViewModel()
            {
                RGID = reports[0].RGID,
                Status = ReportsStatusEnum.Rejected
            };

            var command = new ChangeReportStatusCommand() { Model = model };
            var handler = new ChangeReportStatusCommandHandler(context.Object, user.Object);

            //Act
            //Assert
            Assert.Throws<UserNotFoundExceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestChangeReportStatusCommand_UserIsNotSupportOrAdmin_ShouldThrow_UserRoleException()
        {
            //Arrange 
            user.Setup(x => x.UID).Returns(1);

            var model = new ChangeReportStatusViewModel()
            {
                RGID = reports[0].RGID,
                Status = ReportsStatusEnum.Rejected
            };

            var command = new ChangeReportStatusCommand() { Model = model };
            var handler = new ChangeReportStatusCommandHandler(context.Object, user.Object);

            //Act
            //Assert
            Assert.Throws<UserRoleException>(() => handler.Handle(command));
        }

        [TestCase(2, "32dd879c-ee2f-11db-8314-0800200c9a66", "29dd879c-ee2f-11db-8314-0800200c9a66", ReportsStatusEnum.Rejected)]
        [TestCase(3, "33dd879c-ee2f-11db-8314-0800200c9a66", "30dd879c-ee2f-11db-8314-0800200c9a66", ReportsStatusEnum.New)]
        public void TestChangeReportStatusCommand_UserIsSupportOrAdmin_ShouldThrow_ChangeStatus(int userID, Guid userGID, Guid reportGID, ReportsStatusEnum reportStatus)
        {
            //Arrange 
            user.Setup(x => x.UID).Returns(userID);

            var model = new ChangeReportStatusViewModel()
            {
                RGID = reportGID,
                Status = reportStatus
            };

            var command = new ChangeReportStatusCommand() { Model = model };
            var handler = new ChangeReportStatusCommandHandler(context.Object, user.Object);

            //Act
            handler.Handle(command);

            //Assert
            ClassicAssert.AreEqual(userGID, reports.FirstOrDefault(x => x.RGID == reportGID).RSupportGID);
            ClassicAssert.AreEqual(model.Status, reports.FirstOrDefault(x => x.RGID == reportGID).RStatus);
        }
    }
}
