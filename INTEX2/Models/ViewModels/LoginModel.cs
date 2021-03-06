using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// Set up a model so that the user needs to login to access the admin page

namespace INTEX2.Models.ViewModels
{
    //Login Model in order to login w/Username and Password
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
