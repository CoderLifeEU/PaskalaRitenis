using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaskalaRitenis.Models
{
    public class TeacherRegModel : RegistrationModel
    {
        public TeacherRegModel()
        {
            RegType = RegType.Teacher;
        }
    }
}