using System.Collections.Generic;
using System.Data.Objects;
using System.IO;
using System.Linq;

namespace PaskalaRitenis.Models
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

        private bool YearExistsByYear(int year)
        {
            if (_dataContext.Archives.Where(x => x.Year == year).Count() > 0) return true;
            else return false;
        }

        public IEnumerable<Archive> GetYearsByYearValue(int year)
        {
            try
            {
                if (YearExistsByYear(year))
                {
                    var query = from f in _dataContext.Archives
                                where f.Year == year
                                select f;
                    var gadi = query.ToList();
                    return gadi;
                }
            }
            catch { }
            return null;
        }

        public IEnumerable<ArchiveModel> GetArchive()
        {
            List<ArchiveModel> result = new List<ArchiveModel>();
            List<RezultatiModel> arhivetieGadi = GetYears().Where(x => x.Arhivets).Distinct().ToList();
            foreach (RezultatiModel gads in arhivetieGadi)
            {
                var years = GetYearsByYearValue(gads.Gads).Distinct();
                List<FileModel> tempList = new List<FileModel>();
                foreach (var y in years)
                {
                    tempList.Add(new FileModel()
                    {
                        Id = y.ID,
                        FileName = y.FileName
                    });
                }
                result.Add(new ArchiveModel()
                    {
                        Year = gads.Gads,
                        FileNames = tempList
                    });
            }
            return result;
        }

        public FileModel GetArchiveFileById(int id)
        {
            var result = _dataContext.Archives.Where(x => x.ID == id).FirstOrDefault();
            return new FileModel { Id = result.ID, FileName = result.FileName };
        }

        public RezultatiModel GetResultById(int id)
        {
            var result = _dataContext.Rezultatis.Where(x => x.Publicets && x.ID == id).FirstOrDefault();
            return new RezultatiModel()
                {
                    RezultatsID = result.ID,
                    Gads = result.Gads,
                    RezultatiLink = result.RezultatiLink,
                    Publicets = result.Publicets,
                    Arhivets = result.Arhivets
                };
        }
    }

    public interface IRezultatiRepository
    {
        string InsertYear(RezultatiModel gads);
        IEnumerable<RezultatiModel> GetYears();
        string UpdateYear(RezultatiModel gads);
        IEnumerable<ArchiveModel> GetArchive();
        string DeleteYear(int year);
        FileModel GetArchiveFileById(int id);
        RezultatiModel GetResultById(int id);
    }
}