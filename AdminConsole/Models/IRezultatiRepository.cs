using System.Collections.Generic;
using System.Web.Mvc;

namespace AdminConsole.Models
{
    public interface IRezultatiRepository
    {
        string InsertYear(RezultatiModel gads);
        IEnumerable<RezultatiModel> GetYears();
        string UpdateYear(RezultatiModel gads);
        string DeleteYear(int year);
    }
}