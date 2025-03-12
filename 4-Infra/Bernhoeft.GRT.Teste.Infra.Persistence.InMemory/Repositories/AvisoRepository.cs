using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Attributes;
using Bernhoeft.GRT.Core.EntityFramework.Infra;
using Bernhoeft.GRT.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bernhoeft.GRT.ContractWeb.Infra.Persistence.SqlServer.ContractStore.Repositories
{
    [InjectService(Interface: typeof(IAvisoRepository))]
    public class AvisoRepository : Repository<AvisoEntity>, IAvisoRepository
    {

        public AvisoRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }


        public Task<List<AvisoEntity>> ObterTodosAvisosAsync(TrackingBehavior tracking = TrackingBehavior.Default, CancellationToken cancellationToken = default)
        {
            var query = tracking is TrackingBehavior.NoTracking ? Set.AsNoTrackingWithIdentityResolution() : Set;
            return query.ToListAsync();
        }

        public async Task UpdateAsync(AvisoEntity entity, CancellationToken cancellationToken = default)
        {
            Set.Update(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task CreateAsync(AvisoEntity entity, CancellationToken cancellationToken = default)
        {
            await Set.AddAsync(entity, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);

        }

        public async Task DeleteAsync(AvisoEntity entity, CancellationToken cancellationToken = default)
        {
            Set.Update(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}