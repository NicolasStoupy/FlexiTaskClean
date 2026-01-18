using Domain.Entities;
using Domain.Entities.MasterData;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.FunctionalTests.Features.WorkAreas.Queries.GetWorkAreas
{
    public class GetWorkAreasTest : BaseTestFixture
    {
        [Test]
        public async Task ShouldGetWorkAreas()
        {
            await Testing.RunAsDefaultUserAsync();
            var workArea = new WorkArea() {                 
                Code = "WA100",
                CommonName = "Work Area 100",
                Plant = new Plant()
                {
                    Code = "MOU1",
                    CommonName = "Moustier Plant"
                },
                WorkAreaType = new WorkAreaType() { Code = "TYPE100", Label = "Type 100" }
            };
            await Testing.AddAsync(workArea);
            var query = new Application.WorkAreas.Queries.GetWorkAreas.GetWorkAreasQuery();
            var result = await Testing.SendAsync(query);
            result.workAreas.Count.ShouldBeGreaterThan(0);
            result.workAreas.FirstOrDefault()
                .ShouldNotBeNull()
                .WorkAreaType.Code.ShouldBe("TYPE100");
        }
    }
}
