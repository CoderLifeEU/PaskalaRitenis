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

        [Display(Name = "Skolotājs/skolotāji (kas māca):")]
        public string Advicer { get; set; }

        public List<SelectListItem> StudyType { get; set; }

        public int SelectedStudyTypeId { get; set; }

        public int StudyYear { get; set; }

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