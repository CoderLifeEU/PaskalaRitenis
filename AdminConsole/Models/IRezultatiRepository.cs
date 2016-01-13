using System.Collections.Generic;
using System.Web.Mvc;

namespace AdminConsole.Models
{
    public interface IRezultatiRepository
    {
        void InsertYear(int gads);
        List<BindingModel> GetYears();
        void DeleteYear(int year);
        void InsertForm(FormaModel forma);
        IEnumerable<FormaModel> GetFormas();
        FormaModel GetFormaById(int id);
        FormaModel GetAttachedForma(int year);
        void UpdateForm(FormaModel forma);
        void DeleteForm(int formId);
        void BindForm(int formID, int year);
    }
}