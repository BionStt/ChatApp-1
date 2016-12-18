using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WouterFennis.ChatApp.FrontEnd.ViewModels;
using WouterFennis.ChatApp.FrontEnd.Agents;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WouterFennis.ChatApp.FrontEnd.Controllers
{
    public class ChatRoomOverviewController : Controller
    {
        private readonly IChatRoomBackendServiceAgent _chatRoomService;

        public ChatRoomOverviewController(IChatRoomBackendServiceAgent chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<ChatRoomViewModel> chatRoomList = new List<ChatRoomViewModel>();

            var chatRoomList2 = _chatRoomService.GetAllChatRooms();

            return View(chatRoomList);
        }
    }
}
