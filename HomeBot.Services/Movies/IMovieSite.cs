using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeBot.Services.Movies
{
    public interface IMovieSite
    {
        Task<int> BrowseAsync();
    }
}
