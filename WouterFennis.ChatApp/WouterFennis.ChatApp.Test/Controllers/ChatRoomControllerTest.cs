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
using WouterFennis.ChatApp.Managers;

namespace WouterFennis.ChatApp.Test
{
    [TestClass]
    public class ChatRoomControllerTest
    {
        [TestMethod]
        public void GetAllChatRoomsReturnsIEnumerableOfChatRoom()
        {
            // Arrange
            var chatRoomManagerMock = new Mock<IChatRoomManager>(MockBehavior.Strict);
            chatRoomManagerMock.Setup(manager => manager.GetAllChatRooms()).Returns(new List<ChatRoom>());
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomManagerMock.Object);

            // Act
            var result = chatRoomController.GetAllChatRooms();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<ChatRoom>));
        }

        [TestMethod]
        public void GetAllChatRoomsCallChatRoomManager()
        {
            // Arrange
            var chatRoomManagerMock = new Mock<IChatRoomManager>(MockBehavior.Strict);
            chatRoomManagerMock.Setup(manager => manager.GetAllChatRooms()).Returns(new List<ChatRoom>());
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomManagerMock.Object);

            // Act
            var result = chatRoomController.GetAllChatRooms();

            // Assert
            chatRoomManagerMock.Verify(manager => manager.GetAllChatRooms(), Times.Once);
        }
        
        [TestMethod]
        public void GetChatRoomByIdReturnsIActionResult()
        {
            // Arrange
            var chatRoomManagerMock = new Mock<IChatRoomManager>(MockBehavior.Strict);
            chatRoomManagerMock.Setup(manager => manager.FindChatRoomById(It.IsAny<long>())).Returns(new ChatRoom());
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomManagerMock.Object);
            long chatRoomId = 1;
            // Act
            var result = chatRoomController.GetChatRoomById(chatRoomId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IActionResult));
        }

        [TestMethod]
        public void GetChatRoomByIdWithValidIdReturnsObjectResult()
        {
            // Arrange
            var chatRoomManagerMock = new Mock<IChatRoomManager>(MockBehavior.Strict);
            chatRoomManagerMock.Setup(manager => manager.FindChatRoomById(It.IsAny<long>())).Returns(new ChatRoom());
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomManagerMock.Object);
            long chatRoomId = 1;
            // Act
            var result = chatRoomController.GetChatRoomById(chatRoomId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            ObjectResult objectResult = (ObjectResult)result;
            Assert.IsInstanceOfType(objectResult.Value, typeof(ChatRoom));
        }

        [TestMethod]
        public void GetChatRoomByIdWithInvalidIdReturnsNotFoundResult()
        {
            // Arrange
            var chatRoomManagerMock = new Mock<IChatRoomManager>(MockBehavior.Strict);
            chatRoomManagerMock.Setup(manager => manager.FindChatRoomById(It.IsAny<long>())).Throws(new KeyNotFoundException());
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomManagerMock.Object);
            long chatRoomId = 1;
            // Act
            var result = chatRoomController.GetChatRoomById(chatRoomId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        
        [TestMethod]
        public void AddMessageToChatRoomReturnsIActionResult()
        {
            // Arrange
            var chatRoomManagerMock = new Mock<IChatRoomManager>(MockBehavior.Strict);
            chatRoomManagerMock.Setup(manager => manager.AddMessageToChatRoom(It.IsAny<long>(), It.IsAny<Message>()));
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomManagerMock.Object);
            long chatRoomId = 1;
            Message message = new Message("Carla Red", "Hello World");
            // Act
            var result = chatRoomController.AddMessageToChatRoom(chatRoomId, message);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IActionResult));
        }

        [TestMethod]
        public void AddMessageToChatRoomReturnsStatusCodeResult()
        {
            // Arrange
            var chatRoomManagerMock = new Mock<IChatRoomManager>(MockBehavior.Strict);
            chatRoomManagerMock.Setup(manager => manager.AddMessageToChatRoom(It.IsAny<long>(), It.IsAny<Message>()));
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomManagerMock.Object);
            long chatRoomId = 1;
            Message message = new Message("Carla Red", "Hello World");
            // Act
            var result = chatRoomController.AddMessageToChatRoom(chatRoomId, message);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
        }

        [TestMethod]
        public void AddMessageToChatRoomWithValidInputReturnsStatusCode200()
        {
            // Arrange
            var chatRoomManagerMock = new Mock<IChatRoomManager>(MockBehavior.Strict);
            chatRoomManagerMock.Setup(manager => manager.AddMessageToChatRoom(It.IsAny<long>(), It.IsAny<Message>()));
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomManagerMock.Object);
            long chatRoomId = 1;
            Message message = new Message("Carla Red", "Hello World");
            int expectedStatusCode = 200;
            int expectedErrorCount = 0;

            // Act
            SimulateValidation(message, chatRoomController);
            var result = chatRoomController.AddMessageToChatRoom(chatRoomId, message);

            // Assert
            Assert.IsNotNull(result);
            StatusCodeResult statusCodeResult = (StatusCodeResult)result;
            Assert.AreEqual(expectedStatusCode, statusCodeResult.StatusCode);

            var modelState = chatRoomController.ModelState;
            Assert.AreEqual(expectedErrorCount, modelState.ErrorCount);
        }

        [TestMethod]
        public void AddMessageToChatRoomWithSenderNullReturnsStatusCode400()
        {
            // Arrange
            var chatRoomManagerMock = new Mock<IChatRoomManager>(MockBehavior.Strict);
            chatRoomManagerMock.Setup(manager => manager.AddMessageToChatRoom(It.IsAny<long>(), It.IsAny<Message>()));
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomManagerMock.Object);
            long chatRoomId = 1;
            Message message = new Message(null, "Hello World");
            int expectedStatusCode = 400;
            int expectedErrorCount = 1;

            // Act
            SimulateValidation(message, chatRoomController);
            var result = chatRoomController.AddMessageToChatRoom(chatRoomId, message);

            // Assert
            Assert.IsNotNull(result);
            StatusCodeResult statusCodeResult = (StatusCodeResult)result;
            Assert.AreEqual(expectedStatusCode, statusCodeResult.StatusCode);

            var modelState = chatRoomController.ModelState;
            Assert.AreEqual(expectedErrorCount, modelState.ErrorCount);
        }

        [TestMethod]
        public void AddMessageToChatRoomWithMessageNullReturnsStatusCode400()
        {
            // Arrange
            var chatRoomManagerMock = new Mock<IChatRoomManager>(MockBehavior.Strict);
            chatRoomManagerMock.Setup(manager => manager.AddMessageToChatRoom(It.IsAny<long>(), It.IsAny<Message>()));
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomManagerMock.Object);
            long chatRoomId = 1;
            Message message = new Message("Carla Red", null);
            int expectedStatusCode = 400;
            int expectedErrorCount = 1;

            // Act
            SimulateValidation(message, chatRoomController);
            var result = chatRoomController.AddMessageToChatRoom(chatRoomId, message);

            // Assert
            Assert.IsNotNull(result);
            StatusCodeResult statusCodeResult = (StatusCodeResult)result;
            Assert.AreEqual(expectedStatusCode, statusCodeResult.StatusCode);

            var modelState = chatRoomController.ModelState;
            Assert.AreEqual(expectedErrorCount, modelState.ErrorCount);
        }

        [TestMethod]
        public void AddMessageToChatRoomWithInvalidIdReturnsStatusCode404()
        {
            // Arrange
            var chatRoomManagerMock = new Mock<IChatRoomManager>(MockBehavior.Strict);
            chatRoomManagerMock.Setup(manager => manager.AddMessageToChatRoom(It.IsAny<long>(), It.IsAny<Message>())).Throws(new KeyNotFoundException());
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomManagerMock.Object);
            long chatRoomId = 1;
            Message message = new Message("Carla Red", "Hello World");
            int expectedStatusCode = 404;
            int expectedErrorCount = 0;

            // Act
            SimulateValidation(message, chatRoomController);
            var result = chatRoomController.AddMessageToChatRoom(chatRoomId, message);

            // Assert
            Assert.IsNotNull(result);
            StatusCodeResult statusCodeResult = (StatusCodeResult)result;
            Assert.AreEqual(expectedStatusCode, statusCodeResult.StatusCode);

            var modelState = chatRoomController.ModelState;
            Assert.AreEqual(expectedErrorCount, modelState.ErrorCount);
        }

        [TestMethod]
        public void AddChatRoomReturnsIActionResult()
        {
            // Arrange
            var chatRoomManagerMock = new Mock<IChatRoomManager>(MockBehavior.Strict);
            chatRoomManagerMock.Setup(manager => manager.AddChatRoom(It.IsAny<ChatRoom>())).Returns(1);
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomManagerMock.Object);

            ChatRoom chatRoom = new ChatRoom("Room");

            // Act
            SimulateValidation(chatRoom, chatRoomController);
            var result = chatRoomController.AddChatRoom(chatRoom);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IActionResult));
        }

        [TestMethod]
        public void AddChatRoomReturnsStatusCodeResult()
        {
            // Arrange
            var chatRoomManagerMock = new Mock<IChatRoomManager>(MockBehavior.Strict);
            chatRoomManagerMock.Setup(manager => manager.AddChatRoom(It.IsAny<ChatRoom>())).Returns(1);
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomManagerMock.Object);

            ChatRoom chatRoom = new ChatRoom("Room");

            // Act
            SimulateValidation(chatRoom, chatRoomController);
            var result = chatRoomController.AddChatRoom(chatRoom);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
        }

        [TestMethod]
        public void AddChatRoomWithValidChatRoomReturnsStatusCode200()
        {
            // Arrange
            var chatRoomManagerMock = new Mock<IChatRoomManager>(MockBehavior.Strict);
            chatRoomManagerMock.Setup(manager => manager.AddChatRoom(It.IsAny<ChatRoom>())).Returns(1);
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomManagerMock.Object);

            ChatRoom chatRoom = new ChatRoom("Room");
            int expectedStatusCode = 200;
            int expectedErrorCount = 0;

            // Act
            SimulateValidation(chatRoom, chatRoomController);
            var result = chatRoomController.AddChatRoom(chatRoom);

            // Assert
            Assert.IsNotNull(result);
            StatusCodeResult statusCodeResult = (StatusCodeResult)result;
            Assert.AreEqual(expectedStatusCode, statusCodeResult.StatusCode);

            var modelState = chatRoomController.ModelState;
            Assert.AreEqual(expectedErrorCount, modelState.ErrorCount);
        }

        [TestMethod]
        public void AddChatRoomWithNameNullReturnsStatusCode400()
        {
            // Arrange
            var chatRoomManagerMock = new Mock<IChatRoomManager>(MockBehavior.Strict);
            chatRoomManagerMock.Setup(manager => manager.AddChatRoom(It.IsAny<ChatRoom>())).Returns(1);
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomManagerMock.Object);

            ChatRoom chatRoom = new ChatRoom(null);
            int expectedStatusCode = 400;
            int expectedErrorCount = 1;

            // Act
            SimulateValidation(chatRoom, chatRoomController);
            var result = chatRoomController.AddChatRoom(chatRoom);

            // Assert
            Assert.IsNotNull(result);
            StatusCodeResult statusCodeResult = (StatusCodeResult)result;
            Assert.AreEqual(expectedStatusCode, statusCodeResult.StatusCode);

            var modelState = chatRoomController.ModelState;
            Assert.AreEqual(expectedErrorCount, modelState.ErrorCount);
            Assert.AreEqual(modelState.First().Key, "Name");
        }

        [TestMethod]
        public void AddChatRoomWithNameEmptyReturnsStatusCode400()
        {
            // Arrange
            var chatRoomManagerMock = new Mock<IChatRoomManager>(MockBehavior.Strict);
            chatRoomManagerMock.Setup(manager => manager.AddChatRoom(It.IsAny<ChatRoom>())).Returns(1);
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomManagerMock.Object);

            ChatRoom chatRoom = new ChatRoom("");
            int expectedStatusCode = 400;
            int expectedErrorCount = 1;

            // Act
            SimulateValidation(chatRoom, chatRoomController);
            var result = chatRoomController.AddChatRoom(chatRoom);

            // Assert
            Assert.IsNotNull(result);
            StatusCodeResult statusCodeResult = (StatusCodeResult)result;
            Assert.AreEqual(expectedStatusCode, statusCodeResult.StatusCode);

            var modelState = chatRoomController.ModelState;
            Assert.AreEqual(expectedErrorCount, modelState.ErrorCount);
            Assert.AreEqual(modelState.First().Key, "Name");
        }

        [TestMethod]
        public void PostWithValidChatRoomCallsChatRoomManager()
        {
            // Arrange
            var chatRoomManagerMock = new Mock<IChatRoomManager>(MockBehavior.Strict);
            chatRoomManagerMock.Setup(manager => manager.AddChatRoom(It.IsAny<ChatRoom>())).Returns(1);
            ChatRoomController chatRoomController = new ChatRoomController(chatRoomManagerMock.Object);

            ChatRoom chatRoom = new ChatRoom("Room");

            // Act
            SimulateValidation(chatRoom, chatRoomController);
            chatRoomController.AddChatRoom(chatRoom);

            // Assert
            chatRoomManagerMock.Verify(manager => manager.AddChatRoom(It.IsAny<ChatRoom>()), Times.Once);
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
