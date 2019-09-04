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
        public Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var movieSite = scope.ServiceProvider.GetRequiredService<IMovieSite>();
                return movieSite.BrowseAsync();
            }    
        }
    }
   
}
