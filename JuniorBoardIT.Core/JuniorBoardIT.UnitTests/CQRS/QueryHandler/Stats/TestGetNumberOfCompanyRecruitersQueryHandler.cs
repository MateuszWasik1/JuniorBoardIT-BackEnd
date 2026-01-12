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
    public class TestGetNumberOfCompanyRecruitersQueryHandler
    {
        private Mock<IDataBaseContext> context;
        private Mock<IUserContext> userContext;

        private List<Core.Entities.User> users;
        private List<Core.Entities.User> allUsers;
        private List<Core.Entities.Companies> companies;

        private Guid companyGid;
        private Guid userGid;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();
            userContext = new Mock<IUserContext>();

            companyGid = Guid.NewGuid();
            userGid = Guid.NewGuid();

            users = new List<Core.Entities.User>
            {
                new Core.Entities.User
                {
                    UGID = userGid,
                    UCompanyGID = companyGid
                }
            };

            allUsers = new List<Core.Entities.User>
            {
                new Core.Entities.User { UCompanyGID = companyGid },
                new Core.Entities.User { UCompanyGID = companyGid },
                new Core.Entities.User { UCompanyGID = Guid.NewGuid() }
            };

            companies = new List<Core.Entities.Companies>
            {
                new Core.Entities.Companies
                {
                    CGID = companyGid,
                    CName = "Test Company"
                }
            };

            context.Setup(x => x.User).Returns(users.AsQueryable());
            context.Setup(x => x.AllUsers).Returns(allUsers.AsQueryable());
            context.Setup(x => x.Companies).Returns(companies.AsQueryable());

            userContext.Setup(x => x.UGID).Returns(userGid.ToString());
        }

        [Test]
        public void TestGetNumberOfCompanyRecruitersQueryHandler_QueryCGIDEmpty_ShouldUse_UsersCompanyGID_And_ReturnRecruitersCount()
        {
            // Arrange
            var query = new GetNumberOfCompanyRecruitersQuery { CGID = Guid.Empty };

            var handler = new GetNumberOfCompanyRecruitersQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Datasets.Data.Single(), Is.EqualTo(2));
        }

        [Test]
        public void TestGetNumberOfCompanyRecruitersQueryHandler_QueryCGIDProvided_ShouldUse_QueryCGID()
        {
            // Arrange
            var query = new GetNumberOfCompanyRecruitersQuery { CGID = companyGid };
            var handler = new GetNumberOfCompanyRecruitersQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Datasets.Data.Single(), Is.EqualTo(2));
        }

        [Test]
        public void TestGetNumberOfCompanyRecruitersQueryHandler_CompanyNotFound_ShouldThrow_CompanyNotFoundExceptions()
        {
            // Arrange
            companies.Clear();

            var query = new GetNumberOfCompanyRecruitersQuery { CGID = companyGid };
            var handler = new GetNumberOfCompanyRecruitersQueryHandler(context.Object, userContext.Object);

            // Act
            // Assert
            Assert.Throws<CompanyNotFoundExceptions>(() => handler.Handle(query));
        }

        [Test]
        public void TestGetNumberOfCompanyRecruitersQueryHandler_ShouldReturn_CorrectChartStructure()
        {
            // Arrange
            var query = new GetNumberOfCompanyRecruitersQuery { CGID = companyGid };
            var handler = new GetNumberOfCompanyRecruitersQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Labels, Is.Not.Null);
            Assert.That(result.Labels.Count, Is.EqualTo(1));
            Assert.That(result.Datasets, Is.Not.Null);
            Assert.That(result.Datasets.Data, Is.Not.Null);
            Assert.That(result.Datasets.Data.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestGetNumberOfCompanyRecruitersQueryHandler_ShouldContain_CompanyNameInDatasetLabel()
        {
            // Arrange
            var query = new GetNumberOfCompanyRecruitersQuery { CGID = companyGid };
            var handler = new GetNumberOfCompanyRecruitersQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            StringAssert.Contains("Test Company", result.Datasets.Label);
        }

        [Test]
        public void TestGetNumberOfCompanyRecruitersQueryHandler_NoRecruiters_ShouldReturn_Zero()
        {
            // Arrange
            allUsers.Clear();

            var query = new GetNumberOfCompanyRecruitersQuery { CGID = companyGid };
            var handler = new GetNumberOfCompanyRecruitersQueryHandler(context.Object, userContext.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.Datasets.Data.Single(), Is.EqualTo(0));
        }
    }
}
