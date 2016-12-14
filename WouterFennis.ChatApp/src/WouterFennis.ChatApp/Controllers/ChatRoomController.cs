using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WouterFennis.ChatApp.Domain;
using WouterFennis.ChatApp.DAL.Repositories;
using System.Net;
using Swashbuckle.SwaggerGen.Annotations;

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
        [SwaggerOperation("GetAllChatRooms")]
        [ProducesResponseType(typeof(IEnumerable<ChatRoom>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        public IEnumerable<ChatRoom> Get()
        {       
            IEnumerable<ChatRoom> chatRooms = _chatRoomRepository.FindAll();
            return chatRooms;
        }

        // GET: api/v1/ChatRoom/1
        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation("GetChatRoomById")]
        [ProducesResponseType(typeof(ChatRoom), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        public IActionResult GetById(long id)
        {
            ChatRoom foundChatRoom = null;
            if (_chatRoomRepository.Exists(id))
            {
                foundChatRoom = _chatRoomRepository.FindByIdWithMessages(id).FirstOrDefault();
                return new ObjectResult(foundChatRoom);
            }
            return NotFound();
        }

        // POST api/v1/ChatRoom
        [HttpPost]
        [SwaggerOperation("AddChatRoom")]
        [ProducesResponseType(typeof(OkResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
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
