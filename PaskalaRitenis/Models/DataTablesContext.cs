using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaskalaRitenis.Models
{
    public class DataTablesContext<T>
    {

        public int recordsTotal { get; set;}

        public int recordsFiltered { get; set; }

        public List<T> data { get; set; }

    }

    public class DataTablesParams
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string Search { get; set; }

    }
}