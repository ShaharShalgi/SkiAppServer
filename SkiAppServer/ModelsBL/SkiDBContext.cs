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
        public List<Review>? GetReviewsByuserID(int Id)
        {
            return this.Reviews.Where(u => u.SenderId == Id).ToList();
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
        public bool ToggleUserAdmin(int userId, bool isAdmin)
        {
            try
            {
                Visitor? user = this.Visitors.FirstOrDefault(u => u.UserId == userId);
                if (user == null)
                {
                    return false;
                }

                user.IsAdmin = isAdmin;
                this.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUserById(int userId)
        {
            try
            {
                Visitor? user = this.Visitors.FirstOrDefault(u => u.UserId == userId);
                Professional? pro = this.Professionals.FirstOrDefault(u => u.UserId == userId);
                if (user == null)
                {
                    return false;
                }
                List<Review>? reviews = this.Reviews.Where(u => u.SenderId == userId).ToList();
                List<Review>? reviews2 = this.Reviews.Where(u => u.RecieverId== userId).ToList();
                List<PostPhoto>? postPhotos = this.PostPhotos.Where(u => u.UserId== userId).ToList();
                List<ReviewPhoto>? reviewPhotos = new List<ReviewPhoto>();

                this.Visitors.Remove(user);
                if (pro != null)
                {
                    this.Professionals.Remove(pro);
                }
                if (reviews != null)
                {
                    foreach (Review r in reviews)
                    {
                        List<ReviewPhoto>? temp = GetReviewPhotos(r.ReviewId);
                        foreach(ReviewPhoto p in temp) 
                        { 
                            reviewPhotos.Add(p);
                        }
                       
                        this.Reviews.Remove(r);
                    }
                }
                if (reviews2 != null)
                {
                    foreach (Review r in reviews2)
                    {
                        this.Reviews.Remove(r);
                    }
                }
                if (postPhotos != null)
                {
                    foreach (PostPhoto p in postPhotos)
                    {
                        this.PostPhotos.Remove(p);
                    }
                }
                if (reviewPhotos != null)
                {
                    foreach (ReviewPhoto p in reviewPhotos)
                    {
                        this.ReviewPhotos.Remove(p);
                    }
                }
                this.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
