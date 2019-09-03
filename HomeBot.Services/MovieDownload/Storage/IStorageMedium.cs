using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBot.Services.MovieDownload.Storage
{
    public interface IStorageMedium
    {
        void StoreAsync(string magnet, Action callback);
    }
}
