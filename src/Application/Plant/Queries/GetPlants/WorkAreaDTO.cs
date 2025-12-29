namespace Application.Plant.Queries.GetPlants
{
    public class WorkAreaDTO 
    {
        public string CommonName { get; set; }
        public string Code { get; set; }



        private class Mapping : AutoMapper.Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.WorkArea, WorkAreaDTO>();
            }
        }
    }
}
