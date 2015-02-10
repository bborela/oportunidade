using System.Text.RegularExpressions;

namespace MinutoSegurosWordCount
{
    public class Topico
    {
        public string Titulo { get; set; }
        public string Texto { get; private set; }

        public Topico(string titulo)
        {
            Titulo = Texto = titulo;
        }

        public void ConcatenarTexto(string conteudo)
        {
            Texto += " " + conteudo;
        }

        public void SanitizarTexto()
        {
            Texto = Regex.Replace(Texto, @"\r|\n", " ");
            Texto = Regex.Replace(Texto, @"<div.+?</div>", "");
            Texto = Regex.Replace(Texto, @"<[^>]+>|&[^;]+?;", "");
            Texto = Regex.Replace(Texto, @"The post .+? on [^.]+?\.", "");
            Texto = Regex.Replace(Texto, @"\s{2,}", "");
        }
    }
}
