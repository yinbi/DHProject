using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHGame.Entity.DHWebSiteDB
{
    public class Modules
    {
        [Key]
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int OrderNo { get; set; }
        public bool Nullity { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
