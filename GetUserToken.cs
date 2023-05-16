using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HasuraActionExt
{
    using RestSharp;
    using Types;
    public class GetUserToken
    {

        private readonly ILogger<GetUserToken> _logger;
        public GetUserToken(ILogger<GetUserToken> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [FunctionName("GetToken")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                var jsonBody = await req.ReadAsStringAsync();
                _logger.LogInformation(jsonBody);
                var payload = JsonConvert.DeserializeObject<LoginPayload>(jsonBody);

                string username = payload.Input.Arg1.Username;
                string password = payload.Input.Arg1.Password;
                //string clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
                //string domain = Environment.GetEnvironmentVariable("AUTH_DOMAIN"); 
                //string audience = Environment.GetEnvironmentVariable("AUDIENCE");

                string domain = "bergensynergy.eu.auth0.com";
                string clientId = "dRDyHn7LRJzAFnaSfjlrtnBcSNBnNxCh";
                string audience = "https://api-bergensynergy.hasura.app/v1/graphql";

                var client = new RestClient($"https://{domain}/oauth/token");
                var request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("content-type", "application/json");



                request.AddParameter("application/json", "{\"client_id\":\"" + clientId + "\",\"username\":\"" + username + "\",\"password\":\"" + password + "\",\"scope\":\"openid\",\"audience\":\"" + audience + "\",\"grant_type\":\"password\"}", ParameterType.RequestBody);

                RestResponse response = client.Execute(request);
                var auth0response = JsonConvert.DeserializeObject<Auth0Response>(response.Content);

                return new OkObjectResult("{\"accessToken\":\"" + auth0response.access_token + "\"}");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }


    }
}
