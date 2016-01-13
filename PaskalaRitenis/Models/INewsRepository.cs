using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaskalaRitenis.Models
{
    public interface INewsRepository
    {
        IEnumerable<NewsModel> GetNews();
    }
}
