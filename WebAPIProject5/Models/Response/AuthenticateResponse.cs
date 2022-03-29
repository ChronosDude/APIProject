using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject5.Models;

namespace WebAPIProject5.Models.Response
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        
        public string Rolename { get; set; }
        public string? Token { get; set; }

        public AuthenticateResponse(UserSend user, string token)
        {
            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
            Name = user.Name;
            LastName = user.LastName;
            
            Rolename = user.Rolename;
            Token = token;
        }
    }
}
