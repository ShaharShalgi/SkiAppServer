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
       
        public List<ח>? GetAllPostPhotos()
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
        public List<ח>? GetPostPhotos(int posterId)
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
                // Use a transaction to ensure all operations succeed or fail together
                using var transaction = this.Database.BeginTransaction();

                try
                {
                    Visitor? user = this.Visitors.FirstOrDefault(u => u.UserId == userId);
                    if (user == null)
                    {
                        return false;
                    }

                    // Get all related data first
                    Professional? pro = this.Professionals.FirstOrDefault(u => u.UserId == userId);
                    List<Review> reviewsSent = this.Reviews.Where(u => u.SenderId == userId).ToList();
                    List<Review> reviewsReceived = this.Reviews.Where(u => u.RecieverId == userId).ToList();
                    List<ח> postPhotos = this.PostPhotos.Where(u => u.UserId == userId).ToList();

                    List<ReviewPhoto> reviewPhotos = new List<ReviewPhoto>();

                    // Collect all review photos from sent reviews
                    foreach (Review r in reviewsSent)
                    {
                        List<ReviewPhoto> temp = GetReviewPhotos(r.ReviewId);
                        reviewPhotos.AddRange(temp);
                    }

                    // Collect all review photos from received reviews
                    foreach (Review r in reviewsReceived)
                    {
                        List<ReviewPhoto> temp = GetReviewPhotos(r.ReviewId);
                        reviewPhotos.AddRange(temp);
                    }

                    // Delete in the correct order (children first, then parents)

                    // 1. Delete review photos first
                    if (reviewPhotos.Any())
                    {
                        this.ReviewPhotos.RemoveRange(reviewPhotos);
                    }

                    // 2. Delete post photos
                    if (postPhotos.Any())
                    {
                        this.PostPhotos.RemoveRange(postPhotos);
                    }

                    // 3. Delete reviews (both sent and received)
                    if (reviewsSent.Any())
                    {
                        this.Reviews.RemoveRange(reviewsSent);
                    }
                    if (reviewsReceived.Any())
                    {
                        this.Reviews.RemoveRange(reviewsReceived);
                    }

                    // 4. Delete professional record if exists
                    if (pro != null)
                    {
                        this.Professionals.Remove(pro);
                    }

                    // 5. Finally delete the visitor
                    this.Visitors.Remove(user);

                    // Save all changes
                    this.SaveChanges();

                    // Commit the transaction
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    // Rollback the transaction if anything fails
                    transaction.Rollback();
                    throw;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
