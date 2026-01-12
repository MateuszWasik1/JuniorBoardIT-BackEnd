using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.Stats.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.Stats.Queries;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace JuniorBoardIT.UnitTests.CQRS.QueryHandler.Stats
{
    [TestFixture]
    public class TestGetNumberOfCompaniesPublishedOffertsQueryHandler
    {
        private Mock<IDataBaseContext> context;

        private List<Core.Entities.JobOffers> jobOffers;

        private DateTime startDate;
        private DateTime endDate;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();

            startDate = new DateTime(2024, 1, 15);
            endDate = new DateTime(2024, 3, 10);

            jobOffers = new List<Core.Entities.JobOffers>
            {
                new Core.Entities.JobOffers
                {
                    JOPostedAt = new DateTime(2024, 1, 15),
                    JOCreatedAt = new DateTime(2024, 1, 15)
                },
                new Core.Entities.JobOffers
                {
                    JOPostedAt = new DateTime(2024, 1, 20),
                    JOCreatedAt = new DateTime(2024, 1, 20)
                },
                new Core.Entities.JobOffers
                {
                    JOPostedAt = new DateTime(2024, 2, 5),
                    JOCreatedAt = new DateTime(2024, 2, 5)
                },
                new Core.Entities.JobOffers
                {
                    JOPostedAt = new DateTime(2024, 3, 1),
                    JOCreatedAt = new DateTime(2024, 3, 1)
                },
                new Core.Entities.JobOffers
                {
                    JOPostedAt = new DateTime(2024, 3, 15),
                    JOCreatedAt = new DateTime(2024, 3, 15)
                }
            };

            context.Setup(x => x.JobOffers).Returns(jobOffers.AsQueryable());
        }

        [Test]
        public void TestGetNumberOfCompaniesPublishedOffertsQueryHandler_ShouldCount_OffersPerMonthCorrectly()
        {
            // Arrange
            var query = new GetNumberOfCompaniesPublishedOffertsQuery
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var handler = new GetNumberOfCompaniesPublishedOffertsQueryHandler(context.Object);

            // Act
            var result = handler.Handle(query);

            //Assert
            Assert.That(result.Datasets.Data.Count, Is.EqualTo(3));
            Assert.That(result.Datasets.Data[0], Is.EqualTo(2));
            Assert.That(result.Datasets.Data[1], Is.EqualTo(1));
            Assert.That(result.Datasets.Data[2], Is.EqualTo(1));
        }

        [Test]
        public void TestGetNumberOfCompaniesPublishedOffertsQueryHandler_ShouldGenerate_CorrectLabels()
        {
            // Arrange
            var query = new GetNumberOfCompaniesPublishedOffertsQuery
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var handler = new GetNumberOfCompaniesPublishedOffertsQueryHandler(context.Object);

            // Act
            var result = handler.Handle(query);

            // Arrange
            CollectionAssert.AreEqual(new[] { "2024-1", "2024-2", "2024-3" }, result.Labels);
        }

        [Test]
        public void TestGetNumberOfCompaniesPublishedOffertsQueryHandler_ShouldRespect_PartialFirstMonth()
        {
            // Arrange
            var query = new GetNumberOfCompaniesPublishedOffertsQuery
            {
                StartDate = new DateTime(2024, 1, 20),
                EndDate = endDate
            };

            var handler = new GetNumberOfCompaniesPublishedOffertsQueryHandler(context.Object);

            // Act
            var result = handler.Handle(query);

            // Arrange
            Assert.That(result.Datasets.Data[0], Is.EqualTo(1)); // tylko 20 stycznia
        }

        [Test]
        public void TestGetNumberOfCompaniesPublishedOffertsQueryHandler_ShouldRespect_PartialLastMonth()
        {
            // Arrange
            var query = new GetNumberOfCompaniesPublishedOffertsQuery
            {
                StartDate = startDate,
                EndDate = new DateTime(2024, 3, 1)
            };

            var handler = new GetNumberOfCompaniesPublishedOffertsQueryHandler(context.Object);

            // Act
            var result = handler.Handle(query);

            // Arrange
            Assert.That(result.Datasets.Data.Last(), Is.EqualTo(1));
        }

        [Test]
        public void TestGetNumberOfCompaniesPublishedOffertsQueryHandler_NoOffers_ShouldReturn_Zeros()
        {
            // Arrange
            jobOffers.Clear();

            var query = new GetNumberOfCompaniesPublishedOffertsQuery
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var handler = new GetNumberOfCompaniesPublishedOffertsQueryHandler(context.Object);

            // Act
            var result = handler.Handle(query);

            // Arrange
            Assert.That(result.Datasets.Data.All(x => x == 0), Is.True);
        }

        [Test]
        public void TestGetNumberOfCompaniesPublishedOffertsQueryHandler_ShouldReturn_ValidChartStructure()
        {
            // Arrange
            var query = new GetNumberOfCompaniesPublishedOffertsQuery
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var handler = new GetNumberOfCompaniesPublishedOffertsQueryHandler(context.Object);

            // Act
            var result = handler.Handle(query);

            // Arrange
            Assert.That(result.Labels, Is.Not.Null);
            Assert.That(result.Datasets, Is.Not.Null);
            Assert.That(result.Datasets.Data, Is.Not.Null);
        }

    }
}
