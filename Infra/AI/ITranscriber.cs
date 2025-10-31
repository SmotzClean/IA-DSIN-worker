using System.Threading;
using System.Threading.Tasks;

namespace DSIN.DSIN.Business.Infra.AI;

public interface ITranscriber
{
    Task<string> TranscribeAsync(string rawJson, CancellationToken ct = default);
}
