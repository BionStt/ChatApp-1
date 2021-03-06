﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using WouterFennis.ChatApp.BackEnd.DAL.Repositories;
using WouterFennis.ChatApp.BackEnd.Domain;
using WouterFennis.ChatApp.BackEnd.Managers;

namespace WouterFennis.ChatApp.BackEnd.Test.Managers
{
    [TestClass]
    public class ChatRoomManagerTest
    {
        [TestMethod]
        public void GetAllChatRoomsReturnsIEnumerableChatRoom()
        {
            // Arrange
            var chatRoomRepositoryMock = new Mock<IRepository<ChatRoom, long>>(MockBehavior.Strict);
            chatRoomRepositoryMock.Setup(repository => repository.FindAll()).Returns(new List<ChatRoom>());
            ChatRoomManager chatRoomManager = new ChatRoomManager(chatRoomRepositoryMock.Object);

            // Act
            var result = chatRoomManager.GetAllChatRooms();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<ChatRoom>));
        }

        [TestMethod]
        public void GetAllChatRoomsCallsRepository()
        {
            // Arrange
            var chatRoomRepositoryMock = new Mock<IRepository<ChatRoom, long>>(MockBehavior.Strict);
            chatRoomRepositoryMock.Setup(repository => repository.FindAll()).Returns(new List<ChatRoom>());
            ChatRoomManager chatRoomManager = new ChatRoomManager(chatRoomRepositoryMock.Object);

            // Act
            var result = chatRoomManager.GetAllChatRooms();

            // Assert
            chatRoomRepositoryMock.Verify(repository => repository.FindAll(), Times.Once);
        }

        [TestMethod]
        public void FindChatRoomByIdReturnsChatRoom()
        {
            // Arrange
            var chatRoomRepositoryMock = new Mock<IRepository<ChatRoom, long>>(MockBehavior.Strict);
            chatRoomRepositoryMock.Setup(repository => repository.Exists(It.IsAny<long>())).Returns(true);
            chatRoomRepositoryMock.Setup(repository => repository.FindByIdWithMessages(It.IsAny<long>())).Returns(new List<ChatRoom>() { new ChatRoom("room") });
            ChatRoomManager chatRoomManager = new ChatRoomManager(chatRoomRepositoryMock.Object);
            long chatRoomId = 1;

            // Act
            var result = chatRoomManager.FindChatRoomById(chatRoomId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ChatRoom));
        }

        [TestMethod]
        public void FindChatRoomByIdWithInvalidIdThrowsKeyNotFoundException()
        {
            // Arrange
            var chatRoomRepositoryMock = new Mock<IRepository<ChatRoom, long>>(MockBehavior.Strict);
            chatRoomRepositoryMock.Setup(repository => repository.Exists(It.IsAny<long>())).Returns(false);
            chatRoomRepositoryMock.Setup(repository => repository.FindByIdWithMessages(It.IsAny<long>())).Returns(new List<ChatRoom>() { new ChatRoom("room") });
            ChatRoomManager chatRoomManager = new ChatRoomManager(chatRoomRepositoryMock.Object);
            long chatRoomId = 1;

            // Act Assert
            Assert.ThrowsException<KeyNotFoundException>(() => chatRoomManager.FindChatRoomById(chatRoomId));
        }

        [TestMethod]
        public void FindChatRoomByIdCallsRepository()
        {
            // Arrange
            var chatRoomRepositoryMock = new Mock<IRepository<ChatRoom, long>>(MockBehavior.Strict);
            chatRoomRepositoryMock.Setup(repository => repository.Exists(It.IsAny<long>())).Returns(true);
            chatRoomRepositoryMock.Setup(repository => repository.FindByIdWithMessages(It.IsAny<long>())).Returns(new List<ChatRoom>() { new ChatRoom("room") });
            ChatRoomManager chatRoomManager = new ChatRoomManager(chatRoomRepositoryMock.Object);
            long chatRoomId = 1;

            // Act Assert
            chatRoomManager.FindChatRoomById(chatRoomId);

            // Assert
            chatRoomRepositoryMock.Verify(repository => repository.FindByIdWithMessages(It.IsAny<long>()), Times.Once);
        }

        [TestMethod]
        public void AddMessageToChatRoomAddsMessage()
        {
            // Arrange
            ChatRoom parameter = null;
            var chatRoomRepositoryMock = new Mock<IRepository<ChatRoom, long>>(MockBehavior.Strict);
            chatRoomRepositoryMock.Setup(repository => repository.FindByIdWithMessages(It.IsAny<long>())).Returns(new List<ChatRoom>() { new ChatRoom("room") });
            chatRoomRepositoryMock.Setup(repository => repository.Exists(It.IsAny<long>())).Returns(true);
            chatRoomRepositoryMock.Setup(repository => repository.Update(It.IsAny<ChatRoom>()))
                .Callback<ChatRoom>((chatRoom) => parameter = chatRoom);
            ChatRoomManager chatRoomManager = new ChatRoomManager(chatRoomRepositoryMock.Object);
            long chatRoomId = 1;
            Message message = new Message("Carla Red", "Hello World");
            // Act Assert
            chatRoomManager.AddMessageToChatRoom(chatRoomId, message);

            // Assert
            Assert.AreEqual(1, parameter.Messages.Count);
            Assert.AreEqual("Carla Red", parameter.Messages[0].Sender);
            Assert.AreEqual("Hello World", parameter.Messages[0].Content);
            Assert.AreEqual("room", parameter.Name);
        }

        [TestMethod]
        public void AddMessageToChatRoomWithInvalidIdThrowsKeyNotFoundException()
        {
            // Arrange
            var chatRoomRepositoryMock = new Mock<IRepository<ChatRoom, long>>(MockBehavior.Strict);
            chatRoomRepositoryMock.Setup(repository => repository.FindByIdWithMessages(It.IsAny<long>())).Returns(new List<ChatRoom>() { new ChatRoom("room") });
            chatRoomRepositoryMock.Setup(repository => repository.Exists(It.IsAny<long>())).Returns(false);
            chatRoomRepositoryMock.Setup(repository => repository.Update(It.IsAny<ChatRoom>()));
            ChatRoomManager chatRoomManager = new ChatRoomManager(chatRoomRepositoryMock.Object);
            long chatRoomId = 1;
            Message message = new Message("Carla Red", "Hello World");

            // Act Assert
            Assert.ThrowsException<KeyNotFoundException>(() => chatRoomManager.AddMessageToChatRoom(chatRoomId, message));
        }

        [TestMethod]
        public void AddMessageToChatRoomCallsRepository()
        {
            // Arrange
            var chatRoomRepositoryMock = new Mock<IRepository<ChatRoom, long>>(MockBehavior.Strict);
            chatRoomRepositoryMock.Setup(repository => repository.FindByIdWithMessages(It.IsAny<long>())).Returns(new List<ChatRoom>() { new ChatRoom("room") });
            chatRoomRepositoryMock.Setup(repository => repository.Exists(It.IsAny<long>())).Returns(true);
            chatRoomRepositoryMock.Setup(repository => repository.Update(It.IsAny<ChatRoom>()));
            ChatRoomManager chatRoomManager = new ChatRoomManager(chatRoomRepositoryMock.Object);
            long chatRoomId = 1;
            Message message = new Message("Carla Red", "Hello World");

            // Act
            chatRoomManager.AddMessageToChatRoom(chatRoomId, message);

            // Assert
            chatRoomRepositoryMock.Verify(repository => repository.Update(It.IsAny<ChatRoom>()), Times.Once);
        }

        [TestMethod]
        public void AddChatRoomCallsRepository()
        {
            // Arrange
            var chatRoomRepositoryMock = new Mock<IRepository<ChatRoom, long>>(MockBehavior.Strict);
            chatRoomRepositoryMock.Setup(repository => repository.Insert(It.IsAny<ChatRoom>())).Returns(1);
            ChatRoomManager chatRoomManager = new ChatRoomManager(chatRoomRepositoryMock.Object);

            ChatRoom chatRoom = new ChatRoom("room");

            // Act
            chatRoomManager.AddChatRoom(chatRoom);

            // Assert
            chatRoomRepositoryMock.Verify(repository => repository.Insert(It.IsAny<ChatRoom>()), Times.Once);
        }

        [TestMethod]
        public void AddChatRoomCallsRepositoryWithSameParameter()
        {
            // Arrange
            ChatRoom parameter = null;
            var chatRoomRepositoryMock = new Mock<IRepository<ChatRoom, long>>(MockBehavior.Strict);
            chatRoomRepositoryMock.Setup(repository => repository.Insert(It.IsAny<ChatRoom>()))
                .Callback<ChatRoom>((chatRoom) => parameter = chatRoom)
                .Returns(1);
            ChatRoomManager chatRoomManager = new ChatRoomManager(chatRoomRepositoryMock.Object);

            ChatRoom newChatRoom = new ChatRoom("room");

            // Act
            chatRoomManager.AddChatRoom(newChatRoom);

            // Assert
            Assert.AreEqual("room", parameter.Name);
            Assert.AreEqual(0, parameter.Messages.Count);
        }
    }
}
