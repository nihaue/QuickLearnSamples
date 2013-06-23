using Microsoft.VisualStudio.TestTools.UnitTesting;
using Winterdom.BizTalk.PipelineTesting;
using QuickLearn.Samples;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using Microsoft.QualityTools.Testing.Fakes;

namespace QuickLearn.Samples.Test
{
    [TestClass]
    public class RushOrderMarkerTests
    {

        private const string WAREHOUSE_ID_PROPERTY_NAMESPACE = "http://schemas.quicklearn.com/samples/WarehouseRouting/2013/06/";
        private const string WAREHOUSE_ID_PROPERTY_NAME = "WarehouseId";

        public RushOrderMarkerTests()
        {
            WarehouseIdentifiers.Add("WHA123");
            WarehouseIdentifiers.Add("WHB456");
            WarehouseIdentifiers.Add("WHC789");
        }

        public List<string> WarehouseIdentifiers = new List<string>();

        [TestMethod]
        public void RushOrderMarker_FirstPartOfDay_FirstWarehouseReturned()
        {

            // Arrange
            var pipeline = GeneratePipeline();
            var testMessage = GenerateTestMessage();

            DateTime fakeTime = new DateTime(
                                    year: 2013,
                                    month: 01,
                                    day: 01,
                                    hour: 1, minute: 0, second: 0);

            using (ShimsContext.Create())
            {

                System.Fakes.ShimDateTime.UtcNowGet = () => { return fakeTime; };
                var expectedId = WarehouseIdentifiers[0];

                // Act
                var output = pipeline.Execute(testMessage);

                // Assert
                var warehouseId = output[0].Context.Read(WAREHOUSE_ID_PROPERTY_NAME, WAREHOUSE_ID_PROPERTY_NAMESPACE);
                Assert.AreEqual(expectedId, Convert.ToString(warehouseId), "Incorrect warehouse id returned");

            }

        }

        [TestMethod]
        public void RushOrderMarker_SecondPartOfDay_FirstWarehouseReturned()
        {

            // Arrange
            var pipeline = GeneratePipeline();
            var testMessage = GenerateTestMessage();

            DateTime fakeTime = new DateTime(
                                    year: 2013,
                                    month: 01,
                                    day: 01,
                                    hour: 9, minute: 0, second: 0);

            using (ShimsContext.Create())
            {

                System.Fakes.ShimDateTime.UtcNowGet = () => { return fakeTime; };
                var expectedId = WarehouseIdentifiers[1];

                // Act
                var output = pipeline.Execute(testMessage);

                // Assert
                var warehouseId = output[0].Context.Read(WAREHOUSE_ID_PROPERTY_NAME, WAREHOUSE_ID_PROPERTY_NAMESPACE);
                Assert.AreEqual(expectedId, Convert.ToString(warehouseId), "Incorrect warehouse id returned");

            }

        }

        [TestMethod]
        public void RushOrderMarker_ThirdPartOfDay_ThirdWarehouseReturned()
        {

            // Arrange
            var pipeline = GeneratePipeline();
            var testMessage = GenerateTestMessage();

            DateTime fakeTime = new DateTime(
                                    year: 2013,
                                    month: 01,
                                    day: 01,
                                    hour: 17, minute: 0, second: 0);

            using (ShimsContext.Create())
            {

                System.Fakes.ShimDateTime.UtcNowGet = () => { return fakeTime; };
                var expectedId = WarehouseIdentifiers[2];

                // Act
                var output = pipeline.Execute(testMessage);

                // Assert
                var warehouseId = output[0].Context.Read(WAREHOUSE_ID_PROPERTY_NAME, WAREHOUSE_ID_PROPERTY_NAMESPACE);
                Assert.AreEqual(expectedId, Convert.ToString(warehouseId), "Incorrect warehouse id returned");

            }

        }

        private static Microsoft.BizTalk.Message.Interop.IBaseMessage GenerateTestMessage()
        {
            var inputMsg = MessageHelper.CreateFromStream(new MemoryStream(Encoding.UTF8.GetBytes("OrderMessage")));
            return inputMsg;
        }

        private static ReceivePipelineWrapper GeneratePipeline()
        {
            var pipeline = PipelineFactory.CreateEmptyReceivePipeline();

            pipeline.AddComponent(new RushOrderMarker(), PipelineStage.Decode);

            return pipeline;
        }
    }
}
