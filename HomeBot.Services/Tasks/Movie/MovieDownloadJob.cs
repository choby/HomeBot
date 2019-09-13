using HomeBot.Infrastructure.Db.Entities;
using HomeBot.Services.DbServices;
using HomeBot.Services.Movies;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeBot.Services.Tasks.Movie
{
    public class MovieDownloadJob : IJob
    {
        IServiceProvider _serviceProvider;
        public MovieDownloadJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task Execute(IJobExecutionContext context)
        {
           
            try
            {

                //为每个线程注入新的实例，以避免DbContext被销毁无法访问数据库
                using (var scope = _serviceProvider.CreateScope())
                {
                    var logService = scope.ServiceProvider.GetService<ILogService>();
                    logService.Log(new Log
                    {
                        Info = "下载任务开始",
                        Type = LogType.Info,
                        Level = 0,
                        DateTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)
                    });
                    var movieSite = scope.ServiceProvider.GetService<IMovieSite>();
                    int count = await movieSite.BrowseAsync();
                    logService.Log(new Log
                    {
                        Info = $"下载结束，共下载{count}部电影",
                        Type = LogType.Info,
                        Level = 0,
                        DateTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)
                    });
                }
            
            }
            catch (Exception ex)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var logService = scope.ServiceProvider.GetService<ILogService>();
                    logService.Log(new Log
                    {
                        Info = ex.Message,
                        Type = LogType.Error,
                        Level = 0,
                        DateTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)
                    });
                }
               
            }
           
        }
    }
   
}
