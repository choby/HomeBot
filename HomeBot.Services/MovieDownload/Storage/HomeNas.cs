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

namespace HomeBot.Services.MovieDownload.Storage
{
    public class HomeNas : IStorageMedium
    {
        #region nas配置字段,在configuration.json中配置
        string _url;
        string _user;
        string _password;
        RestClient _client;
        #endregion

        public HomeNas(IConfiguration configuration)
        {
            var nasConfig = configuration.GetSection("nas");
            _url = nasConfig.GetSection("url").Value;
            _user = nasConfig.GetSection("user").Value;
            _password = nasConfig.GetSection("password").Value;
            _client = new RestClient(_url);
        }

        public void StoreAsync(string magnet,Action callback)
        {
            UploadMagnetToNas(magnet, callback);
        }
        private void UploadMagnetToNas(string magnet, Action callback)
        {
            var request = new RestRequest("/transmission/rpc", Method.POST);
            var sessionid = this.GetSessionId();
            request.AddHeader("x-transmission-session-id", sessionid);
            var postParams = new Dictionary<string, object> { { "method", "torrent-add" }, { "arguments", new Dictionary<string, object> { { "download-dir", "/volume1/video" }, { "filename", magnet }, { "paused", false } } } };
            request.AddJsonBody(postParams);
            //_httpClient.DefaultRequestHeaders.Add("x-transmission-session-id", sessionid);
            //var postParams = new Dictionary<string, object> { { "method", "torrent-add" }, { "arguments", new Dictionary<string, object> { { "download-dir", "/volume1/video" }, { "filename", magnet }, { "paused", false } } } };
            //var jsonBody = new StringContent(JsonConvert.SerializeObject(postParams), Encoding.UTF8, "application/json");
            _client.ExecuteAsync(request, response =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    callback();
                }
                // todo: 写入已下载列表和日志
                // var x = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,>>(response.Result.Content.ReadAsStringAsync().Result);
            });
        }
        private string GetSessionId()
        {
            _client.Authenticator = new HttpBasicAuthenticator(_user, _password);

            var request = new RestRequest("/transmission/rpc", Method.POST);
            request.AddHeader("Accept", "application/json");
            var postParams = new Dictionary<string, string> { { "method", "session-get" } };

            request.AddJsonBody(postParams);
            var response = _client.Execute(request);
            var sessionid = response
                .Headers
                .Where(x => x.Name.Equals("x-transmission-session-id", StringComparison.InvariantCultureIgnoreCase))
                .Select(x=>x.Value)
                .FirstOrDefault()
                .ToString();
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_user}:{_password}")));
            //_httpClient.DefaultRequestHeaders.Accept.Clear();
            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var postParams = new Dictionary<string, string>{ { "method","session-get" } };
            //var jsonBody = new StringContent(JsonConvert.SerializeObject(postParams), Encoding.UTF8, "application/json");
            //var sessionid = _httpClient.PostAsync(_url, jsonBody).Result.Headers.GetValues("x-transmission-session-id");
            return sessionid;
        }


    }
}
