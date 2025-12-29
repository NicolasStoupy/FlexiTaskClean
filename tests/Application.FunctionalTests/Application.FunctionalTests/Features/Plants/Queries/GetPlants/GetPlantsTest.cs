using Application.Plants.Queries.GetPlants;
using Domain.Entities;
using Shouldly;

namespace Application.FunctionalTests.Features.Plants.Queries.GetPlants
{
    public class GetPlantsTest : BaseTestFixture
    {
        [Test]
        public async Task ShouldReturnAllPlants()
        {
            await Testing.RunAsDefaultUserAsync();
            var plant = new Plant("OST1", Domain.Enums.PlantLanguage.DE, "OSTWERDINGEN");
            plant.WorkAreas.Add(new WorkArea()
            {
                Code = "WA1",
                CommonName = "Work Area 1"
            });
            await Testing.AddAsync(plant);

            var query = new GetPlantsQuery();

            var result = await Testing.SendAsync(query);

            result.PlantLists.Count.ShouldBe(1);
            result.PlantLists.First().Code.ShouldBe("OST1");
            result.PlantLists.First().WorkAreas.Count.ShouldBe(1);
        }
        [Test]
        public async Task ShouldReturnSpecificPlant()
        {
            await Testing.RunAsDefaultUserAsync();

            await Testing.AddAsync(new Plant("MOU2", Domain.Enums.PlantLanguage.IT, "MOUSTIER"));
            await Testing.AddAsync(new Plant("OST2", Domain.Enums.PlantLanguage.IT, "OSTWerdingend"));

            // Act: get all
            var all = await Testing.SendAsync(new GetPlantsQuery());

            all.PlantLists.ShouldNotBeNull();
            all.PlantLists.Count.ShouldBe(2);

            var mou2 = all.PlantLists.SingleOrDefault(p => p.Code == "MOU2");
            mou2.ShouldNotBeNull("Plant MOU2 should exist after seeding.");          

            // Act: get by id
            var byId = await Testing.SendAsync(new GetPlantsQuery(mou2.Id));

            // Assert
            byId.PlantLists.ShouldNotBeNull();
            byId.PlantLists.Count.ShouldBe(1);
            byId.PlantLists.First().Code.ShouldBe("MOU2");
            byId.PlantLists.First().Id.ShouldBe(mou2.Id);
        }

    }
}
