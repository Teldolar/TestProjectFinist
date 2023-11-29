using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    [Table("clients")]
    public class Client
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("number")]
        public string? Number { get; set; }
        [Column("password")]
        public string? Password { get; set; }
        [Column("urgentaccount")]
        public string? UrgentAccount { get; set; }
        [Column("demandaccount")]
        public string? DemandAccount { get; set; }
        [Column("cardaccount")]
        public string? CardAccount { get; set; }
    }
}
