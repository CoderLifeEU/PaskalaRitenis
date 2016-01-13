using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Models
{
    public interface IVideoRepository
    {
        IEnumerable<VideoModel> GetAllVideo();
        VideoModel GetVideoById(int contactId);
        void UpdateVideo(VideoModel video);
        void InsertVideo(VideoModel video);
        void DeleteVideo(int id);
    }
}
