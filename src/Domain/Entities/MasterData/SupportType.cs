namespace Domain.Entities.MasterData
{
    public class SupportType
    {
        public string SupportTypeID { get; set; } = "";
        public string? Description { get; set; }
        public double MaxLoad { get; set; }
        public bool Active { get; set; }


        public SupportType(string supportTypeID, string? description, double maxLoad, bool active)
        {
            SupportTypeID = supportTypeID;
            Description = description;
            MaxLoad = maxLoad;
            Active = active;
        }
    }
}