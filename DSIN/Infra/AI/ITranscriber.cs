using System.Threading;
using System.Threading.Tasks;

namespace DSIN.DSIN.Business.Infra.AI;

public interface ITranscriber
{
    /// <summary>
    /// Recebe o JSON bruto e devolve o JSON transcrito já estruturado.
    /// </summary>
    Task<string> TranscribeAsync(string rawJson, CancellationToken ct = default);
}