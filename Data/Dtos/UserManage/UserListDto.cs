using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Dtos
{
  public class UserListDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Roles { get; set; }
    }
}
