using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Queries;
using JuniorBoardIT.Core.Exceptions.JobOffers;
using JuniorBoardIT.Core.Exceptions.Reports;
using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;
using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;
using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;
using Moq;
using NUnit.Framework;

namespace JuniorBoardIT.UnitTests.CQRS.QueryHandler.Reports
{
    [TestFixture]
    public class TestGetReportQueryHandler
    {
        private Mock<IDataBaseContext> context;
        private Mock<IMapper> mapper;

        private List<Core.Entities.Reports> reports;
        private List<Core.Entities.JobOffers> jobOffers;
        private List<Core.Entities.Companies> companies;

        private Guid reportGid;
        private Guid jobOfferGid;
        private Guid companyGid;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();
            mapper = new Mock<IMapper>();

            reportGid = Guid.NewGuid();
            jobOfferGid = Guid.NewGuid();
            companyGid = Guid.NewGuid();

            reports = new List<Core.Entities.Reports>
            {
                new Core.Entities.Reports
                {
                    RGID = reportGid,
                    RJOGID = jobOfferGid
                }
            };

            jobOffers = new List<Core.Entities.JobOffers>
            {
                new Core.Entities.JobOffers
                {
                    JOGID = jobOfferGid,
                    JOCGID = companyGid
                }
            };

            companies = new List<Core.Entities.Companies>
            {
                new Core.Entities.Companies
                {
                    CGID = companyGid
                }
            };

            context.Setup(x => x.Reports).Returns(reports.AsQueryable());
            context.Setup(x => x.JobOffers).Returns(jobOffers.AsQueryable());
            context.Setup(x => x.Companies).Returns(companies.AsQueryable());

            mapper.Setup(x => x.Map<Core.Entities.Reports, ReportsViewModel>(It.IsAny<Core.Entities.Reports>())).Returns(new ReportsViewModel());

            mapper.Setup(x => x.Map<Core.Entities.JobOffers, JobOfferViewModel>(It.IsAny<Core.Entities.JobOffers>())).Returns(new JobOfferViewModel { JOCGID = companyGid });

            mapper.Setup(x => x.Map<Core.Entities.Companies, CompanyViewModel>(It.IsAny<Core.Entities.Companies>())).Returns(new CompanyViewModel());
        }

        [Test]
        public void TestGetReportQueryHandler_ReportNotFound_ShouldThrow_ReportNotFoundExceptions()
        {
            // Arrange
            var query = new GetReportQuery { RGID = Guid.NewGuid() };
            var handler = new GetReportQueryHandler(context.Object, mapper.Object);

            // Act
            // Assert
            Assert.Throws<ReportNotFoundExceptions>(() => handler.Handle(query));
        }

        [Test]
        public void TestGetReportQueryHandler_JobOfferNotFound_ShouldThrow_JobOfferNotFoundExceptions()
        {
            // Arrange
            jobOffers.Clear();

            var query = new GetReportQuery { RGID = reportGid };
            var handler = new GetReportQueryHandler(context.Object, mapper.Object);

            // Act
            // Assert
            Assert.Throws<JobOfferNotFoundExceptions>(() => handler.Handle(query));
        }

        [Test]
        public void TestGetReportQueryHandler_CompanyExists_ShouldReturn_FullModel()
        {
            // Arrange
            var query = new GetReportQuery { RGID = reportGid };
            var handler = new GetReportQueryHandler(context.Object, mapper.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ReportModel, Is.Not.Null);
            Assert.That(result.JobOfferModel, Is.Not.Null);
            Assert.That(result.CompanyModel, Is.Not.Null);
        }

        [Test]
        public void TestGetReportQueryHandler_CompanyNotFound_ShouldReturn_EmptyCompanyModel()
        {
            // Arrange
            companies.Clear();

            var query = new GetReportQuery { RGID = reportGid };
            var handler = new GetReportQueryHandler(context.Object, mapper.Object);

            // Act
            var result = handler.Handle(query);

            // Assert
            Assert.That(result.CompanyModel, Is.Not.Null);
        }

        [Test]
        public void TestGetReportQueryHandler_ShouldMap_AllEntities()
        {
            // Arrange
            var query = new GetReportQuery { RGID = reportGid };
            var handler = new GetReportQueryHandler(context.Object, mapper.Object);

            // Act
            handler.Handle(query);

            // Assert
            mapper.Verify(x => x.Map<Core.Entities.Reports, ReportsViewModel>(It.IsAny<Core.Entities.Reports>()), Times.Once);
            mapper.Verify(x => x.Map<Core.Entities.JobOffers, JobOfferViewModel>(It.IsAny<Core.Entities.JobOffers>()), Times.Once);
            mapper.Verify(x => x.Map<Core.Entities.Companies, CompanyViewModel>(It.IsAny<Core.Entities.Companies>()), Times.Once);
        }
    }
}
