using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WouterFennis.ChatApp.Domain;
using WouterFennis.ChatApp.DAL.Repositories;
using System.Net;
using Swashbuckle.SwaggerGen.Annotations;
using WouterFennis.ChatApp.Managers;

namespace WouterFennis.ChatApp.Controllers
{
    [Route("api/v1/[controller]")]
    public class ChatRoomController : Controller
    {
        private IChatRoomManager _chatRoomManager;

        public ChatRoomController(IChatRoomManager chatRoomManager)
        {
            _chatRoomManager = chatRoomManager;
        }
        // GET: api/v1/ChatRoom
        [HttpGet]
        [SwaggerOperation("GetAllChatRooms")]
        [ProducesResponseType(typeof(IEnumerable<ChatRoom>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        public IEnumerable<ChatRoom> GetAllChatRooms()
        {
            IEnumerable<ChatRoom> chatRooms = _chatRoomManager.GetAllChatRooms();
            return chatRooms;
        }

        // GET: api/v1/ChatRoom/1
        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation("GetChatRoomById")]
        [ProducesResponseType(typeof(ChatRoom), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        public IActionResult GetChatRoomById(long chatRoomId)
        {
            try
            {
                ChatRoom foundChatRoom = _chatRoomManager.FindChatRoomById(chatRoomId);
                return new ObjectResult(foundChatRoom);
            }
            catch(KeyNotFoundException exception)
            {
                return NotFound();
            }
        }

        // POST api/v1/ChatRoom/1
        [HttpPost]
        [Route("{id}")]
        [SwaggerOperation("AddMessageToChatRoom")]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(OkResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        public IActionResult AddMessageToChatRoom(long chatRoomId, [FromBody]Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                _chatRoomManager.AddMessageToChatRoom(chatRoomId, message);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        // POST api/v1/ChatRoom
        [HttpPost]
        [SwaggerOperation("AddChatRoom")]
        [ProducesResponseType(typeof(OkResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddChatRoom([FromBody]ChatRoom chatRoom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            long chatRoomId = _chatRoomManager.AddChatRoom(chatRoom);
            // chatRoomId isn't used ATM
            return Ok();
        }
    }
}
