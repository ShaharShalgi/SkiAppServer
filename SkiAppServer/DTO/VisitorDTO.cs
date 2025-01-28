using SkiAppServer.Models;
namespace SkiAppServer.DTO
{
    public class VisitorDTO
    {
        public string Username { get; set; } = null; 
        public string Pass { get; set; } = null;
        public string? Gender { get; set; } = null;
        public string? Email { get; set; } = null;
        public int UserId { get; set; }
        public bool? IsPro { get; set; } = null;
        public VisitorDTO() { }
        public VisitorDTO(Models.Visitor modelVisitor) 
        {
            this.Username = modelVisitor.Username;
            this.Pass = modelVisitor.Pass;
            this.Gender = modelVisitor.Gender;
            this.Email = modelVisitor.Email;
            this.UserId = modelVisitor.UserId;
            this.IsPro = modelVisitor.IsPro;
        }
        public Models.Visitor GetModels()
        {
            Models.Visitor modelsUser = new Models.Visitor()
            {
                UserId = this.UserId,
                Username = this.Username,               
                Email = this.Email,
                Pass = this.Pass,
                Gender = this.Gender,
                IsPro = this.IsPro
                
            };

            return modelsUser;
        }

    }
}
