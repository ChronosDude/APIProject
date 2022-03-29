using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPIProject5.Models
{
    public partial class Ubication
    {
        public Ubication()
        {
            Reports = new HashSet<Report>();
        }

        public int Id { get; set; }
        public string Ubication1 { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? CreatedDate { get; set; }
        public bool? ModifiedDate { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
