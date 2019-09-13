using HomeBot.Infrastructure.Db;
using HomeBot.Infrastructure.Db.Entities;
using HomeBot.Services.DbServices;
using HomeBot.Services.Movies.Storage;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBot.Services.Movies
{
    public class GaoqingLa : IMovieSite
    {
        #region 爬取gaoqingla的一些配置常量
        const string siteIndex = "http://gaoqing.la";
        const string pageUrlPath = "//*[@id='post_container']/li/div/div[@class='article']/h2/a";
        const string magnetPath = "//*[@id='post_content']/p/span[contains(text(),'1080P：') or contains(text(),'高码1080P：')]/../following-sibling::p[1]/span/a";
        const string titlePath = "//*[@id='content']/div[1]/h1";
        #endregion
        HtmlWeb _webClient;
        IStorageMedium _storageMedium;
        IMovieDownloadService _movieDownloadService;
        ILogService _logService;
        public GaoqingLa(HtmlWeb webClient,
            IStorageMedium storageMedium,
            IMovieDownloadService movieDownloadService,
            ILogService logService)
        {
            _webClient = webClient;
            _webClient.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.42 Safari/537.36 Edg/77.0.235.17";
            _storageMedium = storageMedium;
            _movieDownloadService = movieDownloadService;
            _logService = logService;
        }
        public async Task<int> BrowseAsync()
        {
            var urls = this.GetPageUrls();
            int qty = 0;
            foreach (var url in urls)
            {
                try
                {
                    qty += await this.DownloadAsync(url);
                }
                catch (Exception ex)
                {
                    _logService.Log(new Log
                    {
                        Type = LogType.Error,
                        Info = $"下载{url}时出错：{ex.Message},{ex.InnerException?.Message}",
                        Level = 0,
                        DateTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)
                    });
                }
            }
            return qty;
        }
        private IEnumerable<string> GetPageUrls()
        {
            HtmlDocument doc = _webClient.Load(siteIndex);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(pageUrlPath);
            return nodes.Select(node => node.Attributes["href"].Value);
        }


        private Movie GetInfo(string pageUrl)
        {
            HtmlDocument doc = _webClient.Load(pageUrl);
            HtmlNodeCollection aLinks = doc.DocumentNode.SelectNodes(magnetPath); //所有1080p的a标签
            string title = doc.DocumentNode.SelectSingleNode(titlePath).InnerText; //电影名
            string manget = null;
            if (aLinks != null && aLinks.Count > 0)
            {
                var last1080p = aLinks[aLinks.Count - 1];//gaoqingla通常会放720，1080，高清1080和原盘，并且同一种分辨率也有多条链接，只下载最后一条1080
                manget = last1080p.Attributes["href"].Value; //磁力链接
            }

            return new Movie
            {
                DateTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now),
                Manget = manget,
                Page = pageUrl,
                Title = title
            };
        }

        private async Task<int> DownloadAsync(string pageUrl)
        {
            var movie = this.GetInfo(pageUrl);
            if (!string.IsNullOrEmpty(movie.Manget) && !_movieDownloadService.MovieIsMownloaded(pageUrl, movie.Manget))
            {
                var result = await _storageMedium.StoreAsync(movie.Manget);
                if (result)
                {
                    _movieDownloadService.Add(movie);
                    _logService.Log(new Log
                    {
                        Type = LogType.Info,
                        Info = $"完成《{movie.Title}》下载",
                        Level = 0,
                        DateTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)
                    });
                    return 1;
                }
            }
            return 0;
        }

    }
}
