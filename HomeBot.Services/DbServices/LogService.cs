using System.Linq;
using System.Collections.Generic;
using HomeBot.Infrastructure.Db;
using HomeBot.Infrastructure.Db.Entities;

namespace HomeBot.Services.DbServices
{
    public class LogService : ILogService
    {
        IRepository<Log> _repository;
        public LogService(IRepository<Log> repository)
        {
            _repository = repository;
        }
        
        public void Log(Log log)
        {
            _repository.Add(log);
            _repository.SaveChanges();
        }

        public IEnumerable<Log> GetTop50Logs()
        {
            return _repository.Table.OrderByDescending(x => x.Id).Take(50);
        }
    }
}
