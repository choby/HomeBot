using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace HomeBot.Services.Tasks.Movie
{
    public class MovieDownloadTask
    {
        static IScheduler scheduler;
        public static async void Start(IServiceProvider _serviceProvider)
        {
            if (scheduler != null)
            {
                return;
            }
            var props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            var factory = new StdSchedulerFactory(props);
            scheduler = await factory.GetScheduler();
            scheduler.JobFactory = new MovieDownloadJobFactory(_serviceProvider);

            await scheduler.Start();
            var job = JobBuilder.Create<MovieDownloadJob>()
                .WithIdentity("MovieDownloadJob", "MovieDownloadTask")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("MovieDownloadTrigger", "MovieDownloadTask")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(24)
                    .RepeatForever())
            .Build();

          await scheduler.ScheduleJob(job, trigger);
        }
    }
}
