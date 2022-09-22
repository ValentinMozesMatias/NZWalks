﻿using System.ComponentModel.DataAnnotations.Schema;

namespace NZwalks.API.Models.Domain
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //Navigation

        //public List<User_Role> UserRoles{ get; set; }
    }
}
