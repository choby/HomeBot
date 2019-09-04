using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeBot.Services.Movies.Storage
{
    public interface IStorageMedium
    {
        Task<bool> StoreAsync(string magnet);
    }
}
