using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
   public class AppUser: Microsoft.AspNetCore.Identity.IdentityUser,IEntity
    {
        public string Roles { get; set; }
    }
}
