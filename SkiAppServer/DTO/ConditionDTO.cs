namespace SkiAppServer.DTO
{
    public class ConditionDTO
    {
        public int StatusID { get; set; }
        public string? StatusName { get; set; } 
        public ConditionDTO() { }
        public ConditionDTO(Models.Condition modelCondition)
        {
            this.StatusID = modelCondition.StatusId;
            this.StatusName = modelCondition.StatusName;
          
        }
    }
}
