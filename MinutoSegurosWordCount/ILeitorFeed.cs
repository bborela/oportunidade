using System.Collections.Generic;

namespace MinutoSegurosWordCount
{
    public interface ILeitorFeed
    {
        void CarregarFeed();
        IEnumerable<Topico> LerTopicos();
    }
}