using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPIProject5.Models
{
    public partial class Report
    {
        public int Id { get; set; }
        public string Asunto { get; set; }
        public string Descripcion { get; set; }
        public int IdUbication { get; set; }
        public byte[] Foto { get; set; }
        public string ImageUbicaton { get; set; }
        public int IdStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int IdUser { get; set; }

        public virtual Status IdStatusNavigation { get; set; }
        public virtual Ubication IdUbicationNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
