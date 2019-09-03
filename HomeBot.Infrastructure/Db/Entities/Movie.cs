using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HomeBot.Infrastructure.Db.Entities
{
    [Table("Movies")]
    public class Movie : TEntity
    {
        public int Id { get; set; }
        public string DateTime { get; set; }
        public string MovieTitle { get; set; }
        public string MovieManget { get; set; }
        public string MoviePage { get; set; }
    }
}
