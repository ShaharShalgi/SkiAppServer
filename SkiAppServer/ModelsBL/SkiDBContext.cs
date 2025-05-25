using Microsoft.EntityFrameworkCore;
using SkiAppServer.Models;

namespace SkiAppServer.Models
{
    public partial class SkiDBContext : DbContext
    {
       

        public Visitor? GetVisitor(string password)
        {
            return this.Visitors.Where(u => u.Pass == password).FirstOrDefault();
        }
        public Visitor? GetVisitorById(int Id)
        {
            return this.Visitors.Where(u => u.UserId == Id).FirstOrDefault();
        }
        public bool? upload(int senderId, int reciverId)
        {
            if (this.Reviews.Where(u => u.SenderId == senderId && u.RecieverId == reciverId).FirstOrDefault() != null)
                return true;
            return false;
        }
        public Review? GetReviewById(int Id)
        {
            return this.Reviews.Where(u => u.ReviewId == Id).FirstOrDefault();
        }
        public List<Tip>? GetAllTips()
        {
            return this.Tips.ToList();
        }
        public List<Professional>? GetAllCoaches()
        {
            return this.Professionals.Where(u => u.TypeId == 1 && u.Post == true).ToList();
        }
        public List<Professional>? GetAllResorts()
        {
            return this.Professionals.Where(u => u.TypeId == 2 && u.Post == true).ToList();
        }
        public List<Review>? GetReviewsByProID(int Id)
        {
            return this.Reviews.Where(u => u.RecieverId == Id).ToList();
        }

        public List<Visitor>? GetAllVisitors()
        {
            return this.Visitors.ToList();
        }
        public List<PostPhoto>? GetAllPostPhotos()
        {
            return this.PostPhotos.ToList();
        }
        public List<ReviewPhoto>? GetAllReviewPhotos()
        {
            return this.ReviewPhotos.ToList();
        }
        public List<Tip>? GetTipsByDifficulty(int diff)
        {
            return this.Tips.Where(u => u.Difficulty == diff).ToList();
        }
        public List<PostPhoto>? GetPostPhotos(int posterId)
        {
            return this.PostPhotos.Where(u => u.UserId == posterId).ToList();
        }
        public List<ReviewPhoto>? GetReviewPhotos(int reviewId)
        {
            return this.ReviewPhotos.Where(u => u.ReviewId == reviewId).ToList();
        }
        public Professional? GetPro(int Id)
        {
            return this.Professionals.Where(u => u.UserId == Id).FirstOrDefault();
        }
        public List<Professional>? GetPostByPriceCoachASC()
        {
            return this.Professionals.Where(u => u.Post == true && u.TypeId == 1).OrderBy(u => u.Price).ToList();
        }
        public List<Professional>? GetPostByPriceCoachDESC()
        {
            return this.Professionals.Where(u => u.Post == true && u.TypeId == 1).OrderByDescending(u => u.Price).ToList();
        }
        public List<Professional>? GetPostByRatingCoachDESC()
        {
            return this.Professionals.Where(u => u.Post == true && u.TypeId == 1).OrderByDescending(u => u.Rating).ToList();
        }
        public List<Professional>? GetPostByRatingCoachASC()
        {
            return this.Professionals.Where(u => u.Post == true && u.TypeId == 1).OrderBy(u => u.Rating).ToList();
        }
    }
}
