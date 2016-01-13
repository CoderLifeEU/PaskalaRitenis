namespace AdminConsole.Models
{
    public class FormaModel
    {
        public int FormaID { get; set; }
        public string FormaNosaukums { get; set; }
        public string FormaXML { get; set; }
    }

    public class GadsModel
    {
        public int GadsID { get; set; }
        public int GadsValue { get; set; }
    }

    public class FormuSaiteModel
    {
        public int ID { get; set; }
        public int Gads { get; set; }
        public int FormaID { get; set; }
    }

    public class RezultatsModel
    {
        public int RezultatsID { get; set; }
        public int RegistracijasID { get; set; }
        public string Rezultats { get; set; }
        public char VietaAtziniba { get; set; }
        public bool NeieradasDiskvalificets { get; set; }
        public int RezultatuSaite { get; set; }
    }

    public class BindingModel
    {
        public int Gads { get; set; }
        public string FormasNosaukums { get; set; }
    }
}