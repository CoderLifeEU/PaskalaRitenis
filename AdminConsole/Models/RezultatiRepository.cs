using System.Collections.Generic;
using System.Data.Objects;
using System.IO;
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

        public string InsertYear(RezultatiModel gads)
        {
            try
            {
                var rezultats = new Rezultati()
                {
                    Gads = gads.Gads,
                    Publicets = gads.Publicets,
                    RezultatiLink = gads.RezultatiLink,
                    Arhivets = false
                };

                _dataContext.Rezultatis.InsertOnSubmit(rezultats);
                _dataContext.SubmitChanges();
                return "Gads pievienots";
            }
            catch
            {
                return "Kļūda DB apstrādē";
            }
        }

        public IEnumerable<RezultatiModel> GetYears()
        {
            IList<RezultatiModel> yearList = new List<RezultatiModel>();
            var query = from f in _dataContext.Rezultatis
                        select f;
            var gadi = query.ToList();
            foreach (var gadiData in gadi)
            {
                yearList.Add(new RezultatiModel()
                {
                    RezultatsID = gadiData.ID,
                    Gads = gadiData.Gads,
                    RezultatiLink = gadiData.RezultatiLink,
                    Publicets = gadiData.Publicets,
                    Arhivets = gadiData.Arhivets
                });
            }
            return yearList;
        }

        public string UpdateYear(RezultatiModel gads)
        {
            try
            {
                if (IDExists(gads.RezultatsID))
                {
                    Rezultati existingYear = _dataContext.Rezultatis.Where(f => f.ID == gads.RezultatsID).SingleOrDefault();
                    existingYear.Publicets = gads.Publicets;
                    existingYear.Arhivets = gads.Arhivets;
                    _dataContext.SubmitChanges();

                    return "Dati atjaunoti";
                }
                else return "Šāds gads neeksistē";
            }
            catch
            {
                return "Kļūda DB apstrādē";
            }
        }

        public string DeleteYear(int id)
        {
            try
            {
                if (IDExists(id))
                {
                    Rezultati gads = _dataContext.Rezultatis.Where(f => f.ID == id).SingleOrDefault();
                    _dataContext.Rezultatis.DeleteOnSubmit(gads);
                    _dataContext.SubmitChanges();
                    return "Gads nodzēsts";
                }
                else return "Šāds gads neeksistē";
            }
            catch
            {
                return "Kļūda DB apstrādē";
            }
        }

        private bool YearExists(int year)
        {
            if (_dataContext.Rezultatis.Where(x => x.Gads == year).SingleOrDefault() == null) return false;
            else return true;
        }

        private bool IDExists(int id)
        {
            if (_dataContext.Rezultatis.Where(x => x.ID == id).SingleOrDefault() == null) return false;
            else return true;
        }

        private bool FileExists(string link)
        {
            if (File.Exists(link)) return false;
            else return true;
        }
    }
}