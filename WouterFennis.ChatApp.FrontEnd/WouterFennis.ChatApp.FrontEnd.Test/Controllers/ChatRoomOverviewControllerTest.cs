using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WouterFennis.ChatApp.FrontEnd.Controllers;
using WouterFennis.ChatApp.FrontEnd.Test.Mocks;
using WouterFennis.ChatApp.FrontEnd.ViewModels;

namespace WouterFennis.ChatApp.FrontEnd.Test.Controllers
{
    [TestClass]
    public class ChatRoomOverviewControllerTest
    {
        [TestMethod]
        public void IndexReturnsActionResult()
        {
            // Arrange
            ChatRoomBackendServiceMock chatRoomService = new ChatRoomBackendServiceMock();
            ChatRoomOverviewController controller = new ChatRoomOverviewController(chatRoomService);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void IndexReturnsViewResult()
        {
            // Arrange
            ChatRoomBackendServiceMock chatRoomService = new ChatRoomBackendServiceMock();
            ChatRoomOverviewController controller = new ChatRoomOverviewController(chatRoomService);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void IndexReturnsViewResultWithListOfChatRoomViewModel()
        {
            // Arrange
            ChatRoomBackendServiceMock chatRoomService = new ChatRoomBackendServiceMock();
            ChatRoomOverviewController controller = new ChatRoomOverviewController(chatRoomService);

            // Act
            var result = controller.Index();

            // Assert
            ViewResult viewResult = (ViewResult)result;
            var model = viewResult.Model;
            Assert.IsNotNull(model);
            Assert.IsInstanceOfType(viewResult.Model, typeof(IEnumerable<ChatRoomViewModel>));
        }

        [TestMethod]
        public void IndexCallsChatRoomBackendAgent()
        {
            // Arrange
            ChatRoomBackendServiceMock chatRoomService = new ChatRoomBackendServiceMock();
            ChatRoomOverviewController controller = new ChatRoomOverviewController(chatRoomService);

            // Act
            var result = controller.Index();

            // Assert
            Assert.AreEqual(chatRoomService.GetAllChatRoomsTimesCalled, 1);
        }
    }
}
