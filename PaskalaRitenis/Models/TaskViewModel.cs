using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaskalaRitenis.Models
{
    public class TaskViewModel
    {

        public TaskViewModel()
        {
            this.GetTypes();
        }

        public TaskViewModel(List<RezultatiModel> years)
        {
            SetYears(years);
            this.GetTypes();
        }

        public void GetTypes()
        {
            Types = ((IEnumerable<FileType>)Enum.GetValues(typeof(FileType))).Select(c => new SelectListItem() { Value = ((int)c).ToString(), Text = c.ToString() }).ToList();
        }

        public void SetYears(List<RezultatiModel> years)
        {
            this.Years = years.Select(x => new SelectListItem() { Value = x.Gads.ToString(), Text = x.Gads.ToString() }).ToList();
        }

        public List<SelectListItem> Types { get; set; }
        public List<SelectListItem> Years { get; set; }

        [Display(Name = "Faila tips:")]
        [Required(ErrorMessage = "'Faila tips' ir obligāts lauks", AllowEmptyStrings = false)]
        public int Type { get; set; }

        [Display(Name = "Gads:")]
        [Required(ErrorMessage = "'Gads' ir obligāts lauks", AllowEmptyStrings = false)]
        public int Year { get; set; }

        [Display(Name = "Fails:")]
        [Required(ErrorMessage = "'Fails' ir obligāts lauks", AllowEmptyStrings = false)]
        public HttpPostedFileBase TaskFile { get; set; }
    }
}