using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaskalaRitenis.Models
{
    public class UserViewModel
    {
        public int ID;

        [Required(ErrorMessage = "Lietotāja vārds ir obligats lauks")]
        [DisplayName("Lietotāja vārds")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Parole ir obligats lauks")]
        [DisplayName("Parole")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Atkartota parole ir obligats lauks")]
        [Compare("Password", ErrorMessage = "Atkartota parole nesakrīt")]
        [DisplayName("Atkartota parole")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Uzvārds ir obligats lauks")]
        [DisplayName("Uzvārds")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Vārds ir obligats lauks")]
        [DisplayName("Vārds")]
        public string FirstName { get; set; }

        [DisplayName("Loma")]
        public int Role { get; set; }
    }
}