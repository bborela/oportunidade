using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;

namespace MinutoSegurosWordCount
{
    public class LeitorFeed : ILeitorFeed
    {
        private readonly string _uri;
        private SyndicationFeed _feed;
        private Topico _topicoAtual;
        private IEnumerator<SyndicationItem> _itemAtual;

        public LeitorFeed(string uri)
        {
            _uri = uri;
        }

        public void CarregarFeed()
        {
            using (var reader = XmlReader.Create(_uri))
                _feed = SyndicationFeed.Load(reader);

            if (_feed == null)
                throw new NullReferenceException();
        }

        public IEnumerable<Topico> LerTopicos()
        {
            if (_feed == null)
                throw new NullReferenceException();

            var topicos = new List<Topico>();

            _itemAtual = _feed.Items.GetEnumerator();

            while (_itemAtual.MoveNext())
            {
                _topicoAtual = new Topico(_itemAtual.Current.Title.Text);
                _topicoAtual.ConcatenarTexto(_itemAtual.Current.Summary.Text);
                ConcatenarTextosContentEncoded();
                _topicoAtual.SanitizarTexto();
                topicos.Add(_topicoAtual);
            }

            return topicos;
        }

        private void ConcatenarTextosContentEncoded()
        {
            foreach (var extension in _itemAtual.Current.ElementExtensions)
            {
                var textoContentEncoded = LerTextoContentEncoded(extension);
                if (textoContentEncoded != null)
                    _topicoAtual.ConcatenarTexto(textoContentEncoded);
            }
        }

        private static string LerTextoContentEncoded(SyndicationElementExtension extension)
        {
            var element = extension.GetObject<XElement>();
            if (element.Name.LocalName == "encoded"
                && element.Name.Namespace.ToString().Contains("content"))
            {
                return element.Value;
            }

            return null;
        }
    }
}