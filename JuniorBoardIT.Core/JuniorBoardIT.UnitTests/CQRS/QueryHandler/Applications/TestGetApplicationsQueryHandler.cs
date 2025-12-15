using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.Applications.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.Applications.Queries;
using JuniorBoardIT.Core.Models.ViewModels.ApplicationsViewModels;
using JuniorBoardIT.Core.Services;
using Moq;
using NUnit.Framework;

namespace JuniorBoardIT.UnitTests.CQRS.QueryHandler.Applications
{
    [TestFixture]
    public class GetApplicationsQueryHandlerTests
    {
        private Mock<IDataBaseContext> context;
        private Mock<IMapper> mapper;
        private Mock<IUserContext> userContext;

        private List<Core.Entities.Applications> applications;

        private Guid userGuid;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();
            mapper = new Mock<IMapper>();
            userContext = new Mock<IUserContext>();

            userGuid = Guid.NewGuid();

            applications = new List<Core.Entities.Applications>()
            {
                new Core.Entities.Applications
                {
                    AGID = Guid.NewGuid(),
                    AUGID = userGuid,
                    AJOGID = Guid.NewGuid(),
                    AApplicationDate = DateTime.UtcNow
                },
                new Core.Entities.Applications
                {
                    AGID = Guid.NewGuid(),
                    AUGID = userGuid,
                    AJOGID = Guid.NewGuid(),
                    AApplicationDate = DateTime.UtcNow.AddDays(-1)
                },
                new Core.Entities.Applications
                {
                    AGID = Guid.NewGuid(),
                    AUGID = Guid.NewGuid(),
                    AJOGID = Guid.NewGuid(),
                    AApplicationDate = DateTime.UtcNow.AddDays(-2)
                }
            };

            context.Setup(x => x.Applications)
                   .Returns(applications.AsQueryable());

            userContext.Setup(x => x.UGID)
                       .Returns(userGuid.ToString());

            mapper.Setup(x => x.Map<Core.Entities.Applications, ApplicationViewModel>(It.IsAny<Core.Entities.Applications>()))
                  .Returns<Core.Entities.Applications>(a => new ApplicationViewModel
                  {
                      AGID = a.AGID,
                      AUGID = a.AUGID,
                      AJOGID = a.AJOGID,
                      AApplicationDate = a.AApplicationDate
                  });
        }

        [Test]
        public void GetApplicationsQueryHandler_QueryWithUGID_ShouldReturnOnlyApplicationsForThatUGID()
        {
            // Arrange
            var query = new GetApplicationsQuery
            {
                UGID = userGuid,
                Skip = 0,
                Take = 10
            };

            var handler = new GetApplicationsQueryHandler(context.Object, mapper.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.List.Count, Is.EqualTo(2));
            Assert.That(result.List.All(x => x.AUGID == userGuid), Is.True);
        }

        [Test]
        public void GetApplicationsQueryHandler_QueryWithoutUGID_Should_UseUserContextUGID()
        {
            // Arrange
            var query = new GetApplicationsQuery
            {
                UGID = Guid.Empty,
                Skip = 0,
                Take = 10
            };

            var handler = new GetApplicationsQueryHandler(context.Object, mapper.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.List.Count, Is.EqualTo(2));
            Assert.That(result.List.All(x => x.AUGID == userGuid), Is.True);
        }

        [Test]
        public void GetApplicationsQueryHandler_UserFound_Should_ApplySkipAndTakeCorrectly()
        {
            // Arrange
            var query = new GetApplicationsQuery
            {
                UGID = userGuid,
                Skip = 1,
                Take = 1
            };

            var handler = new GetApplicationsQueryHandler(context.Object, mapper.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));   // całkowita liczba
            Assert.That(result.List.Count, Is.EqualTo(1)); // po paginacji
        }

        [Test]
        public void GetApplicationsQueryHandler_NoApplicationsFound_ShouldReturn_EmptyList()
        {
            // Arrange
            var query = new GetApplicationsQuery
            {
                UGID = Guid.NewGuid(),
                Skip = 0,
                Take = 10
            };

            var handler = new GetApplicationsQueryHandler(context.Object, mapper.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
            Assert.That(result.List, Is.Empty);
        }
    }
}
