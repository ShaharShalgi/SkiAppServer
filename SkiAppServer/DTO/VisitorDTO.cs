namespace SkiAppServer.DTO
{
    public class VisitorDTO
    {
        public string Username { get; set; } = null; 
        public string Pass { get; set; } = null;
        public string Gender { get; set; } = null;
        public string Email { get; set; } = null;
        public VisitorDTO() { }
        public VisitorDTO(Models.Visitor modelVisitor) 
        {
            this.Username = modelVisitor.Username;
            this.Pass = modelVisitor.Pass;
            this.Gender = modelVisitor.Gender;
            this.Email = modelVisitor.Email;
        }
        

    }
}
