using HomeBot.Infrastructure.Db;
using HomeBot.Infrastructure.Db.Entities;
using HomeBot.Services.DbServices;
using HomeBot.Services.MovieDownload.Storage;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBot.Services.MovieDownload
{
    public class GaoqingLa : IMovieDownload
    {
        #region 爬取gaoqingla的一些配置常量
        HtmlWeb _webClient;
        IStorageMedium _storageMedium;
        const string siteIndex = "http://gaoqing.la";
        const string pageUrlPath = "//*[@id='post_container']/li/div/div[@class='article']/h2/a";
        const string magnetPath = "//*[@id='post_content']/p/span[contains(text(),'1080P：') or contains(text(),'高码1080P：')]/../following-sibling::p[1]/span/a";
        const string titlePath = "//*[@id='content']/div[1]/h1";
        IMovieDownloadService _movieDownloadService;
        #endregion
        public GaoqingLa(HtmlWeb webClient, IStorageMedium 
            storageMedium, IMovieDownloadService movieDownloadService)
        {
            _webClient = webClient;
            _webClient.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.42 Safari/537.36 Edg/77.0.235.17";
            _storageMedium = storageMedium;
            _movieDownloadService = movieDownloadService;
        }
        public void Excute()
        {
            var urls = this.GetPageUrls();
            foreach (var url in urls)
            {
                    Task.Run(() =>
                    {
                        var movie = GetInfo(url);
                        if (!PageIsDownloaded(url, movie.Manget))
                        {
                            _storageMedium.StoreAsync(movie.Manget, ()=> 
                            {
                                _movieDownloadService.Add(new Movie 
                                { 
                                    DateTime = string.Format("yyyy-MM-dd HH:mm:ss",DateTime.Now),
                                    MoviePage = movie.Page,
                                    MovieManget = movie.Manget,
                                    MovieTitle = movie.Title
                                });
                            });
                        }
                    });   break;             
            }
        }
        private IEnumerable<string> GetPageUrls() {
            HtmlDocument doc = _webClient.Load(siteIndex);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(pageUrlPath);
            return nodes.Select(node => node.Attributes["href"].Value);
        }
        //todo: 检查电影是否下载过
        private bool PageIsDownloaded(string pageUrl,string manget)
        {
            return _movieDownloadService.MovieIsMownloaded(pageUrl,manget);
        }
        private MovieInfo GetInfo(string pageUrl)
        {
            HtmlDocument doc = _webClient.Load(pageUrl);
            HtmlNodeCollection aLinks = doc.DocumentNode.SelectNodes(magnetPath);
            string title = doc.DocumentNode.SelectSingleNode(titlePath).InnerText;
            string manget = null;
            if (aLinks != null && aLinks.Count > 0)
                manget = aLinks[aLinks.Count - 1].Attributes["href"].Value; //gaoqingla通常会放720，1080，高清1080和原盘，只下载最后一条1080

            return new MovieInfo
            {
                Manget = manget,
                Page= pageUrl,
                Title = title
            };
        }

        class MovieInfo
        { 
            public string Title { get; set; }
            public string Page { get; set; }
            public string Manget { get; set; }
        }

    }
}
