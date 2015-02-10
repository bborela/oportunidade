using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MinutoSegurosWordCount
{
    public class ContadorPalavras : IContadorPalavras
    {
        public IDictionary<string, int> OcorrenciasPalavras { get; private set; }

        private readonly string[] _artigosEPreposicoes =
        {
            "e", "é", "um", "uma", "uns", "umas", "a", "o",
            "as", "os", "de", "da", "do", "das", "dos", "com",
            "por", "em", "ante", "perante", "após", "até", "contra",
            "desde", "em", "entre", "para","por", "sem", "sob",
            "sobre", "trás", "atrás de", "num", "numa", "nuns", "numas",
            "dentro de", "para com", "na", "no", "nas", "nos",
            "que", "ao", "à", "aos", "às", "pela", "pelo",
            "pelos", "pelas", "dum", "duma", "duns", "dumas",
        };

        private string _conteudo;

        public ContadorPalavras(string conteudo)
        {
            OcorrenciasPalavras = new Dictionary<string, int>();
            _conteudo = conteudo.ToLower();
        }

        public int NumeroTotalDePalavras
        {
            get { return OcorrenciasPalavras.Values.Sum(); }
        }

        public IEnumerable<OcorrenciaPalavra> DezPalavrasMaisRecorrentes
        {
            get
            {
                return OcorrenciasPalavras
                    .OrderByDescending(x => x.Value)
                    .Select(x => new OcorrenciaPalavra {Palavra = x.Key, NumeroOcorrencias = x.Value})
                    .Take(10);
            }
        }

        public void Contar()
        {
            ExcluirPreposicoesEArtigos();
            ContarOcorrenciasPalavras();
        }

        private void ContarOcorrenciasPalavras()
        {
            var pattern = new Regex(@"\w+");

            foreach (Match match in pattern.Matches(_conteudo))
            {
                if (!OcorrenciasPalavras.ContainsKey(match.Value))
                    OcorrenciasPalavras.Add(match.Value, 1);
                else
                    OcorrenciasPalavras[match.Value]++;
            }
        }

        private void ExcluirPreposicoesEArtigos()
        {
            foreach (var artigoOuPreposicao in _artigosEPreposicoes)
                _conteudo = Regex.Replace(_conteudo, @"\b" + artigoOuPreposicao + @"\b", "",
                    RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }
    }
}
