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
        public int Type { get; set; }
        public string Info { get; set; }
        public int Level { get; set; }
    }
}
