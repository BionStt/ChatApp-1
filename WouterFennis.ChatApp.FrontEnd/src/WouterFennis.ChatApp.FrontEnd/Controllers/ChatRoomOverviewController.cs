using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WouterFennis.ChatApp.FrontEnd.ViewModels;
using WouterFennis.ChatApp.FrontEnd.Agents;
using WouterFennis.ChatApp.FrontEnd.Agents.Models;

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
            var retrievedChatRooms = (List<ChatRoom>)_chatRoomService.GetAllChatRooms();
            var query = from chatRoom in retrievedChatRooms
                        select new ChatRoomViewModel()
                        {
                            Id = chatRoom.Id,
                            Name = chatRoom.Name
                        };
            List<ChatRoomViewModel> chatRoomList = query.ToList();

            return View(chatRoomList);
        }
    }
}
