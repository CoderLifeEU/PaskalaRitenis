using Repository.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class VideoRepository : IVideoRepository
    {
        private VideoDataContext _dataContext;
        public VideoRepository()
        {
            _dataContext = new VideoDataContext();
        }

        public IEnumerable<VideoModel> GetAllVideo()
        {
            IList<VideoModel> list = new List<VideoModel>();
            var query = from video in _dataContext.VideoCodes
                        select video;
            var info = query.ToList();
            foreach (var contactData in info)
            {
                list.Add(new VideoModel()
                {
                    Id = contactData.id,
                    Player = contactData.player,
                    Gads = contactData.gads
                });
            }
            return list;
        }

        public VideoModel GetVideoById(int videoId)
        {
            var query = from u in _dataContext.VideoCodes
                        where u.id == videoId
                        select u;
            var video = query.FirstOrDefault();
            var model = new VideoModel()
            {
                Id = videoId,
                Gads = video.gads,
                Player = video.player
            };
            return model;
        }

        public void UpdateVideo(VideoModel video)
        {
            VideoCode data = _dataContext.VideoCodes.Where(u => u.id == video.Id).SingleOrDefault();
            data.gads = video.Gads;
            data.player = video.Player;
            _dataContext.SubmitChanges();
        }

        public void InsertVideo(VideoModel video)
        {
            var data = new VideoCode()
            {
                gads = video.Gads,
                player = video.Player
            };
            _dataContext.VideoCodes.InsertOnSubmit(data);
            _dataContext.SubmitChanges();
        }

        public void DeleteVideo(int id)
        {
            VideoCode video = _dataContext.VideoCodes.Where(u => u.id == id).SingleOrDefault();
            _dataContext.VideoCodes.DeleteOnSubmit(video);
            _dataContext.SubmitChanges();
        }
    }
}