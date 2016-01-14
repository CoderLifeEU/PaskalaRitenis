using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaskalaRitenis.Models
{
    public class NewsRepository : INewsRepository
    {
        private NewsDataContext _dataContext;
        public NewsRepository()
        {
            _dataContext = new NewsDataContext();
        }
        public IEnumerable<NewsModel> GetNews()
        {
            IList<NewsModel> newsList = new List<NewsModel>();
            var query = from zinjas in _dataContext.Zinjas
                        select zinjas;
            var news = query.ToList();
            foreach (var newsData in news)
            {
                newsList.Add(new NewsModel()
                {
                    Id = newsData.Id,
                    Info = newsData.Info,
                });
            }
            return newsList;
        }
    }
}