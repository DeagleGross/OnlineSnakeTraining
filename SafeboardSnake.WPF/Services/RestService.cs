using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Accessibility;
using RestSharp;
using SafeboardSnake.Core.Helpers;
using SafeboardSnake.Core.Models;
using SafeboardSnake.Core.Models.DataTransferContracts;
using SafeboardSnake.WPF.Constants;
using SafeboardSnake.WPF.Models;

namespace SafeboardSnake.WPF.Services
{
    public class RestService
    {
        private readonly RestClient _client;

        public RestService()
        {
            _client = new RestClient()
            {
                BaseUrl = new Uri(Consts.DomainAddress)
            };
        }

        public async Task<NameResponse> GetUserName()
        {
            var request = new RestRequest("Player/name", Method.GET);
            request.AddParameter("token", Consts.Token);

            var response = await _client.ExecuteGetTaskAsync(request);

            return response.Content.JsonDeserialize<NameResponse>();
        }

        public async Task<GameState> GetGameState()
        {
            var request = new RestRequest("Player/gameboard", Method.GET);
            request.AddParameter("token", Consts.Token);

            var response = await _client.ExecuteGetTaskAsync(request);

            return response.Content.JsonDeserialize<GameState>();
        }

        public async Task ChangeDirection(string directionValue)
        {
            var request = new RestRequest("Player/direction", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            var directionRequestModel = new DirectionRequestModel
            {
                Direction = directionValue, Token = Consts.Token
            };

            var strDirectionRequestModel = directionRequestModel.JsonSerialize();

            request.AddParameter("application/json", strDirectionRequestModel, ParameterType.RequestBody);

            // request.AddJsonBody(directionRequestModel);

            await _client.ExecutePostTaskAsync(request);
        }
    }
}
