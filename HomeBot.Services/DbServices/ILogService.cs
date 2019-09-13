using System;
using System.Collections.Generic;
using System.Text;
using HomeBot.Infrastructure.Db.Entities;

namespace HomeBot.Services.DbServices
{
   public interface ILogService
    {
        void Log(Log log);
        IEnumerable<Log> GetTop50Logs();
    }
}
