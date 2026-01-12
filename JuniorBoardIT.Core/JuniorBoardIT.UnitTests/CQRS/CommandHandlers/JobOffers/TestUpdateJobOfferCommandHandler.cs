using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Commands;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Handlers;
using JuniorBoardIT.Core.Exceptions.JobOffers;
using JuniorBoardIT.Core.Models.Enums.JobOffers;
using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace JuniorBoardIT.UnitTests.CQRS.CommandHandlers.JobOffers
{
    [TestFixture]
    public class TestUpdateJobOfferCommandHandler
    {
        private Mock<IDataBaseContext> context;

        private JobOfferViewModel jobOfferViewModel()
        {
            return new JobOfferViewModel()
            {
                JOGID = new Guid("28dd879c-ee2f-11db-8314-0800200c9a66"),
                JOCGID = new Guid("30dd879c-ee2f-11db-8314-0800200c9a66"),
                JOTitle =  "Title2",
                JOCompanyName = "CompanyName2",
                JOLocationType = LocationEnum.Remote,
                JOOfficeLocation = "OfficeLocation2",
                JOEmploymentType = EmploymentTypeEnum.UZ,
                JOExpirenceLevel = ExpirenceEnum.Junior,
                JOExpirenceYears = 2,
                JOCategory = CategoryEnum.BackEnd,
                JOSalaryMin = 12000,
                JOSalaryMax = 18000,
                JOCurrency = CurrencyEnum.USD,
                JOSalaryType = SalaryEnum.Weekly,
                JODescription = "Description2",
                JORequirements = "Requirements2",
                JOBenefits = "Benefits2",
                JOPostedAt = new DateTime(2026, 1, 13, 20, 30, 0),
                JOExpiresAt = new DateTime(2026, 1, 13, 21, 30, 0),
                JOStatus = StatusEnum.Draft,
            };
        }

        private List<Core.Entities.JobOffers> jobOffers;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();

            jobOffers = new List<Core.Entities.JobOffers>()
            {
                new Core.Entities.JobOffers()
                {
                    JOGID = new Guid("28dd879c-ee2f-11db-8314-0800200c9a66"),
                    JOCGID = new Guid("29dd879c-ee2f-11db-8314-0800200c9a66"),
                    JOTitle =  "Title",
                    JOCompanyName = "CompanyName",
                    JOLocationType = LocationEnum.Remote,
                    JOOfficeLocation = "OfficeLocation",
                    JOEmploymentType = EmploymentTypeEnum.UoP,
                    JOExpirenceLevel = ExpirenceEnum.Senior,
                    JOExpirenceYears = 1,
                    JOCategory = CategoryEnum.FrontEnd,
                    JOSalaryMin = 10000,
                    JOSalaryMax = 15000,
                    JOCurrency = CurrencyEnum.PLN,
                    JOSalaryType = SalaryEnum.Monthly,
                    JODescription = "Description",
                    JORequirements = "Requirements",
                    JOBenefits = "Benefits",
                    JOCreatedAt = DateTime.Now,
                    JOPostedAt = new DateTime(2026, 1, 12, 20, 30, 0),
                    JOExpiresAt = new DateTime(2026, 1, 12, 21, 30, 0),
                    JOStatus = StatusEnum.Active,
                }
            };

            context.Setup(x => x.JobOffers).Returns(jobOffers.AsQueryable());

            context.Setup(x => x.CreateOrUpdate(It.IsAny<Core.Entities.JobOffers>())).Callback<Core.Entities.JobOffers>(jobOffers.Add);
        }

        [Test]
        public void TestUpdateJobOfferCommandHandler_JobOfferNotFound_ShouldThrow_JobOfferNotFoundExceptions()
        {
            // Arrange
            var model = jobOfferViewModel();
            model.JOGID = Guid.NewGuid();

            var command = new UpdateJobOfferCommand() { Model = model };
            var handler = new UpdateJobOfferCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<JobOfferNotFoundExceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateJobOfferCommandHandler_ModelIsValid_ShouldUpdate_JobOffer()
        {
            // Arrange
            var model = jobOfferViewModel();

            var command = new UpdateJobOfferCommand() { Model = jobOfferViewModel() };
            var handler = new UpdateJobOfferCommandHandler(context.Object);

            // Act
            handler.Handle(command);

            // Assert
            ClassicAssert.AreEqual(model.JOTitle, jobOffers[0].JOTitle);
            ClassicAssert.AreEqual(model.JOCompanyName, jobOffers[0].JOCompanyName);
            ClassicAssert.AreEqual(model.JOLocationType, jobOffers[0].JOLocationType);
            ClassicAssert.AreEqual(model.JOOfficeLocation, jobOffers[0].JOOfficeLocation);
            ClassicAssert.AreEqual(model.JOEmploymentType, jobOffers[0].JOEmploymentType);
            ClassicAssert.AreEqual(model.JOExpirenceLevel, jobOffers[0].JOExpirenceLevel);
            ClassicAssert.AreEqual(model.JOExpirenceYears, jobOffers[0].JOExpirenceYears);
            ClassicAssert.AreEqual(model.JOCategory, jobOffers[0].JOCategory);
            ClassicAssert.AreEqual(model.JOSalaryMin, jobOffers[0].JOSalaryMin);
            ClassicAssert.AreEqual(model.JOSalaryMax, jobOffers[0].JOSalaryMax);
            ClassicAssert.AreEqual(model.JOCurrency, jobOffers[0].JOCurrency);
            ClassicAssert.AreEqual(model.JOSalaryType, jobOffers[0].JOSalaryType);
            ClassicAssert.AreEqual(model.JODescription, jobOffers[0].JODescription);
            ClassicAssert.AreEqual(model.JORequirements, jobOffers[0].JORequirements);
            ClassicAssert.AreEqual(model.JOPostedAt, jobOffers[0].JOPostedAt);
            ClassicAssert.AreEqual(model.JOExpiresAt, jobOffers[0].JOExpiresAt);
            ClassicAssert.AreEqual(model.JOStatus, jobOffers[0].JOStatus);

            context.Verify(x => x.CreateOrUpdate(It.IsAny<Core.Entities.JobOffers>()), Times.Once);
            context.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
