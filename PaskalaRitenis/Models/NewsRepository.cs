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
            var news = query.OrderByDescending(a => a.CreatedDate).ToList();
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

        public List<NewsModel> GetNews(int start, int pageSize, string search)
        {
            List<Zinja> news = new List<Zinja>();
            news = _dataContext.Zinjas.Where(x=>x.Info.Contains(search)).Skip(start).Take(pageSize).ToList();

            List<NewsModel> newsList = new List<NewsModel>();
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

        public int CountNews(string search)
        {
            int count = 0;
            count = _dataContext.Zinjas.Where(x => x.Info.Contains(search)).Count();
            return count;
        }

        public NewsModel GetNewsById(int newsId)
        {
            var query = from u in _dataContext.Zinjas
                        where u.Id == newsId
                        select u;
            var news = query.FirstOrDefault();
            var model = new NewsModel()
            {
                Id = newsId,
                Info = news.Info,
            };
            return model;
        }

        public void InsertNews(NewsModel news)
        {
            var newsData = new Zinja()
            {
                Info = news.Info,
            };
            _dataContext.Zinjas.InsertOnSubmit(newsData);
            _dataContext.SubmitChanges();
        }

        public void DeleteNews(int newsId)
        {
            Zinja news = _dataContext.Zinjas.Where(u => u.Id == newsId).SingleOrDefault();
            _dataContext.Zinjas.DeleteOnSubmit(news);
            _dataContext.SubmitChanges();
        }

        public void UpdateNews(NewsModel news)
        {
            Zinja newsData = _dataContext.Zinjas.Where(u => u.Id == news.Id).SingleOrDefault();
            newsData.Info = news.Info;
            _dataContext.SubmitChanges();
        }

    }
}