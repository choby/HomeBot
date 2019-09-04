using Quartz;
using Quartz.Spi;
using System;
using Microsoft.Extensions.DependencyInjection;


namespace HomeBot.Services.Tasks.Movie
{
    public interface IMovieDownloadJobFactory { }
    public class MovieDownloadJobFactory : IJobFactory, IMovieDownloadJobFactory
    {
        IServiceProvider _serviceProvider;
        public MovieDownloadJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetService<MovieDownloadJob>();
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}
