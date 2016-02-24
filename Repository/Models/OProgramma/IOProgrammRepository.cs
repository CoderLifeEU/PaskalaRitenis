using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Models.OProgramma
{
    public interface IOProgrammRepository
    {
        IEnumerable<OProgrammModel> GetProgramm();
        OProgrammModel GetProgrammById(int programmId);
        void InsertProgramm(OProgrammModel programm);
        void DeleteProgramm(int programmId);
        void UpdateProgramm(OProgrammModel programm);
    }
}
