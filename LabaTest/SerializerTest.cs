using Laba1;
using Laba1.Models;
using Laba1.Services;
using NUnit.Framework;

namespace LabaTest
{
    public class SerializerTest
    {
        private Serializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new Serializer();
        }

        [Test]
        public void TestJson()
        {
            var oldDisk = new SsdDisk
            {
                DiskSizeMb = 500,
                DataTransferRateMbps = 2000,
                FormFactor = FormFactor.M2,
                Model = "Panterra Ultra Soft"
            };

            var diskJson = _serializer.SerializeToJson(oldDisk);
            var newDisk = _serializer.DeserializeJsonToModel<SsdDisk>(diskJson);

            Assert.AreEqual(oldDisk.DataTransferRateMbps, newDisk.DataTransferRateMbps);
            Assert.AreEqual(oldDisk.DiskSizeMb, newDisk.DiskSizeMb);
            Assert.AreEqual(oldDisk.FormFactor, newDisk.FormFactor);
            Assert.AreEqual(oldDisk.Model, newDisk.Model);
        }

        [Test]
        public void TestJsonRandomType()
        {
            var json = _serializer.SerializeToJson(42);
            var actual = _serializer.DeserializeJsonToModel<int>(json);

            Assert.AreEqual(42 ,actual);
        }

        [Test]
        public void TestXmlRandomType()
        {
            var json = _serializer.SerializeToXml("randomString");
            var actual = _serializer.DeserializeXmlToModel<string>(json);

            Assert.AreEqual("randomString" ,actual);
        }

        [Test]
        public void TestXml()
        {
            var oldDisk = new SsdDisk
            {
                DiskSizeMb = 500,
                DataTransferRateMbps = 2000,
                FormFactor = FormFactor.M2,
                Model = "Panterra Ultra Soft"
            };

            var diskXml = _serializer.SerializeToXml(oldDisk);
            var newDisk = _serializer.DeserializeXmlToModel<SsdDisk>(diskXml);

            Assert.AreEqual(oldDisk.DataTransferRateMbps, newDisk.DataTransferRateMbps);
            Assert.AreEqual(oldDisk.DiskSizeMb, newDisk.DiskSizeMb);
            Assert.AreEqual(oldDisk.FormFactor, newDisk.FormFactor);
            Assert.AreEqual(oldDisk.Model, newDisk.Model);
        }

    }
}