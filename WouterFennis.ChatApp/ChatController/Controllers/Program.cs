using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatController
{
    [TestClass]
    public class ChatControllerTest
    {
        [TestMethod]
        public void GetReturnsListWithTypeMessage()
        {
            // Arrange
            ChatController chatController = new ChatController();

            // Act
            var result = chatController.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Message>));
        }
    }
}
