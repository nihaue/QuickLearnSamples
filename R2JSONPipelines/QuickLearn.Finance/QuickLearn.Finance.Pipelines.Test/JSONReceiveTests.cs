using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickLearn.Finance.Messaging;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using Winterdom.BizTalk.PipelineTesting;

namespace QuickLearn.Finance.Pipelines.Test
{
    [TestClass]
    public class JSONReceiveTests
    {
        [TestMethod]
        [DeploymentItem(@"Messages\sample.json")]
        [DeploymentItem(@"Configuration\pipelineconfig.xml")]
        public void JSONReceive_JSONMessage_CorrectValidXMLReturned()
        {

            string rootNode = "ServiceResponse";
            string namespaceUri = "http://schemas.finance.yahoo.com/API/2014/08/";

            string sourceDoc = Path.Combine(TestContext.DeploymentDirectory, "sample.json");
            string schemaPath = Path.Combine(TestContext.DeploymentDirectory, "ServiceResponse.xsd");
            string outputDoc = Path.Combine(TestContext.DeploymentDirectory, "JSONReceive.out");

            var pipeline = PipelineFactory.CreateReceivePipeline(typeof(JSONReceive));

            configureJSONReceivePipeline(pipeline, rootNode, namespaceUri);

            using (var inputStream = File.OpenRead(sourceDoc))
            {
                pipeline.AddDocSpec(typeof(ServiceResponse));
                var result = pipeline.Execute(MessageHelper.CreateFromStream(inputStream));

                Assert.IsTrue(result.Count > 0, "No messages returned from pipeline.");

                using (var outputFile = File.OpenWrite(outputDoc))
                {
                    result[0].BodyPart.GetOriginalDataStream().CopyTo(outputFile);
                    outputFile.Flush();
                }

            }

            ServiceResponse schema = new ServiceResponse();
            Assert.IsTrue(schema.ValidateInstance(outputDoc, Microsoft.BizTalk.TestTools.Schema.OutputInstanceType.XML),
                "Output message failed validation against the schema");

            Assert.AreEqual(XDocument.Load(outputDoc).Descendants("Bid").First().Value, "44.97", "Incorrect Bid amount in output file");

        }

        private void configureJSONReceivePipeline(ReceivePipelineWrapper pipeline, string rootNode, string namespaceUri)
        {
            string configPath = Path.Combine(TestContext.DeploymentDirectory, "pipelineconfig.xml");

            var configDoc = XDocument.Load(configPath);

            configDoc.Descendants("RootNode").First().SetValue(rootNode);
            configDoc.Descendants("RootNodeNamespace").First().SetValue(namespaceUri);

            configDoc.Save(configPath);

            pipeline.ApplyInstanceConfig(configPath);
        }


        public TestContext TestContext { get; set; }
    }
}
