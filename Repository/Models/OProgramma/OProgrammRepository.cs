using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Models.OProgramma
{
    public class OProgrammRepository : IOProgrammRepository
    {
        private OProgrammaDataContext _dataContext;
        public OProgrammRepository()
        {
            _dataContext = new OProgrammaDataContext();
        }

        public IEnumerable<OProgrammModel> GetProgramm()
        {
            IList<OProgrammModel> programmList = new List<OProgrammModel>();
            var query = from programma in _dataContext.OlimpProgrammas
                        select programma;
            var programms = query.ToList();
            foreach (var programData in programms)
            {
                programmList.Add(new OProgrammModel()
                {
                    Id = programData.Id,
                    Programma = programData.Programma
                });
            }
            return programmList;
        }

        public OProgrammModel GetProgrammById(int programmId)
        {
            var query = from p in _dataContext.OlimpProgrammas
                        where p.Id == programmId
                        select p;
            var programm = query.FirstOrDefault();
            var model = new OProgrammModel()
            {
                Id = programmId,
                Programma = programm.Programma
            };
            return model;
        }

        public void InsertProgramm(OProgrammModel programm)
        {
            var programmData = new OlimpProgramma()
            {
                Programma = programm.Programma,
            };
            _dataContext.OlimpProgrammas.InsertOnSubmit(programmData);
            _dataContext.SubmitChanges();
        }

        public void DeleteProgramm(int programmId)
        {
            OlimpProgramma programm = _dataContext.OlimpProgrammas.Where(p => p.Id == programmId).SingleOrDefault();
            _dataContext.OlimpProgrammas.DeleteOnSubmit(programm);
            _dataContext.SubmitChanges();
        }

        public void UpdateProgramm(OProgrammModel programm)
        {
            OlimpProgramma programmData = _dataContext.OlimpProgrammas.Where(p => p.Id == programm.Id).SingleOrDefault();
            programmData.Programma = programm.Programma;
            _dataContext.SubmitChanges();
        } 
    }
}
