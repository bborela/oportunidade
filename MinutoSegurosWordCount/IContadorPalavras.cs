using System.Collections.Generic;

namespace MinutoSegurosWordCount
{
    public interface IContadorPalavras
    {
        IDictionary<string, int> OcorrenciasPalavras { get; }
        int NumeroTotalDePalavras { get; }
        IEnumerable<OcorrenciaPalavra> DezPalavrasMaisRecorrentes { get; }
        void Contar();
    }
}