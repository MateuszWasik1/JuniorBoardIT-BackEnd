using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.Stats.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.Stats.Queries;
using JuniorBoardIT.Core.Services;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace JuniorBoardIT.UnitTests.CQRS.QueryHandler.Stats
{
    [TestFixture]
    public class TestGetNumberOfRecruiterPublishedOffertsQueryHandler
    {
        private Mock<IDataBaseContext> context;
        private Mock<IUserContext> userContext;

        private List<Core.Entities.JobOffers> jobOffers;

        private Guid userGid;
        private DateTime startDate;
        private DateTime endDate;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();
            userContext = new Mock<IUserContext>();

            userGid = Guid.NewGuid();

            startDate = new DateTime(2024, 1, 15);
            endDate = new DateTime(2024, 3, 10);

            jobOffers = new List<Core.Entities.JobOffers>
            {
                new Core.Entities.JobOffers
                {
                    JORGID = userGid,
                    JOCreatedAt = new DateTime(2024, 1, 15)
                },
                new Core.Entities.JobOffers
                {
                    JORGID = userGid,
                    JOCreatedAt = new DateTime(2024, 1, 20)
                },
                new Core.Entities.JobOffers
                {
                    JORGID = userGid,
                    JOCreatedAt = new DateTime(2024, 2, 5)
                },
                new Core.Entities.JobOffers
                {
                    JORGID = userGid,
                    JOCreatedAt = new DateTime(2024, 3, 1)
                },
                new Core.Entities.JobOffers
                {
                    JORGID = userGid,
                    JOCreatedAt = new DateTime(2024, 3, 15)
                },
                new Core.Entities.JobOffers
                {
                    JORGID = Guid.NewGuid(),
                    JOCreatedAt = new DateTime(2024, 2, 10)
                }
            };

            context.Setup(x => x.JobOffers).Returns(jobOffers.AsQueryable());

            userContext.Setup(x => x.UGID).Returns(userGid.ToString());
        }

        [Test]
        public void TestGetNumberOfRecruiterPublishedOffertsQueryHandler_ShouldCountJobOffersPerMonthCorrectly()
        {
            // Arrange
            var query = new GetNumberOfRecruiterPublishedOffertsQuery
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var handler = new GetNumberOfRecruiterPublishedOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Datasets.Data.Count, Is.EqualTo(3));
            Assert.That(result.Datasets.Data[0], Is.EqualTo(2));
            Assert.That(result.Datasets.Data[1], Is.EqualTo(1));
            Assert.That(result.Datasets.Data[2], Is.EqualTo(1));
        }

        [Test]
        public void TestGetNumberOfRecruiterPublishedOffertsQueryHandler_ShouldGenerate_CorrectLabels()
        {
            // Arrange
            var query = new GetNumberOfRecruiterPublishedOffertsQuery
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var handler = new GetNumberOfRecruiterPublishedOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            CollectionAssert.AreEqual(new[] { "2024-1", "2024-2", "2024-3" }, result.Labels);
        }

        [Test]
        public void TestGetNumberOfRecruiterPublishedOffertsQueryHandler_ShouldRespect_PartialFirstMonth()
        {
            // Arrange
            var query = new GetNumberOfRecruiterPublishedOffertsQuery
            {
                StartDate = new DateTime(2024, 1, 20),
                EndDate = endDate
            };

            var handler = new GetNumberOfRecruiterPublishedOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Datasets.Data[0], Is.EqualTo(1));
        }

        [Test]
        public void TestGetNumberOfRecruiterPublishedOffertsQueryHandler_ShouldRespect_PartialLastMonth()
        {
            // Arrange
            var query = new GetNumberOfRecruiterPublishedOffertsQuery
            {
                StartDate = startDate,
                EndDate = new DateTime(2024, 3, 1)
            };

            var handler = new GetNumberOfRecruiterPublishedOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Datasets.Data.Last(), Is.EqualTo(1));
        }

        [Test]
        public void TestGetNumberOfRecruiterPublishedOffertsQueryHandler_NoJobOffersForUser_ShouldReturn_Zeros()
        {
            // Arrange
            jobOffers.Clear();

            var query = new GetNumberOfRecruiterPublishedOffertsQuery
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var handler = new GetNumberOfRecruiterPublishedOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Datasets.Data.All(x => x == 0), Is.True);
        }

        [Test]
        public void TestGetNumberOfRecruiterPublishedOffertsQueryHandler_ShouldReturn_CorrectChartStructure()
        {
            // Arrange
            var query = new GetNumberOfRecruiterPublishedOffertsQuery
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var handler = new GetNumberOfRecruiterPublishedOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Labels, Is.Not.Null);
            Assert.That(result.Datasets, Is.Not.Null);
            Assert.That(result.Datasets.Data, Is.Not.Null);
            Assert.That(result.Labels.Count, Is.EqualTo(result.Datasets.Data.Count));
        }

        [Test]
        public void TestGetNumberOfRecruiterPublishedOffertsQueryHandler_ShouldIgnore_OffersFromOtherUsers()
        {
            // Arrange
            var query = new GetNumberOfRecruiterPublishedOffertsQuery
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var handler = new GetNumberOfRecruiterPublishedOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Datasets.Data.Sum(), Is.EqualTo(4));
        }
    }
}
