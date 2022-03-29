using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProject5.Models.Request
{
    public class ReportRequest
    {
        public int Id { get; set; }
        public string Asunto { get; set; }
        public string Descripcion { get; set; }
        public byte[] Foto { get; set; }
        public int IdUbicacion { get; set; }
        public string Ubicacion { get; set; }
        public int IdStatus { get; set; }
        public string Estatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public string ImageUbicaton { get; set; }
        public int IdUser { get; set; }
        public string User { get; set; }

    }
}
