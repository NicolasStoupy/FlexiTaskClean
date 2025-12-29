using Application.Plants.Commands.DeletePlant;
using Domain.Entities;
using Shouldly;

namespace Application.FunctionalTests.Features.Plants.Commands.DeletePlant
{
    public class DeletePlantTest : BaseTestFixture
    {
        [Test]
        public async Task ShouldDeletePlant()
        {
            // Arrange
            await Testing.RunAsDefaultUserAsync();
            var plantCreated = await Testing.AddWithReturnAsync<Plant>(new Plant("TEST1", Domain.Enums.PlantLanguage.EN, "TestPlant"));

            // Vérifier que le plant existe bien avant la suppression
            var plantBefore = await Testing.FindAsync<Plant>(plantCreated.Id);
            plantBefore.ShouldNotBeNull();

            var countBefore = await Testing.CountAsync<Plant>();

            // Act
            var command = new DeletePlantCommand(plantCreated.Id);
            await Testing.SendAsync(command);

            // Assert
            var plantInDb = await Testing.FindAsync<Plant>(plantCreated.Id);
            plantInDb.ShouldBeNull();

            var countAfter = await Testing.CountAsync<Plant>();
            countAfter.ShouldBe(countBefore - 1);
        }
    }
}
