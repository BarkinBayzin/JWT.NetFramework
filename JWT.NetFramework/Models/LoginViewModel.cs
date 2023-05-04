using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JWT.NetFramework.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User Name can't be empty")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password can't be empty")]
        public string Password { get; set; }
    }
}