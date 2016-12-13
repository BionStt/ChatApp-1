using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WouterFennis.ChatApp.Domain;
using WouterFennis.ChatApp.DAL.Repositories;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WouterFennis.ChatApp.Controllers
{
    [Route("api/v1/[controller]")]
    public class ChatRoomController : Controller
    {
        private IRepository<ChatRoom, long> _chatRoomRepository;

        public ChatRoomController(IRepository<ChatRoom, long> chatRoomRepository)
        {
            _chatRoomRepository = chatRoomRepository;
        }
        // GET: api/v1/ChatRoom
        [HttpGet]
        public IEnumerable<ChatRoom> Get()
        {           
            return _chatRoomRepository.FindAll();
        }

        // POST api/v1/ChatRoom
        [HttpPost]
        public ActionResult Post([FromBody]ChatRoom chatRoom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _chatRoomRepository.Insert(chatRoom);
            return Ok();
        }
    }
}
