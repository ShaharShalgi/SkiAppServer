using SkiAppServer.Models;
namespace SkiAppServer.DTO
{
    public class VisitorDTO
    {
        public string Username { get; set; } = null; 
        public string Pass { get; set; } = null;
        public string Gender { get; set; } = null;
        public string Email { get; set; } 
        public VisitorDTO() { }
        public VisitorDTO(Models.Visitor modelVisitor) 
        {
            this.Username = modelVisitor.Username;
            this.Pass = modelVisitor.Pass;
            this.Gender = modelVisitor.Gender;
            this.Email = modelVisitor.Email;
        }
        public Models.Visitor GetModels()
        {
            Models.Visitor modelsUser = new Models.Visitor()
            {
                
                Username = this.Username,               
                Email = this.Email,
                Pass = this.Pass,
                Gender = this.Gender,
                
            };

            return modelsUser;
        }

    }
}
