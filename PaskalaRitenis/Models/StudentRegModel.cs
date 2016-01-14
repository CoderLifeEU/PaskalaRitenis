using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaskalaRitenis.Models
{
    public class StudentRegModel : RegistrationModel
    {
        public StudentRegModel()
        {
            RegType = RegType.Student;
        }
    }
}