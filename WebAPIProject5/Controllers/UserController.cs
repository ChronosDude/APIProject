using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject5.Models;
using WebAPIProject5.Models.Response;
using WebAPIProject5.Models.Request;

namespace WebAPIProject5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IActionResult Get()
        {
            Respuesta<List<UserRequest>> userRespuesta = new Respuesta<List<UserRequest>>();
            try
            {
                using (AWProyectoContext db = new AWProyectoContext())
                {
                    var query = from u in db.Users
                                join r in db.Roles on u.IdRole equals r.Id
                                where u.IsDeleted == false
                                select new UserRequest
                                {
                                    Id =u.Id,
                                    UserName=u.UserName,
                                    Email=u.Email,
                                    Name =u.Name,
                                    LastName=u.LastName,
                                    Password=u.Password,
                                     IsDeleted=u.IsDeleted,
                                    IsAdmin =u.IsAdmin,
                                    CreatedDate=u.CreatedDate,
                                     ModifiedDate=u.ModifiedDate,
                                    IdRole=u.IdRole,
                                    Rolename=r.Role1


                                };


                    var lst = query.ToList();
                    //var lst = db.Reports.ToList();
                    userRespuesta.Exito = 1;
                   userRespuesta.Data = lst;
                }
            }
            catch (Exception ex)
            {
                userRespuesta.Mensaje = ex.Message;
            }
            return Ok(userRespuesta);

        }

        [HttpGet("{Id}")]
        //Creamos el método para traer los datos en formato JSON
        public IActionResult Get(int Id)
        {
            Respuesta<UserRequest> userRespuesta = new Respuesta<UserRequest>(); //esto se modifico
            try
            {
                using (AWProyectoContext db = new AWProyectoContext())
                {
                    var query = from u in db.Users
                                join r in db.Roles on u.IdRole equals r.Id
                                where u.Id == Id
                                select new UserRequest
                                {
                                    Id = u.Id,
                                    UserName = u.UserName,
                                    Email = u.Email,
                                    Name = u.Name,
                                    LastName = u.LastName,
                                    Password = u.Password,
                                    IsDeleted = u.IsDeleted,
                                    IsAdmin = u.IsAdmin,
                                    CreatedDate = u.CreatedDate,
                                    ModifiedDate = u.ModifiedDate,
                                    IdRole = u.IdRole,

                                    Rolename = r.Role1


                                };

                    var lst = query.FirstOrDefault();
                    //var lst = db.Reports.Find(Id);
                    userRespuesta.Exito = 1;
                    userRespuesta.Data = lst;
                }
            }
            catch (Exception ex)
            {
                userRespuesta.Mensaje = ex.Message;
            }
            return Ok(userRespuesta);
        }

        [HttpPost]
        public IActionResult Add(User model)
        {
            Respuesta<object> oRespuesta = new Respuesta<object>();
            try
            {
                using (AWProyectoContext db = new AWProyectoContext())
                {

                    User newUser = new User();


                    newUser.UserName = model.UserName;
                    newUser.Email = model.Email;
                    newUser.Name = model.Name;
                    newUser.LastName = model.LastName;
                    newUser.Password = model.Password;
                    newUser.IsDeleted = false;
                    newUser.IsAdmin = false;
                    newUser.CreatedDate = DateTime.Now;
                    newUser.ModifiedDate = null;
                    newUser.IdRole = model.IdRole != 0?model.IdRole:3;


                    db.Users.Add(newUser);
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
        public IActionResult Editar(UserRequest model)
        {
            Respuesta<object> oRespuesta = new Respuesta<object>();
            try
            {
                using (AWProyectoContext db = new AWProyectoContext())
                {


                    User eRep = db.Users.Find(model.Id);
                    Role eUbi = db.Roles.Find(model.IdRole);


                    eRep.UserName = model.UserName;
                    eRep.Email = model.Email;
                    eRep.Name = model.Name;
                    eRep.LastName = model.LastName;
                    eRep.Password = model.Password;
                    eRep.IdRole = model.IdRole;
                    eRep.ModifiedDate = DateTime.Now;
                    eRep.IsDeleted = false;
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

                    User eRep = db.Users.Find(id);
                    eRep.IsDeleted = true;
                    db.Entry(eRep).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
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
