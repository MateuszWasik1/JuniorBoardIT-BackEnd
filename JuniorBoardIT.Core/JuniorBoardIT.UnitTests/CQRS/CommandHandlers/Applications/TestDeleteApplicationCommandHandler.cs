using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.Applications.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Applications.Handlers;
using JuniorBoardIT.Core.Exceptions.Applications;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace JuniorBoardIT.UnitTests.CQRS.CommandHandlers.Applications
{
    [TestFixture]
    public class TestDeleteApplicationCommandHandler
    {
        private Mock<IDataBaseContext> context;

        private List<Core.Entities.Applications> applications;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();

            applications = new List<Core.Entities.Applications>()
            {
                new Core.Entities.Applications()
                {
                    AID = 1,
                    AGID = new Guid("30dd879c-ee2f-11db-8314-0800200c9a66"),
                },
                new Core.Entities.Applications()
                {
                    AID = 2,
                    AGID = new Guid("31dd879c-ee2f-11db-8314-0800200c9a66"),
                },
            };

            context.Setup(x => x.Applications).Returns(applications.AsQueryable());

            context.Setup(x => x.DeleteApplication(It.IsAny<Core.Entities.Applications>())).Callback<Core.Entities.Applications>(application => applications.Remove(application));
        }

        [Test]
        public void TestDeleteApplicationCommandHandler_ApplicationIsNotFound_ShouldThrow_ApplicationNotFoundExceptions()
        {
            //Arrange 
            var command = new DeleteApplicationCommand() { AGID = new Guid("32dd879c-ee2f-11db-8314-0800200c9a66") };
            var handler = new DeleteApplicationCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<ApplicationNotFoundExceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestDeleteApplicationCommandHandler_ApplicationIsFound_ShouldDelete_GivenApplication()
        {
            //Arrange 
            var command = new DeleteApplicationCommand() { AGID = new Guid("31dd879c-ee2f-11db-8314-0800200c9a66") };
            var handler = new DeleteApplicationCommandHandler(context.Object);

            //Act
            handler.Handle(command);

            //Assert
            ClassicAssert.AreEqual(1, applications.Count);
            ClassicAssert.AreEqual(1, applications[0].AID);
            ClassicAssert.AreEqual(new Guid("30dd879c-ee2f-11db-8314-0800200c9a66"), applications[0].AGID);
        }
    }
}
