using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Common.Models
{
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public SerializableDictionary() { }
        public SerializableDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }
        public SerializableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer) { }
        public SerializableDictionary(IEqualityComparer<TKey> comparer) : base(comparer) { }
        public SerializableDictionary(int capacity) : base(capacity) { }
        public SerializableDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) { }

        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.IsEmptyElement)
                return;
            reader.ReadStartElement();
            while (true)
            {
                try
                {
                    if (reader.NodeType == System.Xml.XmlNodeType.EndElement)
                        break;
                    reader.MoveToAttribute(0);
                    var key = (TKey)Enum.Parse(typeof(TKey), reader.Value, true);
                    reader.MoveToAttribute(1);
                    var val = (TValue)Convert.ChangeType(reader.Value, typeof(TValue));
                    this.Add(key, val);
                    if (!reader.Read())
                        break;
                }
                catch (Exception ex) { throw ex; }
            }
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");

                writer.WriteAttributeString("key", key.ToString());
                writer.WriteAttributeString("value", this[key].ToString());

                writer.WriteEndElement();
            }
        }
        #endregion
    }
}
