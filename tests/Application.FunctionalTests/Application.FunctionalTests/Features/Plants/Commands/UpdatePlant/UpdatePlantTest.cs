using Domain.Entities.MasterData;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.FunctionalTests.Features.Plants.Commands.UpdatePlant
{

    public class UpdatePlantTest : BaseTestFixture
    {
        [Test]
        public async Task ShouldUpdatePlant()
        {
            // Arrange
            await Testing.RunAsDefaultUserAsync();
            var plantCreated = await Testing.AddWithReturnAsync<Plant>(new Plant("MO1", Domain.Enums.PlantLanguage.EN, "TestPlant2"));
            var updateCommand = new Application.Features.Plants.Commands.UpdatePlants.UpdatePlantCommand()
            {
                PlantID = plantCreated.Id,
                Code = "MO2",
                CommonName = "UpdatedTestPlant2",
                Language = "FR"
            };
            // Act
            var updatedPlantId = await Testing.SendAsync(updateCommand);
            // Assert
            updatedPlantId.ShouldBe(plantCreated.Id);
            var plantUpdated = await Testing.FindAsync<Plant>(updatedPlantId);

            plantUpdated.ShouldNotBeNull();
            plantUpdated.Id.ShouldBe(plantCreated.Id);
            plantUpdated.Code.ShouldBe("MO2");
            plantUpdated.CommonName.ShouldBe("UpdatedTestPlant2");
            plantUpdated.Language.ShouldBe(Domain.Enums.PlantLanguage.FR);

        }
    }
}
