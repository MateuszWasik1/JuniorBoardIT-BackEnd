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
    public class TestGetNumberOfActiveCompaniesOffertsQueryHandler
    {
        private Mock<IDataBaseContext> context;
        private Mock<IUserContext> userContext;

        private List<Core.Entities.User> users;
        private List<Core.Entities.Companies> companies;
        private List<Core.Entities.JobOffers> jobOffers;

        private Guid companyGid;
        private Guid userGid;
        private DateTime date;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();
            userContext = new Mock<IUserContext>();

            companyGid = Guid.NewGuid();
            userGid = Guid.NewGuid();
            date = new DateTime(2024, 5, 10);

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
                    JORGID = companyGid,
                    JOPostedAt = date
                },
                new Core.Entities.JobOffers
                {
                    JORGID = companyGid,
                    JOPostedAt = date.AddDays(-1)
                }
            };

            context.Setup(x => x.User).Returns(users.AsQueryable());
            context.Setup(x => x.Companies).Returns(companies.AsQueryable());
            context.Setup(x => x.JobOffers).Returns(jobOffers.AsQueryable());

            userContext.Setup(x => x.UGID).Returns(userGid.ToString());
        }

        [Test]
        public void TestGetNumberOfActiveCompaniesOffertsQueryHandler_QueryCGIDEmpty_ShouldUse_UsersCompanyGID()
        {
            // Arrange
            var query = new GetNumberOfActiveCompaniesOffertsQuery
            {
                CGID = Guid.Empty,
                Date = date
            };

            var handler = new GetNumberOfActiveCompaniesOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Datasets.Data.First(), Is.EqualTo(1));
        }

        [Test]
        public void TestGetNumberOfActiveCompaniesOffertsQueryHandler_QueryCGIDProvided_ShouldUse_QueryCGID()
        {
            // Arrange
            var query = new GetNumberOfActiveCompaniesOffertsQuery
            {
                CGID = companyGid,
                Date = date
            };

            var handler = new GetNumberOfActiveCompaniesOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Datasets.Data.First(), Is.EqualTo(1));
        }

        [Test]
        public void TestGetNumberOfActiveCompaniesOffertsQueryHandler_CompanyNotFound_ShouldThrow_CompanyNotFoundExceptions()
        {
            // Arrange
            companies.Clear();

            var query = new GetNumberOfActiveCompaniesOffertsQuery
            {
                CGID = companyGid,
                Date = date
            };

            var handler = new GetNumberOfActiveCompaniesOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            // Assert
            Assert.Throws<CompanyNotFoundExceptions>(() => handler.Handle(query));
        }

        [Test]
        public void TestGetNumberOfActiveCompaniesOffertsQueryHandler_ShouldCount_OnlyOffersFromGivenDate()
        {
            // Arrange
            var query = new GetNumberOfActiveCompaniesOffertsQuery
            {
                CGID = companyGid,
                Date = date
            };

            var handler = new GetNumberOfActiveCompaniesOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Datasets.Data.Single(), Is.EqualTo(1));
        }

        [Test]
        public void TestGetNumberOfActiveCompaniesOffertsQueryHandler_ShouldReturn_CorrectChartStructure()
        {
            // Arrange
            var query = new GetNumberOfActiveCompaniesOffertsQuery
            {
                CGID = companyGid,
                Date = date
            };

            var handler = new GetNumberOfActiveCompaniesOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Labels, Has.Count.EqualTo(1));
            Assert.That(result.Datasets, Is.Not.Null);
            Assert.That(result.Datasets.Data, Has.Count.EqualTo(1));
        }

        [Test]
        public void TestGetNumberOfActiveCompaniesOffertsQueryHandler_ShouldContain_CompanyNameInLabel()
        {
            // Arrange
            var query = new GetNumberOfActiveCompaniesOffertsQuery
            {
                CGID = companyGid,
                Date = date
            };

            var handler = new GetNumberOfActiveCompaniesOffertsQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            StringAssert.Contains("Test Company", result.Datasets.Label);
        }
    }
}
