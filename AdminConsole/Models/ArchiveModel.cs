using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminConsole.Models
{
    public class ArchiveModel
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int FileId { get; set; }
    }

    public class ArchiveFileModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
    }
}