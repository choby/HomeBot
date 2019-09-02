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
        #endregion
        public GaoqingLa(HtmlWeb webClient, IStorageMedium storageMedium)
        {
            _webClient = webClient;
            _webClient.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.42 Safari/537.36 Edg/77.0.235.17";
            _storageMedium = storageMedium;
        }
        public void Excute()
        {
            var urls = this.GetPageUrls();
            foreach (var url in urls)
            {
                if (!PageIsDownloaded(url))
                {
                    Task.Run(()=> {
                        string magnet = GetMagnet(url);
                        _storageMedium.Store(magnet);
                    });
                }
            }           
        }
        private IEnumerable<string> GetPageUrls() {
            HtmlDocument doc = _webClient.Load(siteIndex);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(pageUrlPath);
            return nodes.Select(node => node.Attributes["href"].Value);
        }
        //todo: 检查电影是否下载过
        private bool PageIsDownloaded(string pageUrl)
        {
            return false;
        }
        private string GetMagnet(string pageUrl)
        {
            HtmlDocument doc = _webClient.Load(pageUrl);
            HtmlNodeCollection aLinks = doc.DocumentNode.SelectNodes(magnetPath);

            if (aLinks != null && aLinks.Count > 0)
                return aLinks[aLinks.Count - 1].Attributes["href"].Value; //gaoqingla通常会放720，1080，高清1080和原盘，只下载最后一条1080

            return null;
        }

    }
}
