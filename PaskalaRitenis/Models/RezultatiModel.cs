using System.Collections.Generic;
namespace PaskalaRitenis.Models
{
    public class RezultatiModel
    {
        public int RezultatsID { get; set; }
        public int Gads { get; set; }
        public bool Publicets { get; set; }
        public string RezultatiLink { get; set; }
        public bool Arhivets { get; set; }
    }

    public class FileModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
    }

    public class ArchiveModel
    {
        public int Year { get; set; }
        public List<FileModel> FileNames { get; set; }
    }
}