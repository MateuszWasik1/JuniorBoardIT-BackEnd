using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.Stats.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.Stats.Queries;
using JuniorBoardIT.Core.Exceptions.Companies;
using JuniorBoardIT.Core.Services;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace JuniorBoardIT.UnitTests.CQRS.QueryHandler.Stats
{
    [TestFixture]
    public class TestGetNumberOfCompanyPublishedOffertsQueryHandler
    {

        private Mock<IDataBaseContext> context;
        private Mock<IUserContext> userContext;

        private List<Core.Entities.User> users;
        private List<Core.Entities.Companies> companies;
        private List<Core.Entities.JobOffers> jobOffers;

        private Guid companyGid;
        private Guid userGid;

        private DateTime startDate;
        private DateTime endDate;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();
            userContext = new Mock<IUserContext>();

            companyGid = Guid.NewGuid();
            userGid = Guid.NewGuid();

            startDate = new DateTime(2024, 1, 15);
            endDate = new DateTime(2024, 3, 10);

            users = new List<Core.Entities.User>
            {
                new Core.Entities.User
                {
                    UGID = userGid,
                    UCompanyGID = companyGid
                }
            };

            companies = new List<Core.Entities.Companies>
            {
                new Core.Entities.Companies
                {
                    CGID = companyGid,
                    CName = "Test Company"
                }
            };

            jobOffers = new List<Core.Entities.JobOffers>
            {
                new Core.Entities.JobOffers
                {
                    JOCGID = companyGid,
                    JOPostedAt = new DateTime(2024, 1, 15)
                },
                new Core.Entities.JobOffers
                {
                    JOCGID = companyGid,
                    JOPostedAt = new DateTime(2024, 1, 20)
                },
                new Core.Entities.JobOffers
                {
                    JOCGID = companyGid,
                    JOPostedAt = new DateTime(2024, 2, 5)
                },
                new Core.Entities.JobOffers
                {
                    JOCGID = companyGid,
                    JOPostedAt = new DateTime(2024, 3, 1)
                },
                new Core.Entities.JobOffers
                {
                    JOCGID = companyGid,
                    JOPostedAt = new DateTime(2024, 3, 15)
                }
            };

            context.Setup(x => x.User).Returns(users.AsQueryable());
            context.Setup(x => x.Companies).Returns(companies.AsQueryable());
            context.Setup(x => x.JobOffers).Returns(jobOffers.AsQueryable());

            userContext.Setup(x => x.UGID).Returns(userGid.ToString());
        }

        [Test]
        public void TestGetNumberOfCompanyPublishedOffertsQueryHandler_QueryCGIDEmpty_ShouldUse_UsersCompanyGID()
        {
            // Arrange
            var query = new GetNumberOfCompanyPublishedOffertsQuery
            {
                CGID = Guid.Empty,
                StartDate = startDate,
                EndDate = endDate
            };

            var handler = new GetNumberOfCompanyPublishedOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Datasets.Data.Sum(), Is.EqualTo(4));
        }

        [Test]
        public void TestGetNumberOfCompanyPublishedOffertsQueryHandler_QueryCGIDProvided_ShouldUse_QueryCGID()
        {
            // Arrange
            var query = new GetNumberOfCompanyPublishedOffertsQuery
            {
                CGID = companyGid,
                StartDate = startDate,
                EndDate = endDate
            };

            var handler = new GetNumberOfCompanyPublishedOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Datasets.Data.Count, Is.EqualTo(3));
        }

        [Test]
        public void TestGetNumberOfCompanyPublishedOffertsQueryHandler_CompanyNotFound_ShouldThrow_CompanyNotFoundExceptions()
        {
            // Arrange
            companies.Clear();

            var query = new GetNumberOfCompanyPublishedOffertsQuery
            {
                CGID = companyGid,
                StartDate = startDate,
                EndDate = endDate
            };
            var handler = new GetNumberOfCompanyPublishedOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            // Assert
            Assert.Throws<CompanyNotFoundExceptions>(() => handler.Handle(query));
        }

        [Test]
        public void TestGetNumberOfCompanyPublishedOffertsQueryHandler_ShouldCount_JobOffersPerMonthCorrectly()
        {
            // Arrange
            var query = new GetNumberOfCompanyPublishedOffertsQuery
            {
                CGID = companyGid,
                StartDate = startDate,
                EndDate = endDate
            };

            var handler = new GetNumberOfCompanyPublishedOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Datasets.Data[0], Is.EqualTo(2));
            Assert.That(result.Datasets.Data[1], Is.EqualTo(1));
            Assert.That(result.Datasets.Data[2], Is.EqualTo(1));
        }

        [Test]
        public void TestGetNumberOfCompanyPublishedOffertsQueryHandler_ShouldRespect_PartialFirstMonth()
        {
            // Arrange
            var query = new GetNumberOfCompanyPublishedOffertsQuery
            {
                CGID = companyGid,
                StartDate = new DateTime(2024, 1, 20),
                EndDate = endDate
            };

            var handler = new GetNumberOfCompanyPublishedOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Datasets.Data[0], Is.EqualTo(1));
        }

        [Test]
        public void TestGetNumberOfCompanyPublishedOffertsQueryHandler_ShouldRespect_PartialLastMonth()
        {
            // Arrange
            var query = new GetNumberOfCompanyPublishedOffertsQuery
            {
                CGID = companyGid,
                StartDate = startDate,
                EndDate = new DateTime(2024, 3, 1)
            };

            var handler = new GetNumberOfCompanyPublishedOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Datasets.Data.Last(), Is.EqualTo(1));
        }

        [Test]
        public void TestGetNumberOfCompanyPublishedOffertsQueryHandler_NoJobOffers_ShouldReturn_Zeros()
        {
            // Arrange
            jobOffers.Clear();

            var query = new GetNumberOfCompanyPublishedOffertsQuery
            {
                CGID = companyGid,
                StartDate = startDate,
                EndDate = endDate
            };

            var handler = new GetNumberOfCompanyPublishedOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Datasets.Data.All(x => x == 0), Is.True);
        }

        [Test]
        public void TestGetNumberOfCompanyPublishedOffertsQueryHandler_ShouldGenerate_CorrectLabels()
        {
            // Arrange
            var query = new GetNumberOfCompanyPublishedOffertsQuery
            {
                CGID = companyGid,
                StartDate = startDate,
                EndDate = endDate
            };

            var handler = new GetNumberOfCompanyPublishedOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            // Assert
            CollectionAssert.AreEqual(new[] { "2024-1", "2024-2", "2024-3" }, handler.Handle(query).Labels);
        }

        [Test]
        public void TestGetNumberOfCompanyPublishedOffertsQueryHandler_ShouldContain_CompanyNameInDatasetLabel()
        {
            // Arrange
            var query = new GetNumberOfCompanyPublishedOffertsQuery
            {
                CGID = companyGid,
                StartDate = startDate,
                EndDate = endDate
            };

            var handler = new GetNumberOfCompanyPublishedOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            StringAssert.Contains("Test Company", result.Datasets.Label);
        }
    }
}
