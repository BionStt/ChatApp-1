using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;
using Newtonsoft.Json;
using WouterFennis.ChatApp.FrontEnd.Agents;
using WouterFennis.ChatApp.FrontEnd.Agents.Models;
using System.Net.Http;

namespace WouterFennis.ChatApp.FrontEnd.Test.Mocks
{
    public class ChatRoomBackendServiceMock : IChatRoomBackendServiceAgent
    {
        public int GetAllChatRoomsTimesCalled { get; private set; }

        public Uri BaseUri
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public JsonSerializerSettings DeserializationSettings
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public JsonSerializerSettings SerializationSettings
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Task<HttpOperationResponse<object>> AddChatRoomWithHttpMessagesAsync(ChatRoom chatRoom = null, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpOperationResponse<object>> AddMessageToChatRoomWithHttpMessagesAsync(long chatRoomId, Message message = null, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<HttpOperationResponse<object>> GetAllChatRoomsWithHttpMessagesAsync(Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            GetAllChatRoomsTimesCalled++;

            // Create Mock return value
            HttpRequestMessage _httpRequest = new HttpRequestMessage();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("GET");

            var _result = new HttpOperationResponse<object>();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            _result.Body = false;

            return Task.FromResult(_result);
        }

        public Task<HttpOperationResponse<object>> GetChatRoomByIdWithHttpMessagesAsync(long chatRoomId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
