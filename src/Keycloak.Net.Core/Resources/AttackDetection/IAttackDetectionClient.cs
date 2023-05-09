using System.Threading;
using System.Threading.Tasks;
using Keycloak.Net.Core.Models.AttackDetection;

namespace Keycloak.Net.Core.Resources.AttackDetection;

public interface IAttackDetectionClient
{
    Task<bool> ClearUserLoginFailuresAsync(string realm, CancellationToken cancellationToken = default);
    Task<bool> ClearUserLoginFailuresAsync(string realm, string userId, CancellationToken cancellationToken = default);

    Task<UserNameStatus> GetUserNameStatusInBruteForceDetectionAsync(string realm, string userId,
        CancellationToken cancellationToken = default);
}