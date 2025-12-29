using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Application.FunctionalTests.Interfaces
{
    public interface ITestDatabase
    {
        Task InitialiseAsync();

        DbConnection GetConnection();

        string GetConnectionString();

        Task ResetAsync();

        Task DisposeAsync();
    }
}
