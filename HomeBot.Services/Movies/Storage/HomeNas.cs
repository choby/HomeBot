using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HomeBot.Services.Movies.Storage
{
    public class HomeNas : IStorageMedium
    {
        #region nas配置字段,在configuration.json中配置
        string _url;
        string _user;
        string _password;
        #endregion
        RestClient _client;
        string sessionId;
        public HomeNas(IConfiguration configuration)
        {
            var nasConfig = configuration.GetSection("nas");
            _url = nasConfig.GetSection("url").Value;
            _user = nasConfig.GetSection("user").Value;
            _password = nasConfig.GetSection("password").Value;
            _client = new RestClient(_url);
        }

        public async Task<bool> StoreAsync(string magnet)
        {
            return await UploadMagnetToNas(magnet);
        }
        private Task<bool> UploadMagnetToNas(string magnet)
        {
            var request = new RestRequest("/transmission/rpc", Method.POST);
            var sessionid = this.GetSessionId();
            request.AddHeader("x-transmission-session-id", sessionid);
            var postParams = new Dictionary<string, object> { { "method", "torrent-add" }, { "arguments", new Dictionary<string, object> { { "download-dir", "/volume1/video" }, { "filename", magnet }, { "paused", false } } } };
            request.AddJsonBody(postParams);
            return Task.FromResult(_client.ExecuteTaskAsync(request).Result.StatusCode == HttpStatusCode.OK);
        }
        private string GetSessionId()
        {
            if (this.sessionId != null)
                return this.sessionId;

            _client.Authenticator = new HttpBasicAuthenticator(_user, _password);

            var request = new RestRequest("/transmission/rpc", Method.POST);
            request.AddHeader("Accept", "application/json");
            var postParams = new Dictionary<string, string> { { "method", "session-get" } };

            request.AddJsonBody(postParams);
            var response = _client.Execute(request);
            this.sessionId = response
                .Headers
                .Where(x => x.Name.Equals("x-transmission-session-id", StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Value)
                .FirstOrDefault()
                .ToString();
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_user}:{_password}")));
            //_httpClient.DefaultRequestHeaders.Accept.Clear();
            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var postParams = new Dictionary<string, string>{ { "method","session-get" } };
            //var jsonBody = new StringContent(JsonConvert.SerializeObject(postParams), Encoding.UTF8, "application/json");
            //var sessionid = _httpClient.PostAsync(_url, jsonBody).Result.Headers.GetValues("x-transmission-session-id");

            return this.sessionId;
        }


    }
}
