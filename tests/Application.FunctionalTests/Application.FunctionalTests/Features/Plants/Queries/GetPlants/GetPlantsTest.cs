using System.Linq;
using Application.Features.Configurations.Plants.Queries.GetPlants;
using Domain.Entities;
using Domain.Entities.MasterData;
using Shouldly;

namespace Application.FunctionalTests.Features.Plants.Queries.GetPlants
{
    /// <summary>
    /// Suite de tests fonctionnels pour la requête <see cref="GetPlantsQuery"/>.
    /// Ces tests utilisent le fixture de test de base (<see cref="BaseTestFixture"/>) qui fournit:
    /// - l'exécution des commandes/queries via <c>Testing.SendAsync</c>,
    /// - l'ajout d'entités en base via <c>Testing.AddAsync</c>,
    /// - la connexion en tant qu'utilisateur par défaut via <c>Testing.RunAsDefaultUserAsync</c>.
    /// </summary>
    public class GetPlantsTest : BaseTestFixture
    {
        /// <summary>
        /// Test: doit retourner tous les plants présents en base.
        /// Arrange:
        ///  - Se connecter en tant qu'utilisateur par défaut.
        ///  - Créer un plant avec une zone de travail (work area) et l'ajouter en base.
        /// Act:
        ///  - Envoyer une instance de <see cref="GetPlantsQuery"/> sans filtre.
        /// Assert:
        ///  - Vérifier qu'un seul plant est retourné,
        ///  - Vérifier le code du plant et que la liste des zones de travail contient l'élément attendu.
        /// </summary>
        [Test]
        public async Task ShouldReturnAllPlants()
        {
            await Testing.RunAsAdministratorAsync();

            // Arrange: création d'un plant avec une zone de travail
            var plant = new Plant("OST1", Domain.Enums.PlantLanguage.DE, "OSTWERDINGEN");
            plant.WorkAreas.Add(new WorkArea()
            {
                Code = "WA1",
                CommonName = "Work Area 1",
                WorkAreaType = new WorkAreaType() { Code = "PROD"}
            });
            await Testing.AddAsync(plant);

            // Act: exécution de la query pour récupérer tous les plants
            var query = new GetPlantsQuery();
            var result = await Testing.SendAsync(query);

            // Assert: validations sur le résultat
            result.PlantLists.Count.ShouldBe(1);
            result.PlantLists.First().Code.ShouldBe("OST1");
            result.PlantLists.First().WorkAreas.Count.ShouldBe(1);
        }

        /// <summary>
        /// Test: doit permettre de récupérer un plant spécifique par son identifiant.
        /// Arrange:
        ///  - Se connecter en tant qu'utilisateur par défaut.
        ///  - Ajouter deux plants en base.
        /// Act:
        ///  - Récupérer d'abord la liste complète, puis récupérer un plant par son Id via <see cref="GetPlantsQuery(int?)"/>.
        /// Assert:
        ///  - Vérifier que la liste complète contient les deux plants,
        ///  - Vérifier que la requête par Id retourne exactement le plant demandé et que son Id correspond.
        /// </summary>
        [Test]
        public async Task ShouldReturnSpecificPlant()
        {
            await Testing.RunAsAdministratorAsync();

            // Arrange: ajouter deux plants en base
            await Testing.AddAsync(new Plant("MOU2", Domain.Enums.PlantLanguage.IT, "MOUSTIER"));
            await Testing.AddAsync(new Plant("OST2", Domain.Enums.PlantLanguage.IT, "OSTWerdingend"));

            // Act: récupérer tous les plants
            var all = await Testing.SendAsync(new GetPlantsQuery());

            // Assert: s'assurer que les deux plants existent
            all.PlantLists.ShouldNotBeNull();
            all.PlantLists.Count.ShouldBe(2);

            var mou2 = all.PlantLists.SingleOrDefault(p => p.Code == "MOU2");
            mou2.ShouldNotBeNull("Plant MOU2 should exist after seeding.");

            // Act: récupérer par id le plant MOU2
            var byId = await Testing.SendAsync(new GetPlantsQuery(mou2.Id));

            // Assert: validations sur la récupération par id
            byId.PlantLists.ShouldNotBeNull();
            byId.PlantLists.Count.ShouldBe(1);
            byId.PlantLists.First().Code.ShouldBe("MOU2");
            byId.PlantLists.First().Id.ShouldBe(mou2.Id);
        }
    }
}
