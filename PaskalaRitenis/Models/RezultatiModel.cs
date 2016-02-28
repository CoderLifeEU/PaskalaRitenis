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
        public string Path { get; set; }
        public string Name { get; set; }
    }
}