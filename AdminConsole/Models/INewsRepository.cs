using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminConsole.Models
{
    public interface INewsRepository
    {
        IEnumerable<NewsModel> GetNews();
        NewsModel GetNewsById(int newsId);
        void InsertNews(NewsModel news);
        void DeleteNews(int newsId);
        void UpdateNews(NewsModel news);
    }
}
