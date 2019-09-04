using HomeBot.Infrastructure.Db;
using System;
using System.Collections.Generic;
using System.Text;
using HomeBot.Infrastructure.Db.Entities;
using System.Linq;

namespace HomeBot.Services.DbServices
{
    public class MovieDownloadService : IMovieDownloadService
    {
        IRepository<Movie> _repository;
        public MovieDownloadService(IRepository<Movie> repository)
        {
            _repository = repository;
        }
        public bool MovieIsMownloaded(string pageUrl, string manget)
        {
                return _repository.Table.Any(x => x.Page == pageUrl && x.Manget == manget);
        }
        public int Add(Movie movie)
        {
            _repository.Add(movie);
            return _repository.SaveChanges();
        }
    }
}
