using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaskalaRitenis.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Lietotāja Vārds:")]
        [Required(ErrorMessage = "'Lietotāja Vārds' ir obligāts lauks", AllowEmptyStrings = false)]
        [StringLength(20, ErrorMessage = "Pieļaujamais garums ir 20")]
        public string Username { get; set; }

        [Display(Name = "Parole:")]
        [Required(ErrorMessage = "'Parole' ir obligāts lauks", AllowEmptyStrings = false)]
        [StringLength(20, ErrorMessage = "Pieļaujamais garums ir 20")]
        public string Password { get; set; }
    }
}