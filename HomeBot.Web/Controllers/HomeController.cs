using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeBot.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;
using HtmlAgilityPack;
using HomeBot.Services.MovieDownload.Storage;
using HomeBot.Services.MovieDownload;

namespace HomeBot.Controllers
{
    public class HomeController : Controller
    {

        IMovieDownload _movieDownload;
        HtmlWeb _webClient;
        IStorageMedium _storageMedium;
        public HomeController(IStorageMedium storageMedium, IMovieDownload movieDownload,
            HtmlWeb webClient)
        {
            _storageMedium = storageMedium;
            _movieDownload = movieDownload;
            _webClient = webClient;
        }


        public IActionResult Index()
        {

            // this.downloadMagnet("magnet:?xt=urn:btih:7757f5c4d12b3fcc9a34552a3522cd5525032844&dn=Dark.Phoenix.2019.1080p.BluRay.x264-GECKOS&tr=http%3A%2F%2Ftracker.trackerfix.com%3A80%2Fannounce&tr=udp%3A%2F%2F9.rarbg.me%3A2740&tr=udp%3A%2F%2F9.rarbg.to%3A2790");
            //var magnet = getMagnet("http://gaoqing.la/cold-blood.html");
            //_storageMedium.Store("xx");
            _movieDownload.Excute();
            return View();
        }

        void downloadMagnet(string magnet)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes("choby:a135246A")));

                var address = new Uri("https://bt.vlinli.cn/transmission/rpc");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //登陆，获取session标识
                var sessionid = client.PostAsJsonAsync(address,
                    new Dictionary<string, string>
                    {
                        { "method","session-get" }
                    }).Result.Headers.GetValues("x-transmission-session-id");

                //将session标识加入请求
                client.DefaultRequestHeaders.Add("x-transmission-session-id", sessionid);


                //下载参数
                var body = new Dictionary<string, object>
                {
                    { "method", "torrent-add"},
                    {"arguments",
                        new Dictionary<string, object>
                        {
                            { "download-dir","/volume1/video"},
                            {"filename",magnet },
                            { "paused",false}
                        }
                    }   
                };

                client.PostAsJsonAsync(address, body).ContinueWith(response=> {
                   // var x = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,>>(response.Result.Content.ReadAsStringAsync().Result);
                });
            }
        }


        string getMagnet(string pageUrl)
        {


            _webClient.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.42 Safari/537.36 Edg/77.0.235.17";
            HtmlDocument doc = _webClient.Load(pageUrl);
            //
            //HttpClient client = _httpClientFactory.CreateClient();
            //client.DefaultRequestHeaders.Clear();
            //client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
            //client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            //client.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.9");
            //client.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
            //client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            //client.DefaultRequestHeaders.Add("Cookie", "UM_distinctid=16ce7e5fdb9663-0730393ca5b947-2a5b2417-240000-16ce7e5fdba9b5; CNZZDATA5780407=cnzz_eid%3D1363353297-1567254780-%26ntime%3D1567265081");
            //client.DefaultRequestHeaders.Add("Host", "gaoqing.la");
            //client.DefaultRequestHeaders.Add("Referer", "http://gaoqing.la/");
            //client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            //client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.42 Safari/537.36 Edg/77.0.235.17");

            //var taskResponse = client.GetAsync("http://gaoqing.la/cold-blood.html", HttpCompletionOption.ResponseContentRead);
            
            //taskResponse.Wait();
            //taskResponse.Result.Content.Headers.ContentType.CharSet = "UTF-8";
            //var response = taskResponse.Result;
            //var taskContent = response.Content.ReadAsStringAsync();
            //taskContent.Wait();
            //HtmlDocument doc = new HtmlDocument();
            //doc.LoadHtml(taskContent.Result);
            string rowPath = "//*[@id='post_content']/p/span[contains(text(),'1080P：') or contains(text(),'高码1080P：')]/../following-sibling::p[1]/span/a";
            HtmlNodeCollection a = doc.DocumentNode.SelectNodes(rowPath);

            if (a != null && a.Count() > 0)
                return a[a.Count() - 1].Attributes["href"].Value;

            return null;

        }



        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
