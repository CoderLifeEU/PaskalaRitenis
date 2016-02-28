using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminConsole.Models
{
    public class RegistrationModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PersonCode { get; set; }

        public string City { get; set; }

        public string StudyType { get; set; }

        public string SelectedKlass { get; set; }

        public string School { get; set; }

        public string IsSelected { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Advicer { get; set; }  

        public string PlaceRequired { get; set; }

        public string PlaceRequiredType { get; set; }
    }
}