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

            StudyYear = new List<SelectListItem>();
            StudyYear.Add(new SelectListItem { Value = "1", Text = "6. klase" });
            StudyYear.Add(new SelectListItem { Value = "2", Text = "7. klase" });
            StudyYear.Add(new SelectListItem { Value = "3", Text = "8. klase" });
            StudyYear.Add(new SelectListItem { Value = "4", Text = "9. klase" });
            StudyYear.Add(new SelectListItem { Value = "5", Text = "10. klase" });
            StudyYear.Add(new SelectListItem { Value = "6", Text = "11. klase" });
            StudyYear.Add(new SelectListItem { Value = "7", Text = "12. klase" });

            StudyType = new List<SelectListItem>();
            StudyType.Add(new SelectListItem { Value = "1", Text = "Vispārējās izglītības iestāde" });
            StudyType.Add(new SelectListItem { Value = "2", Text = "Profesionālo vidējo izglītības iestāde" });

            PlaceRequired = new List<SelectListItem>();
            PlaceRequired.Add(new SelectListItem { Value = "1", Text = "Ne" });
            PlaceRequired.Add(new SelectListItem { Value = "2", Text = "Ja" });
        }

        public RegType RegType { get; set; }

        [Display(Name = "Vārds:")]
        public string Name { get; set; }

        [Display(Name = "Uzvārds:")]
        public string Surname { get; set; }

        public string PersonCode { get; set; }

        [Display(Name = "Pilsēta/Novads:")]
        public string City { get; set; }

        [Display(Name = "Skola:")]
        public List<SelectListItem> School { get; set; }

        public int SelectedSchoolId { get; set; }

        public string SpecialSchool { get; set; }

        [Display(Name = "Telefons:")]
        public string Phone { get; set; }

        [Display(Name = "E­-pasts:")]
        public string Email { get; set; }

        [Display(Name = "Skolotājs/skolotāji:")]
        public string Advicer { get; set; }

        [Display(Name = "Mācību iestādes tips:")]
        public List<SelectListItem> StudyType { get; set; }

        public int SelectedStudyTypeId { get; set; }

        [Display(Name = "Kurss:")]
        public List<SelectListItem> StudyYear { get; set; }

        public int SelectedStudyYear { get; set; }

        [Display(Name = "Vai būs nepieciešamā kopmītne?")]
        public List<SelectListItem> PlaceRequired { get; set; }

        public string PlaceRequiredId { get; set; }

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