using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class ApplicationDbContextFactory :IApplicationDbContextFactory
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public ApplicationDbContextFactory(IDbContextFactory<ApplicationDbContext> factory)
            => _factory = factory;

        public async Task<IApplicationDbContext> CreateAsync(CancellationToken ct = default)
            => await _factory.CreateDbContextAsync(ct);
    }
}
