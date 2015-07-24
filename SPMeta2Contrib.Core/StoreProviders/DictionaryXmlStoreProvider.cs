using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SPMeta2Contrib.Core.StoreProviders
{
    public class DictionaryXmlStoreProvider: DictionaryStoreProvider<string, string>
    {
        protected string Filename;

        public DictionaryXmlStoreProvider(string filename)
        {
            Filename = filename;
        }
        public override void Load(IDictionary<string, string> container)
        {
            if (!File.Exists(Filename)) return;
                

            XElement root = XElement.Load(Filename);
            foreach (var item in root.Elements())
            {
                var keyEl = item.Element("Key");
                var valEl = item.Element("Value");

                if (keyEl == null || valEl == null)
                    throw new Exception("Incorrect xml format");

                container.Add(keyEl.Value, valEl.Value);
            }
        }

        public override void Save(IDictionary<string, string> dataSource)
        {
            XElement root = new XElement("root",
                dataSource.Select(kv =>
                    new XElement("Item",
                        new XElement("Key", kv.Key),
                        new XElement("Value", kv.Value))));

            root.Save(Filename);
        }
    }
}
