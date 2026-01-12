using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Queries;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Handlers;
using JuniorBoardIT.Core.Exceptions.JobOffers;
using JuniorBoardIT.Core.Models.Enums.JobOffers;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;
using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;

namespace JuniorBoardIT.UnitTests.CQRS.QueryHandler.JobOffers
{
    [TestFixture]
    public class TestGetJobOfferQueryHandler
    {
        private Mock<IDataBaseContext> context;

        private Mock<IMapper>? mapper;

        private List<Core.Entities.JobOffers> jobOffers;

        private List<Core.Entities.Companies> companies;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();

            mapper = new Mock<IMapper>();

            jobOffers = new List<Core.Entities.JobOffers>()
            {
                new Core.Entities.JobOffers()
                {
                    JOGID = new Guid("27dd879c-ee2f-11db-8314-0800200c9a66"),
                },
                new Core.Entities.JobOffers()
                {
                    JOGID = new Guid("28dd879c-ee2f-11db-8314-0800200c9a66"),
                    JOCGID = new Guid("29dd879c-ee2f-11db-8314-0800200c9a66"),
                }
            };

            companies = new List<Core.Entities.Companies>()
            {
                new Core.Entities.Companies()
                {
                    CGID = new Guid("29dd879c-ee2f-11db-8314-0800200c9a66")
                }
            };

            context.Setup(x => x.JobOffers).Returns(jobOffers.AsQueryable());

            context.Setup(x => x.Companies).Returns(companies.AsQueryable());

            mapper.Setup(x => x.Map<Core.Entities.JobOffers, JobOfferViewModel>(It.IsAny<Core.Entities.JobOffers>()))
              .Returns<Core.Entities.JobOffers>(jobOffer => new JobOfferViewModel
              {
                  JOGID = jobOffer.JOGID,
                  JOCGID = jobOffer.JOCGID,
              });

            mapper.Setup(x => x.Map<Core.Entities.Companies, CompanyViewModel>(It.IsAny<Core.Entities.Companies>()))
              .Returns<Core.Entities.Companies>(company => new CompanyViewModel
              {
                  CGID = company.CGID,
              });
        }

        [Test]
        public void TestGetJobOfferQueryHandler_JobOfferIsNotFound_ShouldThrow_JobOfferNotFoundExceptions()
        {
            // Arrange
            var command = new GetJobOfferQuery() { JOGID = Guid.NewGuid() };
            var handler = new GetJobOfferQueryHandler(context.Object, mapper.Object);

            // Act
            // Assert
            Assert.Throws<JobOfferNotFoundExceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestGetJobOfferQueryHandler_JobOfferIsFound_JobOfferWithout_Company_ShouldReturn_JobOffer_WithoutCompany()
        {
            // Arrange
            var command = new GetJobOfferQuery() { JOGID = new Guid("27dd879c-ee2f-11db-8314-0800200c9a66") };
            var handler = new GetJobOfferQueryHandler(context.Object, mapper.Object);

            // Act
            var result = handler.Handle(command);

            // Assert
            ClassicAssert.AreEqual(2, jobOffers.Count);
            ClassicAssert.AreEqual(result.JobOffer.JOGID, jobOffers[0].JOGID);
            ClassicAssert.AreEqual(result.Company.CGID, Guid.Empty);
        }

        [Test]
        public void TestGetJobOfferQueryHandler_JobOfferIsFound_JobOfferWith_Company_ShouldReturn_JobOffer_WithCompany()
        {
            // Arrange
            var command = new GetJobOfferQuery() { JOGID = new Guid("28dd879c-ee2f-11db-8314-0800200c9a66") };
            var handler = new GetJobOfferQueryHandler(context.Object, mapper.Object);

            // Act
            var result = handler.Handle(command);

            // Assert
            ClassicAssert.AreEqual(2, jobOffers.Count);
            ClassicAssert.AreEqual(result.JobOffer.JOGID, jobOffers[1].JOGID);
            ClassicAssert.AreEqual(result.Company.CGID, companies[0].CGID);
        }
    }
}
