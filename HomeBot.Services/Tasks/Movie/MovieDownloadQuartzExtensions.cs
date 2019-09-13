using System;
namespace HomeBot.Services.Tasks.Movie
{
    public static class MovieDownloadQuartzExtensions
    {
        public static void MovieDownloadQuartzExtensions(this IApplicationBuilder app)
        {
            app.ApplicationServices.GetService<IScheduler>();
        }

        public static async void AddQuartz(this IServiceCollection services)
        {
            var props = new NameValueCollection
     {
      {"quartz.serializer.type", "json"}
     };
            var factory = new StdSchedulerFactory(props);
            var scheduler = await factory.GetScheduler();

            var jobFactory = new IoCJobFactory(services.BuildServiceProvider());
            scheduler.JobFactory = jobFactory;
            await scheduler.Start();
            services.AddSingleton(scheduler);
        }
    }
}
