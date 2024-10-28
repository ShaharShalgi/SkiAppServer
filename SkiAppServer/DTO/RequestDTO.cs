namespace SkiAppServer.DTO
{
    public class RequestDTO
    {
        public int RequestId { get; set; }


        public int? SenderId { get; set; }


        public int? RecieverId { get; set; }


        public int? StatusId { get; set; }
        public RequestDTO() { }
        public RequestDTO(Models.Request modelRequest)
        {
            this.RequestId = modelRequest.RequestId;
            this.SenderId = modelRequest.SenderId;
            this.StatusId = modelRequest.StatusId;
            this.RecieverId = modelRequest.RecieverId;

        }
    }
}
