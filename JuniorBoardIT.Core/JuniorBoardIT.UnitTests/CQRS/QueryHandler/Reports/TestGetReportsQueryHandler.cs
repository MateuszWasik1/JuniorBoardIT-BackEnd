using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Queries;
using JuniorBoardIT.Core.Models.Enums;
using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;
using JuniorBoardIT.Core.Services;
using Moq;
using NUnit.Framework;

namespace JuniorBoardIT.UnitTests.CQRS.QueryHandler.Reports
{
    [TestFixture]
    public class TestGetReportsQueryHandler
    {
        private Mock<IDataBaseContext> context;
        private Mock<IMapper> mapper;
        private Mock<IUserContext> userContext;

        private List<Core.Entities.Reports> reports;
        private List<Core.Entities.User> users;

        private Guid userGid;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();
            mapper = new Mock<IMapper>();
            userContext = new Mock<IUserContext>();

            userGid = Guid.NewGuid();

            reports = new List<Core.Entities.Reports>()
            {
                new Core.Entities.Reports
                {
                    RID = 1,
                    RSupportGID = null,
                    RStatus = ReportsStatusEnum.New,
                    RText = "Pierwszy raport",
                    RDate = DateTime.UtcNow.AddDays(-2)
                },
                new Core.Entities.Reports
                {
                    RID = 2,
                    RSupportGID = userGid,
                    RStatus = ReportsStatusEnum.InVerification,
                    RText = "Drugi raport",
                    RDate = DateTime.UtcNow.AddDays(-1)
                },
                new Core.Entities.Reports
                {
                    RID = 3,
                    RSupportGID = Guid.NewGuid(),
                    RStatus = ReportsStatusEnum.InVerification,
                    RText = "Trzeci raport",
                    RDate = DateTime.UtcNow
                }
            };

            users = new List<Core.Entities.User>()
            {
                new Core.Entities.User
                {
                    UID = 1,
                    UGID = userGid,
                    URID = (int) RoleEnum.Support
                }
            };

            context.Setup(x => x.Reports).Returns(reports.AsQueryable());
            context.Setup(x => x.User).Returns(users.AsQueryable());

            userContext.Setup(x => x.UID).Returns(1);
            userContext.Setup(x => x.UGID).Returns(userGid.ToString());

            mapper.Setup(x => x.Map<Core.Entities.Reports, ReportsViewModel>(It.IsAny<Core.Entities.Reports>())).Returns<Core.Entities.Reports>(r => new ReportsViewModel { RID = r.RID });
        }

        [Test]
        public void TestGetReportsQueryHandler_ReportTypeNew_ShouldReturn_OnlyNewReports()
        {
            // Arrange
            var query = new GetReportsQuery
            {
                ReportType = ReportsTypeEnum.New,
                Skip = 0,
                Take = 10
            };

            var handler = new GetReportsQueryHandler(context.Object, userContext.Object, mapper.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.List.Count, Is.EqualTo(1));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.List[0].RID, Is.EqualTo(1));
        }

        [Test]
        public void TestGetReportsQueryHandler_ReportTypeImVerificator_ShouldReturn_AssignedReports()
        {
            // Arrange
            var query = new GetReportsQuery
            {
                ReportType = ReportsTypeEnum.ImVerificator,
                Skip = 0,
                Take = 10
            };

            var handler = new GetReportsQueryHandler(context.Object, userContext.Object, mapper.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.List.Count, Is.EqualTo(1));
            Assert.That(result.List[0].RID, Is.EqualTo(2));
        }

        [Test]
        public void TestGetReportsQueryHandler_ReportTypeAll_Admin_ShouldReturn_AllReports()
        {
            // Arrange
            users[0].URID = (int) RoleEnum.Admin;

            var query = new GetReportsQuery
            {
                ReportType = ReportsTypeEnum.All,
                Skip = 0,
                Take = 10
            };

            var handler = new GetReportsQueryHandler(context.Object, userContext.Object, mapper.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.List.Count, Is.EqualTo(3));
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void TestGetReportsQueryHandler_ReportTypeAll_NotAdmin_ShouldReturn_FilteredReports()
        {
            // Arrange
            var query = new GetReportsQuery
            {
                ReportType = ReportsTypeEnum.All,
                Skip = 0,
                Take = 10
            };

            var handler = new GetReportsQueryHandler(context.Object, userContext.Object, mapper.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.List.Count, Is.EqualTo(1));
            Assert.That(result.List[0].RID, Is.EqualTo(2));
        }

        [Test]
        public void TestGetReportsQueryHandler_WithMessageFilter_ShouldReturn_MatchingReports()
        {
            // Arrange
            var query = new GetReportsQuery
            {
                ReportType = ReportsTypeEnum.All,
                Message = "Drugi",
                Skip = 0,
                Take = 10
            };

            var handler = new GetReportsQueryHandler(context.Object, userContext.Object, mapper.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.List.Count, Is.EqualTo(1));
            Assert.That(result.List[0].RID, Is.EqualTo(2));
        }

        [Test]
        public void TestGetReportsQueryHandler_ShouldApply_SkipAndTake()
        {
            // Arrange
            users[0].URID = (int) RoleEnum.Admin;

            var query = new GetReportsQuery
            {
                ReportType = ReportsTypeEnum.All,
                Skip = 1,
                Take = 1
            };

            var handler = new GetReportsQueryHandler(context.Object, userContext.Object, mapper.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.List.Count, Is.EqualTo(1));
            Assert.That(result.Count, Is.EqualTo(3));
        }
    }
}
