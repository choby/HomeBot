using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HomeBot.Infrastructure.Db.Entities
{
    [Table("Logs")]
    public class Log : TEntity
    {
        public int Id { get; set; }
        public LogType Type { get; set; }
        public string Info { get; set; }
        public int Level { get; set; }
        public string DateTime { get; set; }
    }

    public enum LogType
    {
        Info = 1,
        Warning = 2,
        Error = 3
    }
}
