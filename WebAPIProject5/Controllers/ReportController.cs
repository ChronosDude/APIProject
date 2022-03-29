using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject5.Models;
using WebAPIProject5.Models.Response;
using WebAPIProject5.Models.Request;

namespace WebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpGet]
        //Creamos el método para traer los datos en formato JSON
        public IActionResult Get()
        {
            Respuesta <List<ReportRequest>> oRespuesta = new Respuesta<List<ReportRequest>>();
            try
            {
                using (AWProyectoContext db = new AWProyectoContext())
                {
                    var query = from r in db.Reports
                                join u in db.Ubications on r.IdUbication equals u.Id
                                join s in db.Statuses on r.IdStatus equals s.Id
                                join us in db.Users on r.IdUser equals us.Id
                                where r.IsDeleted == false
                                select new ReportRequest { 
                                    Id = r.Id,
                                    Asunto = r.Asunto,
                                    Descripcion = r.Descripcion,
                                    Foto = r.Foto,
                                    Ubicacion = u.Ubication1,
                                    Estatus = s.Status1,
                                    CreatedDate=r.CreatedDate,
                                    ModifiedDate=r.ModifiedDate,
                                    IsDeleted=r.IsDeleted,
                                    Latitud=u.Latitud,
                                    Longitud=u.Longitud,
                                    ImageUbicaton=r.ImageUbicaton,
                                    IdUser=r.IdUser,
                                    User=us.UserName
                                   

                                };


                    var lst = query.ToList();
                    //var lst = db.Reports.ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
           
        }
        [HttpGet("GetAllByUser")]
        //Creamos el método para traer los datos en formato JSON
        public IActionResult GetAllByUser(int uid)
        {
            Respuesta<List<ReportRequest>> oRespuesta = new Respuesta<List<ReportRequest>>();
            try
            {
                using (AWProyectoContext db = new AWProyectoContext())
                {
                    var query = from r in db.Reports
                                join u in db.Ubications on r.IdUbication equals u.Id
                                join s in db.Statuses on r.IdStatus equals s.Id
                                join us in db.Users on r.IdUser equals us.Id
                                where r.IsDeleted == false && r.IdUser == uid
                                select new ReportRequest
                                {
                                    Id = r.Id,
                                    Asunto = r.Asunto,
                                    Descripcion = r.Descripcion,
                                    Foto = r.Foto,
                                    Ubicacion = u.Ubication1,
                                    Estatus = s.Status1,
                                    CreatedDate = r.CreatedDate,
                                    ModifiedDate = r.ModifiedDate,
                                    IsDeleted = r.IsDeleted,
                                    Latitud = u.Latitud,
                                    Longitud = u.Longitud,
                                    ImageUbicaton = r.ImageUbicaton,
                                    IdUser = r.IdUser,
                                    User = us.Name

                                };


                    var lst = query.ToList();
                    //var lst = db.Reports.ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);

        }
        [HttpGet("{Id}")]
        //Creamos el método para traer los datos en formato JSON
        public IActionResult Get(int Id)
        {
            Respuesta<ReportRequest> oRespuesta = new Respuesta<ReportRequest>(); //esto se modifico
            try
            {
                using (AWProyectoContext db = new AWProyectoContext())
                {
                    var query = from r in db.Reports
                                
                                join u in db.Ubications on r.IdUbication equals u.Id
                                join s in db.Statuses on r.IdStatus equals s.Id
                                join us in db.Users on r.IdUser equals us.Id
                                where r.Id == Id

                                select new ReportRequest 
                                {
                                    Id = r.Id,
                                    Asunto = r.Asunto,
                                    Descripcion = r.Descripcion,
                                    Foto = r.Foto,
                                    IdUbicacion = r.IdUbication,
                                    Ubicacion = u.Ubication1,
                                    IdStatus = r.IdStatus,
                                    Estatus = s.Status1,
                                    CreatedDate = r.CreatedDate,
                                    ModifiedDate = r.ModifiedDate,
                                    IsDeleted = r.IsDeleted,
                                    Latitud = u.Latitud,
                                    Longitud = u.Longitud,
                                    ImageUbicaton = r.ImageUbicaton,
                                    IdUser = r.IdUser,
                                    User = us.Name

                                };

                    var lst = query.FirstOrDefault();
                    //var lst = db.Reports.Find(Id);
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
        [HttpPost]
        public IActionResult Add(ReportRequest model)
        {
            Respuesta<object> oRespuesta = new Respuesta<object>();
            try
            {
                using (AWProyectoContext db = new AWProyectoContext())
                {
                    
                    Ubication newUbi = new Ubication();
                    Report newRep = new Report();

                    newUbi.Ubication1 = model.Ubicacion;
                    newUbi.Latitud = model.Latitud;
                    newUbi.Longitud = model.Longitud;
                    db.Ubications.Add(newUbi);
                    db.SaveChanges();

                    
                    
                    newRep.Asunto = model.Asunto;
                    newRep.Descripcion = model.Descripcion;
                    newRep.IdUbication = newUbi.Id;
                    newRep.Foto = model.Foto;
                    newRep.ImageUbicaton = model.ImageUbicaton;
                    newRep.IdStatus = 1;
                    newRep.CreatedDate = DateTime.Now;
                    newRep.ModifiedDate = null;
                    newRep.IsDeleted = false;
                    newRep.IdUser = model.IdUser;

                    
                    db.Reports.Add(newRep);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
        [HttpPut]
        public IActionResult Editar(ReportRequest model)
        {
            Respuesta<object> oRespuesta = new Respuesta<object>();
            try
            {
                using (AWProyectoContext db = new AWProyectoContext())
                {


                    Report eRep = db.Reports.Find(model.Id);
                    Ubication eUbi = db.Ubications.Find(model.IdUbicacion);

                    eRep.IdStatus = model.IdStatus;
                    eRep.Asunto = model.Asunto;
                    eRep.Descripcion = model.Descripcion;
                    eRep.Foto = model.Foto;
                    eUbi.Ubication1 = model.Ubicacion;
                    eRep.ModifiedDate = DateTime.Now;
                    eRep.IsDeleted = false;
                    eUbi.Latitud = model.Latitud;
                    eUbi.Longitud = model.Longitud;
                    eRep.ImageUbicaton = model.ImageUbicaton;
                    db.Entry(eUbi).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    db.Entry(eRep).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            Respuesta<object> oRespuesta = new Respuesta<object>();
            try
            {
                using (AWProyectoContext db = new AWProyectoContext())
                {
                    //Report eRep = db.Reports.Find(id);

                    Report eRep = db.Reports.Find(id);
                    Ubication eUbi = db.Ubications.Find(eRep.IdUbication);

                    eUbi.IsDeleted = true;
                    eRep.IsDeleted = true;


                    db.Entry(eUbi).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    db.Entry(eRep).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    /*db.Remove(eRep);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;*/
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
    }
}
