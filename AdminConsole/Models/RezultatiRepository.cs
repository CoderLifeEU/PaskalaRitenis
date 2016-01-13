using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;

namespace AdminConsole.Models
{
    public class RezultatiRepository : IRezultatiRepository
    {
        private RezultatiDataContext _dataContext;

        public RezultatiRepository()
        {
            _dataContext = new RezultatiDataContext();
        }

        public void InsertYear(int gads)
        {
            if (!YearExists(gads))
            {
                var newGads = new Gadi()
                {
                    Gads = gads
                };
                _dataContext.Gadis.InsertOnSubmit(newGads);
                _dataContext.SubmitChanges();
            }
        }

        public IEnumerable<FormaModel> GetFormas()
        {
            IList<FormaModel> formList = new List<FormaModel>();
            var query = from f in _dataContext.RezultatuFormas
                        select f;
            var formas = query.ToList();
            foreach (var formData in formas)
            {
                formList.Add(new FormaModel()
                {
                    FormaID = formData.ID,
                    FormaXML = formData.Forma,
                    FormaNosaukums = formData.Nosaukums
                });
            }
            return formList;
        }

        public FormaModel GetFormaById(int id)
        {
            if (FormExists(id))
            {
                RezultatuForma forma;
                var query = from f in _dataContext.RezultatuFormas
                            where f.ID == id
                            select f;
                forma = query.FirstOrDefault();

                return new FormaModel()
                {
                    FormaID = forma.ID,
                    FormaNosaukums = forma.Nosaukums,
                    FormaXML = forma.Forma
                };
            }
            else return null;
        }

        public FormaModel GetAttachedForma(int year)
        {
            var query = from f in _dataContext.FormuSaites
                        where f.Gads == year
                        select f;
            var link = query.FirstOrDefault();
            if (link != null && link.FormaID > 0)
            {
                var query2 = from g in _dataContext.RezultatuFormas
                            where g.ID == link.FormaID
                            select g;
                var forma = query2.FirstOrDefault();
                FormaModel form = new FormaModel()
                {
                    FormaID = forma.ID,
                    FormaNosaukums = forma.Nosaukums,
                    FormaXML = forma.Forma
                };
                return form;
            }
            else return null;
        }

        public List<BindingModel> GetYears()
        {
            List<BindingModel> result = new List<BindingModel>();
            List<int> yearList = new List<int>();
            var query = from f in _dataContext.Gadis
                        select f.Gads;
            yearList = query.Distinct().ToList();
            foreach(var year in yearList)
            {
                if (_dataContext.FormuSaites.Where(x => x.Gads == year).SingleOrDefault() != null)
                {
                    int formaID = _dataContext.FormuSaites.Where(x => x.Gads == year).SingleOrDefault().FormaID;
                    result.Add(new BindingModel() { Gads = year, FormasNosaukums = _dataContext.RezultatuFormas.Where(x => x.ID == formaID).SingleOrDefault().Nosaukums });
                }
                else
                {
                    result.Add(new BindingModel() { Gads = year, FormasNosaukums = "" });
                }
            }

            return result;
        }

        public void InsertForm(FormaModel forma)
        {
            if (!FormExists(forma.FormaNosaukums))
            {
                var newForma = new RezultatuForma()
                {
                    Nosaukums = forma.FormaNosaukums,
                    Forma = forma.FormaXML
                };
                _dataContext.RezultatuFormas.InsertOnSubmit(newForma);
                _dataContext.SubmitChanges();
            }
        }

        public void DeleteForm(int formId)
        {
            if (FormExists(formId))
            {
                RezultatuForma forma = _dataContext.RezultatuFormas.Where(f => f.ID == formId).SingleOrDefault();
                _dataContext.RezultatuFormas.DeleteOnSubmit(forma);

                IList<FormuSaite> saite = _dataContext.FormuSaites.Where(f => f.FormaID == formId).ToList();
                if (saite.Count > 0 && saite != null)
                {
                    foreach(var link in saite)
                    {
                        _dataContext.FormuSaites.DeleteOnSubmit(link);
                    }
                }

                _dataContext.SubmitChanges();
            }
        }

        public void DeleteYear(int year)
        {
            if (YearExists(year))
            {
                Gadi gads = _dataContext.Gadis.Where(f => f.Gads == year).SingleOrDefault();
                _dataContext.Gadis.DeleteOnSubmit(gads);
                
                FormuSaite saite = _dataContext.FormuSaites.Where(f => f.Gads == year).SingleOrDefault();
                if(saite!=null)_dataContext.FormuSaites.DeleteOnSubmit(saite);

                _dataContext.SubmitChanges();
            }
        }

        public void UpdateForm(FormaModel forma)
        {
            if (FormExists(forma.FormaNosaukums))
            {
                RezultatuForma form = _dataContext.RezultatuFormas.Where(f => f.ID == forma.FormaID).SingleOrDefault();
                form.Nosaukums = forma.FormaNosaukums;
                form.Forma = forma.FormaXML;
                _dataContext.SubmitChanges();
            }
        }

        public void BindForm(int formID, int year)
        {
            if (!BindingExists(formID, year) && FormExists(formID) && YearExists(year))
            {
                if (_dataContext.FormuSaites.Where(x => x.Gads == year).SingleOrDefault() == null)
                {
                    var saite = new FormuSaite()
                    {
                        FormaID = formID,
                        Gads = year
                    };
                    _dataContext.FormuSaites.InsertOnSubmit(saite);
                    _dataContext.SubmitChanges();
                }
                else
                {
                    FormuSaite saite = _dataContext.FormuSaites.Where(f => f.Gads == year).SingleOrDefault();
                    saite.FormaID = formID;
                    _dataContext.SubmitChanges();
                }

            }
        }

        public bool YearExists(int year)
        {
            if (_dataContext.Gadis.Where(x => x.Gads == year).SingleOrDefault() == null) return false;
            else return true;

        }

        public bool FormExists(string nosaukums)
        {
            if (_dataContext.RezultatuFormas.Where(x => x.Nosaukums == nosaukums).SingleOrDefault() == null) return false;
            else return true;
        }

        public bool FormExists(int formID)
        {
            if (_dataContext.RezultatuFormas.Where(x => x.ID == formID).SingleOrDefault() == null) return false;
            else return true;
        }

        public bool BindingExists(int formID, int year)
        {
            if (_dataContext.FormuSaites.Where(x => x.FormaID == formID && x.Gads == year).SingleOrDefault() == null) return false;
            else return true;
        }
    }
}