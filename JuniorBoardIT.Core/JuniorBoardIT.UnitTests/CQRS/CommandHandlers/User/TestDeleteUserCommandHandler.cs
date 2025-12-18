using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.User.Commands;
using JuniorBoardIT.Core.CQRS.Resources.User.Handlers;
using JuniorBoardIT.Core.Exceptions;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace JuniorBoardIT.UnitTests.CQRS.CommandHandlers.User
{
    [TestFixture]
    public class TestDeleteUserCommandHandler
    {
        private Mock<IDataBaseContext>? context;

        private List<Core.Entities.User>? users;
        private List<Core.Entities.JobOffers>? jobOffers;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();

            users = new List<Core.Entities.User>
            {
                new Core.Entities.User()
                {
                    UID = 1,
                    UGID = new Guid("98dacc1d-7bee-4635-9c4c-9404a4af80dd"),
                },
                new Core.Entities.User()
                {
                    UID = 2,
                    UGID = new Guid("99dacc1d-7bee-4635-9c4c-9404a4af80dd"),
                },
            };

            jobOffers = new List<Core.Entities.JobOffers>()
            {
                new Core.Entities.JobOffers()
                {
                    JOID = 1,
                    JORGID = users[0].UGID
                }
            };

            context.Setup(x => x.AllUsers).Returns(users.AsQueryable());
            context.Setup(x => x.JobOffers).Returns(jobOffers.AsQueryable());

            context.Setup(x => x.DeleteUser(It.IsAny<Core.Entities.User>())).Callback<Core.Entities.User>(user => users.Remove(user));
        }

        [Test]
        public void TestDeleteUserCommandHandler_UserNotFound_ShouldThrow_UserNotFoundExceptions()
        {
            //Arrange
            var query = new DeleteUserCommand() { UGID = new Guid("69dacc1d-7bee-4635-9c4c-9404a4af80dd") };
            var handler = new DeleteUserCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<UserNotFoundExceptions>(() => handler.Handle(query));
        }

        [Test]
        public void TestDeleteUserCommandHandler_UserNotFound_ShouldThrow_UserHasPublishedJobOffersExceptions()
        {
            //Arrange
            var query = new DeleteUserCommand() { UGID = users[0].UGID };
            var handler = new DeleteUserCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<UserHasPublishedJobOffersExceptions>(() => handler.Handle(query));
        }

        [Test]
        public void TestDeleteUserCommandHandler_UserFound_ShouldDelete_User()
        {
            //Arrange
            var query = new DeleteUserCommand() { UGID = users[1].UGID };
            var handler = new DeleteUserCommandHandler(context.Object);

            //Act
            handler.Handle(query);

            //Assert
            ClassicAssert.AreEqual(1, users.Count);

            context.Verify(x => x.DeleteUser(It.IsAny<Core.Entities.User>()), Times.Once);
        }
    }
}
