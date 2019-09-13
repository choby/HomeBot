using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace HomeBot.Services.Tasks.Movie
{
    public static class MovieDownloadQuartzExtensions
    {
        
        public static async Task StartMovieDownload(this IApplicationBuilder app)
        {
            
                var factory = new StdSchedulerFactory();
                var scheduler = await factory.GetScheduler();

                scheduler.JobFactory = app.ApplicationServices.GetService<IMovieDownloadJobFactory>() as IJobFactory;

                await scheduler.Start();
                var job = JobBuilder.Create<MovieDownloadJob>()
                    .WithIdentity("MovieDownloadJob", "MovieDownloadTask")
                    .Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity("MovieDownloadTrigger", "MovieDownloadTask")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInHours(12)
                        .RepeatForever())
                    .Build();

                await scheduler.ScheduleJob(job, trigger);

            
        }

        
    }
}
