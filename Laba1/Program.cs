using System;
using Laba1.Models;
using Laba1.Services;
using System.Linq;

namespace Laba1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // var ssdDisk = new SsdDisk { DiskSizeMb = 500, DataTransferRateMbps = 2000, FormFactor = FormFactor.M2, Model = "Panterra Ultra Soft" };
            //
            // Console.WriteLine($"Несериализованный объект {ssdDisk}");
            //
            // var serializer = new Serializer();
            // var ssdDiskSerializedJson = serializer.SerializeToJson(ssdDisk);
            // Console.WriteLine($"Cериализованный объект {ssdDiskSerializedJson}");
            //
            // var deserializedDisk = serializer.DeserializeJsonToModel<SsdDisk>(ssdDiskSerializedJson);
            // Console.WriteLine("Десериализованный объект:\n" +
            //     $"DTR - {deserializedDisk.DataTransferRateMbps} \n" +
            //     $"DS - {deserializedDisk.DiskSizeMb} \n" +
            //     $"FF - {deserializedDisk.FormFactor}\n" +
            //     $"M - {deserializedDisk.Model}.");
            //
            //
            // var ssdDiskSerializedXml = serializer.SerializeToXml(ssdDisk);
            // Console.WriteLine($"Cериализованный объект XML {ssdDiskSerializedXml}");
            //
            // var deserializedDiskXml = serializer.DeserializeXmlToModel<SsdDisk>(ssdDiskSerializedXml);
            // Console.WriteLine("Десериализованный объект XML \n" +
            //     $"DTR - {deserializedDiskXml.DataTransferRateMbps} \n" +
            //     $"DS - {deserializedDiskXml.DiskSizeMb} \n" +
            //     $"FF - {deserializedDiskXml.FormFactor}\n" +
            //     $"M - {deserializedDiskXml.Model}.");


            var serializationType = Console.ReadLine();
            var serializedBody = Console.ReadLine();
            
            Serializer serializer = new();
                        
            
            if (serializationType == "JSON")
            {
                var input = serializer.DeserializeJsonToModel<InputModel>(serializedBody);

                var output = GetOutputModel(input);

                Console.WriteLine(serializer.SerializeToJson(output));
            } else if (serializationType == "XML")
            {
                var input = serializer.DeserializeJsonToModel<InputModel>(serializedBody);

                var output = GetOutputModel(input);

                Console.WriteLine(serializer.SerializeToXml(output));
            } else
            {
                Console.WriteLine("Error. Unknown type");
            }
        }

        public static OutputModel GetOutputModel(InputModel input)
        {
            var decimalList = input.Sums.ToList();
            decimalList.AddRange(input.Muls.Select(p => (decimal)p).ToList());

            return new OutputModel
            {
                SumResult = input.Sums.Sum() * input.K, 
                MulResult = input.Muls.Aggregate(1, (current, inputMul) => current * inputMul),
                SortedInputs = decimalList.OrderBy(p => p).ToArray()
            };
        }
    }
}
