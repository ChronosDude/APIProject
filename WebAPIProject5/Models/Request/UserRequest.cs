﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPIProject5.Models.Request
{
    public partial class UserRequest
    {


        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsAdmin { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int IdRole { get; set; }

        public string Rolename { get; set; }

    }
}
