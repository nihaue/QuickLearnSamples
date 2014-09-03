using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.IO;
using System.Xml;

namespace QuickLearn.Finance.Messaging.Test
{
    [TestClass]
    public class ServiceResponseTests
    {

        [TestMethod]
        [DeploymentItem(@"Messages\sample.json")]
        public void ServiceResponse_ValidInstanceSingleResultNullAsk_ValidationSucceeds()
        {

            // Arrange
            ServiceResponse target = new ServiceResponse();
            string rootNode = "ServiceResponse";
            string namespaceUri = "http://schemas.finance.yahoo.com/API/2014/08/";
            string sourceDoc = Path.Combine(TestContext.DeploymentDirectory, "sample.json");
            string sourceDocAsXml = Path.Combine(TestContext.DeploymentDirectory, "sample.json.xml");

            ConvertJsonToXml(sourceDoc, sourceDocAsXml, rootNode, namespaceUri);

            // Act
            bool validationResult = target.ValidateInstance(sourceDocAsXml, Microsoft.BizTalk.TestTools.Schema.OutputInstanceType.XML);

            // Assert
            Assert.IsTrue(validationResult, "Instance {0} failed validation against the schema.", sourceDoc);

        }

        /// <summary>
        /// Converts a JSON Message Instance to XML for validation
        /// </summary>
        /// <param name="inputFilePath">Path to the JSON message instance</param>
        /// <param name="outputFilePath">Path to store the XML representation of the JSON instance</param>
        /// <param name="rootNode">Name that should be used for the root node of the XML instance</param>
        /// <param name="namespaceUri">Target namespace URI that should be used for the XML instance</param>
        /// <param name="namespacePrefix">Namespace prefix that should be used for the XML instance</param>
        public void ConvertJsonToXml(string inputFilePath, string outputFilePath,
            string rootNode = "Root", string namespaceUri = "http://tempuri.org", string namespacePrefix = "ns0")
        {
            var jsonString = File.ReadAllText(inputFilePath);
            var rawDoc = JsonConvert.DeserializeXmlNode(jsonString, rootNode, true);

            // Here we are ensuring that the custom namespace shows up on the root node
            // so that we have a nice clean message type on the request messages
            var xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateElement(namespacePrefix, rawDoc.DocumentElement.LocalName, namespaceUri));
            xmlDoc.DocumentElement.InnerXml = rawDoc.DocumentElement.InnerXml;

            xmlDoc.Save(outputFilePath);
        }

        public TestContext TestContext { get; set; }
    }
}
