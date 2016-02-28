using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaskalaRitenis.Models
{
    public class RegistrationModel
    {
        public RegistrationModel()
        {
            School = new List<SelectListItem>();

            StudyType = new List<SelectListItem>();
            StudyType.Add(new SelectListItem { Value = "1", Text = "Vispārējās izglītības iestāde" });
            StudyType.Add(new SelectListItem { Value = "2", Text = "Profesionālo vidējo izglītības iestāde" });
        }

        public RegType RegType { get; set; }

        [Display(Name = "Vārds:")]
        [Required(ErrorMessage = "'Vārds' ir obligāts lauks", AllowEmptyStrings = false)]
        [StringLength(20, ErrorMessage = "Pieļaujamais garums ir 20")]
        [RegularExpression(@"^[A-ZĀČĒĢĪĶĻŅŌŖŠŪŽa-zāčēģīķļņōŗšūž]+$", ErrorMessage = "Atļauts ievadīt tikai burtus")]
        public string Name { get; set; }

        [Display(Name = "Uzvārds:")]
        [Required(ErrorMessage = "'Uzvārds' ir obligāts lauks", AllowEmptyStrings = false)]
        [StringLength(20, ErrorMessage = "Pieļaujamais garums ir 20")]
        [RegularExpression(@"^[A-ZĀČĒĢĪĶĻŅŌŖŠŪŽa-zāčēģīķļņōŗšūž]+$", ErrorMessage = "Atļauts ievadīt tikai burtus")]
        public string Surname { get; set; }

        public string PersonCode { get; set; }

        [Display(Name = "Pilsēta/Novads:")]
        [Required(ErrorMessage = "'Pilsēta/Novads' ir obligāts lauks", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Pieļaujamais garums ir 50")]
        public string City { get; set; }

        [Display(Name = "Skola:")]
        public List<SelectListItem> School { get; set; }

        public int SelectedSchoolId { get; set; }

        [Display(Name = "Skola:")]
        [Required(ErrorMessage = "'Skola' ir obligāts lauks", AllowEmptyStrings = false)]
        [StringLength(200, ErrorMessage = "Pieļaujamais garums ir 200")]
        public string SpecialSchool { get; set; }

        [Display(Name = "Telefons:")]
        [Required(ErrorMessage = "'Telefons' ir obligāts lauks", AllowEmptyStrings = false)]
        [StringLength(20, ErrorMessage = "Pieļaujamais garums ir 20")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Atļauts ievadīt tikai ciparus")]        
        public string Phone { get; set; }

        [Display(Name = "E­-pasts:")]
        [Required(ErrorMessage = "'E­-pasts' ir obligāts lauks", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Pieļaujamais garums ir 50")]
        public string Email { get; set; }

        [Display(Name = "Skolotājs/skolotāji:")]
        [Required(ErrorMessage = "'Skolotājs/skolotāji' ir obligāts lauks", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Pieļaujamais garums ir 50")]
        public string Advicer { get; set; }

        [Display(Name = "Mācību iestādes tips:")]
        public List<SelectListItem> StudyType { get; set; }

        public int SelectedStudyTypeId { get; set; }

        [Display(Name = "Kurss:")]
        public string SelectedKlass { get; set; }

        [Display(Name = "Kurss:")]
        public string SelectedKurss { get; set; }

        [Display(Name = "Vai būs nepieciešama kopmītne?")]
        public string PlaceRequired { get; set; }

        public string PlaceRequiredType { get; set; }

        [Display(Name = "Ar drošības noteikumiem iepazinos un piekrītu")]
        public bool Sequrity { get; set; }
    }

    public enum RegType
    {
        Student,

        Teacher
    }

    public enum StudyType
    {
        School,

        Other
    }

    public enum PlaceRequired
    {
        No,

        Yes
    }
}