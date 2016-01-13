using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace PaskalaRitenis.Models
{

    public class SupportersModel
    {
        [XmlElement("Supporter")]
        public List<Supporter> Supporters { get; set; }
    }

    public class Supporter
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        [XmlAttribute("visible")]
        public bool Visible { get; set; }
    }

}