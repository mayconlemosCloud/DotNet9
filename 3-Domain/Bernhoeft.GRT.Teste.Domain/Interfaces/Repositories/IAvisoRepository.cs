using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Enums;

namespace Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories
{
    public interface IAvisoRepository : IRepository<AvisoEntity>
    {
        Task<List<AvisoEntity>> ObterTodosAvisosAsync(TrackingBehavior tracking = TrackingBehavior.Default, CancellationToken cancellationToken = default);
        Task UpdateAsync(AvisoEntity entity, CancellationToken cancellationToken = default);
        Task CreateAsync(AvisoEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(AvisoEntity entity, CancellationToken cancellationToken = default);

    }
}