using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminConsole.Models
{
    public class ArchiveRepository : IArchiveRepository
    {
        private ArchiveDataContext _dataContext;

        public ArchiveRepository()
        {
            _dataContext = new ArchiveDataContext();
        }

        public string InsertFile(ArchiveModel file)
        {
            if (_dataContext.Archives.Where(x => x.FileName == file.FileName && x.Year == file.Year).FirstOrDefault() == null)
            {
                try
                {
                    _dataContext.Archives.InsertOnSubmit(new Archive()
                    {
                        FileName = file.FileName,
                        Year = file.Year
                    });
                    _dataContext.SubmitChanges();
                    return "Fails piesaistīts";
                }
                catch
                {
                    return "Kļūda DB apstrādē";
                }
            }
            else return "Fails jau ir piesaistīts!";
        }

        public IEnumerable<ArchiveModel> GetYears()
        {
            IList<ArchiveModel> yearList = new List<ArchiveModel>();
            var query = from f in _dataContext.Archives
                        select f;
            var gadi = query.ToList();
            foreach (var gadiData in gadi)
            {
                yearList.Add(new ArchiveModel()
                {
                    Id = gadiData.ID,
                    Year = gadiData.Year,
                    FileName = gadiData.FileName
                });
            }
            return yearList;
        }

        public ArchiveModel GetYearById(int id)
        {
            var query = from f in _dataContext.Archives
                        where f.ID == id
                        select f;
            var gads = query.FirstOrDefault();

            return new ArchiveModel
            {
                Id = gads.ID,
                Year = gads.Year,
                FileName = gads.FileName
            };
        }

        public IEnumerable<ArchiveModel> GetYearsByYearValue(int year)
        {
            try
            {
                if (YearExistsByYear(year))
                {
                    var query = from f in _dataContext.Archives
                                where f.Year == year
                                select f;
                    var gadi = query.ToList();
                    List<ArchiveModel> result = new List<ArchiveModel>();
                    foreach (var x in gadi)
                    {
                        result.Add(new ArchiveModel()
                        {
                            Id = x.ID,
                            Year = x.Year,
                            FileName = x.FileName
                        });
                    }
                    return result;
                }
            }
            catch { }
            return null;
        }

        public string UpdateYear(ArchiveModel gads)
        {
            try
            {
                if (YearExistsByYear(gads.Id))
                {
                    Archive existingYear = _dataContext.Archives.Where(f => f.ID == gads.Id).SingleOrDefault();
                    existingYear.Year = gads.Year;
                    existingYear.FileName = gads.FileName;
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

        public string DeleteYearById(int id)
        {
            try
            {
                if (YearExistsById(id))
                {
                    Archive gads = _dataContext.Archives.Where(f => f.ID == id).SingleOrDefault();
                    _dataContext.Archives.DeleteOnSubmit(gads);
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

        public string DeleteYearByYearNumber(int year)
        {
            try
            {
                if (YearExistsByYear(year))
                {
                    var gads = _dataContext.Archives.Where(f => f.Year == year).ToList();
                    foreach (var g in gads)
                    {
                        _dataContext.Archives.DeleteOnSubmit(g);
                    }
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

        public IEnumerable<Archive> GetFilesExceptYear(int year)
        {
            try
            {
                if (YearExistsByYear(year))
                {
                    var query = from f in _dataContext.Archives
                                where f.Year != year
                                select f;
                    var faili = query.ToList();
                    return faili;
                }
            }
            catch { }
            return null;
        }

        private bool YearExistsByYear(int year)
        {
            if (_dataContext.Archives.Where(x => x.Year == year).Count() > 0) return true;
            else return false;
        }

        private bool YearExistsById(int id)
        {
            if (_dataContext.Archives.Where(x => x.ID == id).Count() > 0) return true;
            else return false;
        }

        private bool FileExistsOnDb(string name)
        {
            if (_dataContext.Archives.Where(x => x.FileName == name).Count() > 0) return true;
            else return false;
        }

        public string DeleteFileOnDb(string fileName)
        {
            try
            {
                if (FileExistsOnDb(fileName))
                {
                    var file = _dataContext.Archives.Where(f => f.FileName == fileName).ToList();
                    foreach (var x in file)
                    {
                        _dataContext.Archives.DeleteOnSubmit(x);
                    }
                    _dataContext.SubmitChanges();
                    return "Fails nodzēsts";
                }
                else return "Šāds fails neeksistē";
            }
            catch
            {
                return "Kļūda DB apstrādē";
            }
        }

    }

    public interface IArchiveRepository
    {
        IEnumerable<ArchiveModel> GetYears();
        ArchiveModel GetYearById(int id);
        IEnumerable<ArchiveModel> GetYearsByYearValue(int year);
        string UpdateYear(ArchiveModel gads);
        string DeleteYearById(int id);
        IEnumerable<Archive> GetFilesExceptYear(int year);
        string InsertFile(ArchiveModel file);
        string DeleteFileOnDb(string fileName);
        string DeleteYearByYearNumber(int year);
    }
}