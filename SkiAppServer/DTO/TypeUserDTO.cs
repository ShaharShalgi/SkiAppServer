using System.ComponentModel.DataAnnotations;

namespace SkiAppServer.DTO
{
    public class TypeUserDTO
    {
        public int TypeId { get; set; }

  
        public string? TypeName { get; set; }
        public TypeUserDTO() { }
        public TypeUserDTO(Models.TypeUser modelTypeUser) 
        {
            this.TypeId = modelTypeUser.TypeId;
            this.TypeName = modelTypeUser.TypeName;
        }
    }
}
