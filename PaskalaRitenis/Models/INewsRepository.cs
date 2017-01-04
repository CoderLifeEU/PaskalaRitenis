using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaskalaRitenis.Models
{
    public interface INewsRepository
    {
        IEnumerable<NewsModel> GetNews();
        List<NewsModel> GetNews(int start, int pageSize, string search);
        int CountNews(string search);
        NewsModel GetNewsById(int newsId);
        void InsertNews(NewsModel news);
        void DeleteNews(int newsId);
        void UpdateNews(NewsModel news);
    }
}
