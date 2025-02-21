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
        public List<Tip>? GetAllTips()
        {
            return this.Tips.ToList();
        }
        public List<Tip>? GetTipsByDifficulty(int diff)
        {
            return this.Tips.Where(u => u.Difficulty == diff).ToList();
        }
        public List<PostPhoto>? GetPostPhotos(int posterId)
        {
            return this.PostPhotos.Where(u => u.UserId == posterId).ToList();
        }
        public Professional? GetPro(int Id)
        {
            return this.Professionals.Where(u => u.UserId == Id).FirstOrDefault();
        }
    }
}
