using System;

namespace MinutoSegurosWordCount
{
    class Program
    {
        private const string FeedDoc = @"feed.xml";

        static void Main(string[] args)
        {
            var leitorFeed = new LeitorFeed(FeedDoc);
            leitorFeed.CarregarFeed();

            foreach (var topico in leitorFeed.LerTopicos())
            {
                var contadorPalavras = new ContadorPalavras(topico.Texto);
                contadorPalavras.Contar();
                Console.WriteLine("Tópico: {0}\nQuantidade total de palavras: {1}", topico.Titulo,
                    contadorPalavras.NumeroTotalDePalavras);
                Console.WriteLine("Palavras mais recorrentes:");
                var i = 1;
                foreach (var ocorrencias in contadorPalavras.DezPalavrasMaisRecorrentes)
                Console.WriteLine("{0}) {1}, {2}", i++, ocorrencias.Palavra, ocorrencias.NumeroOcorrencias);
                Console.WriteLine();
            }
        }
    }
}
