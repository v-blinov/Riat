using System;
using Laba1.Models;
using Laba1.Services;

namespace Laba1
{
    class Program
    {
        static void Main(string[] args)
        {
            var ssdDisk = new SsdDisk
            {
                DiskSizeMb = 500,
                DataTransferRateMbps = 2000,
                FormFactor = FormFactor.M2,
                Model = "Panterra Ultra Soft"
            };

            Console.WriteLine($"Несериализованный объект {ssdDisk}");

            var serializer = new Serializer();
            var ssdDiskSerializedJson = serializer.SerializeToJson(ssdDisk);
            Console.WriteLine($"Cериализованный объект {ssdDiskSerializedJson}");

            var deserializedDisk = serializer.DeserializeJsonToModel<SsdDisk>(ssdDiskSerializedJson);
            Console.WriteLine($"Десериализованный объект:\n" +
                $"DTR - {deserializedDisk.DataTransferRateMbps} \n" +
                $"DS - {deserializedDisk.DiskSizeMb} \n" +
                $"FF - {deserializedDisk.FormFactor}\n" +
                $"M - {deserializedDisk.Model}.");

            
            var ssdDiskSerializedXml = serializer.SerializeToXml(ssdDisk);
            Console.WriteLine($"Cериализованный объект XML {ssdDiskSerializedXml}");

            var deserializedDiskXml = serializer.DeserializeXmlToModel<SsdDisk>(ssdDiskSerializedXml);
            Console.WriteLine($"Десериализованный объект XML \n" +
                $"DTR - {deserializedDiskXml.DataTransferRateMbps} \n" +
                $"DS - {deserializedDiskXml.DiskSizeMb} \n" +
                $"FF - {deserializedDiskXml.FormFactor}\n" +
                $"M - {deserializedDiskXml.Model}.");
        }
    }
}
