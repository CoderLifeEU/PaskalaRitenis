using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaskalaRitenis.Models
{
    public class HomePageModel
    {
        /// <summary>
        /// News List 
        /// </summary>
        public IEnumerable<NewsModel> News { get; set; }

        /// <summary>
        /// Supporters List
        /// </summary>
        public IEnumerable<Supporter> Supporters { get; set; }
    }
}