using Application.FunctionalTests.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.FunctionalTests.Databases
{
    public  class TestDatabaseFactory
    {
        public static async Task<ITestDatabase> CreateAsync()
        {

            var database = new SqlTestcontainersTestDatabase();

            await database.InitialiseAsync();

            return database;
        }
    }
}
