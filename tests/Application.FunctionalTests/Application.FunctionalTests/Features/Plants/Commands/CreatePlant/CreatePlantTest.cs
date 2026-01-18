using Application.Plants.Commands.CreatePlant;
using Domain.Entities.MasterData;
using FluentValidation;
using Shouldly;


namespace Application.FunctionalTests.Features.Plants.Commands.CreatePlant
{
    public class CreatePlantTest: BaseTestFixture
    {
        [Test]
        public async Task ShouldCreatePlant()
        {
            await Testing.RunAsDefaultUserAsync();
            var command = new CreatePlantCommand("PLT1", "Plant One", "EN");
            var plantId = await Testing.SendAsync(command);
            var plant = await Testing.FindAsync<Plant>(plantId);
            plant.ShouldNotBeNull();
        }
        [Test]
        public async Task ShouldRequireMinimumFields()
        {
            var command = new CreatePlantCommand("", "Plant One", "EN");

            await Should.ThrowAsync<ValidationException>(() => Testing.SendAsync(command));
        }
    }
}
