using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WouterFennis.ChatApp.Controllers;
using WouterFennis.ChatApp.DAL.Repositories;
using WouterFennis.ChatApp.Domain;

namespace WouterFennis.ChatApp.Test
{
    [TestClass]
    public class ChatRoomControllerTest
    {
        [TestMethod]
        public void GetReturnsIEnumerableOfTypeChatRoom()
        {
            // Arrange
            var chatRoomRepositoryMock = new Mock<IRepository<ChatRoom, long>>(MockBehavior.Strict);
            chatRoomRepositoryMock.Setup(repository => repository.FindAll()).Returns(new List<ChatRoom>());
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomRepositoryMock.Object);

            // Act
            var result = chatRoomController.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<ChatRoom>));
        }

        [TestMethod]
        public void PostReturnsActionResult()
        {
            // Arrange
            var chatRoomRepositoryMock = new Mock<IRepository<ChatRoom, long>>(MockBehavior.Strict);
            chatRoomRepositoryMock.Setup(repository => repository.Insert(It.IsAny<ChatRoom>())).Returns(1);
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomRepositoryMock.Object);

            ChatRoom chatRoom = new ChatRoom("Room");

            // Act
            SimulateValidation(chatRoom, chatRoomController);
            var result = chatRoomController.Post(chatRoom);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void PostReturnsStatusCodeResult()
        {
            // Arrange
            var chatRoomRepositoryMock = new Mock<IRepository<ChatRoom, long>>(MockBehavior.Strict);
            chatRoomRepositoryMock.Setup(repository => repository.Insert(It.IsAny<ChatRoom>())).Returns(1);
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomRepositoryMock.Object);

            ChatRoom chatRoom = new ChatRoom("Room");

            // Act
            SimulateValidation(chatRoom, chatRoomController);
            var result = chatRoomController.Post(chatRoom);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
        }

        [TestMethod]
        public void PostWithValidChatRoomReturnsStatusCode200()
        {
            // Arrange
            var chatRoomRepositoryMock = new Mock<IRepository<ChatRoom, long>>(MockBehavior.Strict);
            chatRoomRepositoryMock.Setup(repository => repository.Insert(It.IsAny<ChatRoom>())).Returns(1);
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomRepositoryMock.Object);

            ChatRoom chatRoom = new ChatRoom("Room");
            int expectedStatusCode = 200;
            int expectedErrorCount = 0;

            // Act
            SimulateValidation(chatRoom, chatRoomController);
            var result = chatRoomController.Post(chatRoom);

            // Assert
            Assert.IsNotNull(result);
            StatusCodeResult statusCodeResult = (StatusCodeResult)result;
            Assert.AreEqual(expectedStatusCode, statusCodeResult.StatusCode);

            var modelState = chatRoomController.ModelState;
            Assert.AreEqual(expectedErrorCount, modelState.ErrorCount);
        }

        [TestMethod]
        public void PostWithNameNullReturnsStatusCode400()
        {
            // Arrange
            var chatRoomRepositoryMock = new Mock<IRepository<ChatRoom, long>>(MockBehavior.Strict);
            chatRoomRepositoryMock.Setup(repository => repository.Insert(It.IsAny<ChatRoom>())).Returns(1);
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomRepositoryMock.Object);

            ChatRoom chatRoom = new ChatRoom(null);
            int expectedStatusCode = 400;
            int expectedErrorCount = 1;

            // Act
            SimulateValidation(chatRoom, chatRoomController);
            var result = chatRoomController.Post(chatRoom);

            // Assert
            Assert.IsNotNull(result);
            StatusCodeResult statusCodeResult = (StatusCodeResult)result;
            Assert.AreEqual(expectedStatusCode, statusCodeResult.StatusCode);

            var modelState = chatRoomController.ModelState;
            Assert.AreEqual(expectedErrorCount, modelState.ErrorCount);
            Assert.AreEqual(modelState.First().Key, "Name");
        }

        [TestMethod]
        public void PostWithNameEmptyReturnsStatusCode400()
        {
            // Arrange
            var chatRoomRepositoryMock = new Mock<IRepository<ChatRoom, long>>(MockBehavior.Strict);
            chatRoomRepositoryMock.Setup(repository => repository.Insert(It.IsAny<ChatRoom>())).Returns(1);
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomRepositoryMock.Object);

            ChatRoom chatRoom = new ChatRoom("");
            int expectedStatusCode = 400;
            int expectedErrorCount = 1;

            // Act
            SimulateValidation(chatRoom, chatRoomController);
            var result = chatRoomController.Post(chatRoom);

            // Assert
            Assert.IsNotNull(result);
            StatusCodeResult statusCodeResult = (StatusCodeResult)result;
            Assert.AreEqual(expectedStatusCode, statusCodeResult.StatusCode);

            var modelState = chatRoomController.ModelState;
            Assert.AreEqual(expectedErrorCount, modelState.ErrorCount);
            Assert.AreEqual(modelState.First().Key, "Name");
        }

        [TestMethod]
        public void PostWithValidChatRoomCallsRepository()
        {
            // Arrange
            var chatRoomRepositoryMock = new Mock<IRepository<ChatRoom, long>>(MockBehavior.Strict);
            chatRoomRepositoryMock.Setup(repository => repository.Insert(It.IsAny<ChatRoom>())).Returns(1);
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomRepositoryMock.Object);

            ChatRoom chatRoom = new ChatRoom("Room");

            // Act
            SimulateValidation(chatRoom, chatRoomController);
            chatRoomController.Post(chatRoom);

            // Assert
            chatRoomRepositoryMock.Verify(repository => repository.Insert(It.IsAny<ChatRoom>()), Times.Once);
        }

        private void SimulateValidation(object model, ChatRoomController chatRoomController)
        {
            // mimic the behaviour of the model binder which is responsible for Validating the Model
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                chatRoomController.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }
        }
    }
}
