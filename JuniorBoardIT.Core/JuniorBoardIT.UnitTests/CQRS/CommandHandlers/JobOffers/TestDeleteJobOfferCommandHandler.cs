using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Commands;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Handlers;
using JuniorBoardIT.Core.Exceptions.JobOffers;
using JuniorBoardIT.Core.Models.Enums.JobOffers;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace JuniorBoardIT.UnitTests.CQRS.CommandHandlers.JobOffers
{
    [TestFixture]
    public class TestDeleteJobOfferCommandHandler
    {
        private Mock<IDataBaseContext> context;

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

            context.Setup(x => x.DeleteJobOffer(It.IsAny<Core.Entities.JobOffers>())).Callback<Core.Entities.JobOffers>(jobOffer => jobOffers.Remove(jobOffer));
        }

        [Test]
        public void TestDeleteJobOfferCommandHandler_JobOfferIsNotFound_ShouldThrow_JobOfferNotFoundExceptions()
        {
            // Arrange
            var command = new DeleteJobOfferCommand() { JOGID = Guid.NewGuid() };
            var handler = new DeleteJobOfferCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<JobOfferNotFoundExceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestDeleteJobOfferCommandHandler_JobOfferIsFound_ShouldDelete_JobOffer()
        {
            // Arrange
            var command = new DeleteJobOfferCommand() { JOGID = new Guid("28dd879c-ee2f-11db-8314-0800200c9a66") };
            var handler = new DeleteJobOfferCommandHandler(context.Object);

            // Act
            handler.Handle(command);

            // Assert
            ClassicAssert.AreEqual(0, jobOffers.Count);

            context.Verify(x => x.DeleteJobOffer(It.IsAny<Core.Entities.JobOffers>()), Times.Once);
            context.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
