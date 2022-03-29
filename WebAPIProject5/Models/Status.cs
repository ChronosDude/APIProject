using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPIProject5.Models
{
    public partial class Status
    {
        public Status()
        {
            Reports = new HashSet<Report>();
        }

        public int Id { get; set; }
        public string Status1 { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
