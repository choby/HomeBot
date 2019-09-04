using HomeBot.Infrastructure.Db.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBot.Services.DbServices
{
    public interface IMovieDownloadService
    {
        public bool MovieIsMownloaded(string pageUrl, string manget);
        public int Add(Movie movie);
    }
}
