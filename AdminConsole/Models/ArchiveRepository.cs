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

        public string InsertYear(ArchiveModel year)
        {
            try
            {
                var rezultats = new Archive()
                {
                    Year = year.Year,
                    FileId = year.FileId
                };

                _dataContext.Archives.InsertOnSubmit(rezultats);
                _dataContext.SubmitChanges();
                return "Gads pievienots";
            }
            catch
            {
                return "Kļūda DB apstrādē";
            }
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
                    Id = gadiData.Id,
                    Year = gadiData.Year,
                    FileId = gadiData.FileId
                });
            }
            return yearList;
        }

        public ArchiveModel GetYearById(int id)
        {
            var query = from f in _dataContext.Archives
                        where f.Id == id
                        select f;
            var gads = query.FirstOrDefault();

            return new ArchiveModel
            {
                Id = gads.Id,
                Year = gads.Year,
                FileId = gads.FileId
            };
        }

        public string UpdateYear(ArchiveModel gads)
        {
            try
            {
                if (YearExistsByYear(gads.Id))
                {
                    Archive existingYear = _dataContext.Archives.Where(f => f.Id == gads.Id).SingleOrDefault();
                    existingYear.Year = gads.Year;
                    existingYear.FileId = gads.FileId;
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
                if (YearExistsById(id))
                {
                    Archive gads = _dataContext.Archives.Where(f => f.Id == id).SingleOrDefault();
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

        public IEnumerable<ArchiveFile> GetFilesByYear(int year)
        {
            try
            {
                if (YearExistsByYear(year))
                {
                    var query = from f in _dataContext.Archives
                                where f.Year == year
                                select f;
                    var gads = query.FirstOrDefault();

                    return gads.ArchiveFiles;
                }
            }
            catch { }
            return null;
        }

        private bool YearExistsByYear(int year)
        {
            if (_dataContext.Archives.Where(x => x.Year == year).SingleOrDefault() == null) return false;
            else return true;
        }

        private bool YearExistsById(int id)
        {
            if (_dataContext.Archives.Where(x => x.Id == id).SingleOrDefault() == null) return false;
            else return true;
        }

        private bool FileExistsOnServer(string fileName)
        {
            if (File.Exists(fileName)) return false;
            else return true;
        }

        private bool FileExistsOnDb(string fileName)
        {
            if (_dataContext.ArchiveFiles.Where(x => x.FileName == fileName).SingleOrDefault() == null) return false;
            else return true;
        }

        private bool FileExistsOnDb(int id)
        {
            if (_dataContext.ArchiveFiles.Where(x => x.Id == id).SingleOrDefault() == null) return false;
            else return true;
        }

        public string InsertFile(ArchiveFileModel file)
        {
            try
            {
                _dataContext.ArchiveFiles.InsertOnSubmit(new ArchiveFile()
                {
                    FileName = file.FileName
                });
                _dataContext.SubmitChanges();
                return "Fails pievienots";
            }
            catch
            {
                return "Kļūda DB apstrādē";
            }
        }

        public string DeleteFileOnDb(int id)
        {
            try
            {
                if (FileExistsOnDb(id))
                {
                    ArchiveFile file = _dataContext.ArchiveFiles.Where(f => f.Id == id).SingleOrDefault();
                    _dataContext.ArchiveFiles.DeleteOnSubmit(file);
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
        string InsertYear(ArchiveModel year);
        IEnumerable<ArchiveModel> GetYears();
        ArchiveModel GetYearById(int id);
        string UpdateYear(ArchiveModel gads);
        string DeleteYear(int id);
        IEnumerable<ArchiveFile> GetFilesByYear(int year);
        string InsertFile(ArchiveFileModel file);
        string DeleteFileOnDb(int id);
    }
}