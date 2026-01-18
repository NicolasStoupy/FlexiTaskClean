using Application.WorkAreas.Commands.CreateWorkArea;
using Domain.Entities.MasterData;
using Shouldly;

namespace Application.FunctionalTests.Features.WorkAreas.Commands.CreateWorkArea
{
    public class CreateWorkAreaTest : BaseTestFixture
    {
        [Test]
        public async Task ShouldCreateWorkArea()
        {
            await Testing.RunAsDefaultUserAsync();

            var plant = new Plant
            {
                Code = "PL1",
                CommonName = "Plant 1"
            };
            var WorkAreaType = new WorkAreaType
            {
                Code = "WAT1",
                Label = "Work Area Type 1"
            };
            var newPlant = await Testing.AddWithReturnAsync<Plant>(plant);
            var newWorkAreaType =  await Testing.AddWithReturnAsync<WorkAreaType>(WorkAreaType);
            var command = new CreateWorkAreaCommand
            {
                Code = "WA2",
                CommonName = "Work Area 2",
                PlantID = newPlant.Id,
                TypeID= newWorkAreaType.Id
            };

            var workAreaId = await Testing.SendAsync(command);
            var workArea = await Testing.FindAsync<WorkArea>(workAreaId);

            workArea.ShouldNotBeNull();
            workArea.Code.ShouldBe("WA2");
            workArea.CommonName.ShouldBe("Work Area 2");      
     
        }
    }
}
