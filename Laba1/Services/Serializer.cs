using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Laba1.Services
{
    public class Serializer
    {
        public string SerializeToJson<T>(T model) => 
            JsonSerializer.Serialize(model);

        public T DeserializeJsonToModel<T>(string jsonString) => 
            JsonSerializer.Deserialize<T>(jsonString);
        
        //-----------------------------------------------------------------------------------
        
        private static readonly XmlWriterSettings Settings = new()
        {
            Indent = true,              // Испольлзовать ли отступ  для элементов
            OmitXmlDeclaration = true   //следует ли опустить XML-объявление
                                        // а-ля <?xml version="1.0" encoding="UTF-8" standalone="no" ?>
        };
        private static readonly ConcurrentDictionary<Type, XmlSerializer> Serializers = new();
        private static readonly XmlSerializerNamespaces Namespaces = new(new[] { XmlQualifiedName.Empty });

        private static XmlSerializer CreateSerializer(Type type) => new(type);
        
        public string SerializeToXml<T>(T disk)
        {
            using var stream = new StringWriter();
            using var writer = XmlWriter.Create(stream, Settings);

            var serializer = Serializers.GetOrAdd(typeof(T), CreateSerializer);

            serializer.Serialize(writer, disk, Namespaces);
            return stream.ToString();
        }

        public T DeserializeXmlToModel<T>(string xmlString)
        {
            var serializer = Serializers.GetOrAdd(typeof(T), CreateSerializer);

            using var reader = new StringReader(xmlString);
            return (T)serializer.Deserialize(reader);
        }
    }
}
