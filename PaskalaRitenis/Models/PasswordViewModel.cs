using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaskalaRitenis.Models
{
    public class PasswordViewModel
    {
        public PasswordViewModel() { }

        public PasswordViewModel(User domain)
        {
            this.ID = domain.ID;
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "Parole ir obligats lauks")]
        [DisplayName("Parole")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Atkartota parole ir obligats lauks")]
        [Compare("Password", ErrorMessage = "Atkartota parole nesakrīt")]
        [DisplayName("Atkartota parole")]
        public string ConfirmPassword { get; set; }

    }
}