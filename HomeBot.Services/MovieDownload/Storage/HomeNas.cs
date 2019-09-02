using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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
        HttpClient _httpClient;
        #endregion

        public HomeNas(IConfiguration configuration)
        {
            var nasConfig = configuration.GetSection("nas");
            _url = nasConfig.GetSection("url").Value;
            _user = nasConfig.GetSection("user").Value;
            _password = nasConfig.GetSection("password").Value;
            _httpClient = new HttpClient();
        }

        public void Store(string magnet)
        {
            UploadMagnetToNas(magnet);
        }
        private void UploadMagnetToNas(string magnet)
        {
            var sessionid = this.GetSessionId();
            _httpClient.DefaultRequestHeaders.Add("x-transmission-session-id", sessionid);
            var postParams = new Dictionary<string, object> { { "method", "torrent-add" }, { "arguments", new Dictionary<string, object> { { "download-dir", "/volume1/video" }, { "filename", magnet }, { "paused", false } } } };
            _httpClient.PostAsJsonAsync(_url, postParams).ContinueWith(response =>
            {
                // todo: 写入已下载列表和日志
                // var x = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,>>(response.Result.Content.ReadAsStringAsync().Result);
            });
        }
        private string GetSessionId()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_user}:{_password}")));
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var postParams = new Dictionary<string, string>{ { "method","session-get" } };
            var sessionid = _httpClient.PostAsJsonAsync(_url, postParams).Result.Headers.GetValues("x-transmission-session-id");
            return sessionid.ToArray()[0];
        }

        
    }
}
