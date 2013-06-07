using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickLearn.BizTalk.Components;
using QuickLearn.BizTalk.Schemas;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Winterdom.BizTalk.PipelineTesting;

namespace QuickLearn.BizTalk.Test
{
    [TestClass]
    public class PropertyMessageTests
    {

        // NOTE: These are simple sanity checks, and are in no-way actually comprehensive at this point.
        // (e.g., there is no checking of the behavior of the configuration properties of the pipeline component)
        
        [TestMethod]
        public void PropertyMessageDecoder_PropertyOnlyMessage_BodyConformsToSchema()
        {

            // Arrange
            var pipeline = GeneratePipeline(customPropertyNamespace: null, excludeSystemProperties: false);
            var inputMsg = GenerateTestMessage();

            // Act
            var outputs = pipeline.Execute(inputMsg);
            DumpOutputToFile(outputs);

            // Assert
            Assert.IsTrue(ValidateInstance(), "Pipeline output failed schema validation");
            ArchiveOutput();
            
        }

        [TestMethod]
        public void PropertyMessageDecoder_CompletelyEmptyMessage_BodyConformsToSchema()
        {

            // Arrange
            var pipeline = GeneratePipeline(customPropertyNamespace: null, excludeSystemProperties: false);
            var inputMsg = MessageHelper.CreateFromStream(new MemoryStream());
            Assert.IsTrue(inputMsg.Context.CountProperties == 0, "Context properties found when none expected");

            // Act
            var outputs = pipeline.Execute(inputMsg);
            DumpOutputToFile(outputs);

            // Assert
            Assert.IsTrue(ValidateInstance(), "Pipeline output failed schema validation");
            ArchiveOutput();
            
        }

        #region Utility Methods

        private static void DumpOutputToFile(MessageCollection outputs)
        {
            var outputStream = new MemoryStream();

            outputs.First().BodyPart.GetOriginalDataStream().CopyTo(outputStream);

            string outputFileName = string.Format("{0}.xml", new StackTrace().GetFrame(1).GetMethod().Name);
            File.WriteAllBytes(outputFileName, outputStream.ToArray());
        }

        private static void ArchiveOutput()
        {
            string outputFileName = string.Format("{0}.xml", new StackTrace().GetFrame(1).GetMethod().Name);
            Console.WriteLine("Pipeline Output");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine(File.ReadAllText(outputFileName));
            Console.WriteLine(new string('-', 60));
            File.Delete(outputFileName);
        }

        private static bool ValidateInstance()
        {
            PropertyMessage schemaInstance = new PropertyMessage();

            string outputFileName = string.Format("{0}.xml", new StackTrace().GetFrame(1).GetMethod().Name);
            return schemaInstance.ValidateInstance(outputFileName, Microsoft.BizTalk.TestTools.Schema.OutputInstanceType.XML);
        }

        private static Microsoft.BizTalk.Message.Interop.IBaseMessage GenerateTestMessage()
        {
            var inputMsg = MessageHelper.CreateFromStream(new MemoryStream());
            inputMsg.Context.Write("TestCustomProperty1", "http://tempuri.org", "Value1");
            inputMsg.Context.Write("TestOtherProperty1", "http://example.org", "Value2");
            inputMsg.Context.Write("TestSystemProperty1", "http://schemas.microsoft.com/BizTalk/2003/system-properties", "Value3");
            inputMsg.Context.Write("TestCustomProperty2", "http://tempuri.org", "Value4");
            inputMsg.Context.Write("TestOtherProperty2", "http://example.org", "Value5");
            inputMsg.Context.Write("TestSystemProperty2", "http://schemas.microsoft.com/BizTalk/2003/system-properties", "Value6");
            return inputMsg;
        }

        private static ReceivePipelineWrapper GeneratePipeline(string customPropertyNamespace, bool excludeSystemProperties)
        {
            PropertyMessageDecoder propertyMessageDecoder = new PropertyMessageDecoder();
            propertyMessageDecoder.CustomPropertyNamespace = customPropertyNamespace;
            propertyMessageDecoder.ExcludeSystemProperties = excludeSystemProperties;

            ReceivePipelineWrapper pipeline = PipelineFactory.CreateEmptyReceivePipeline();

            pipeline.AddComponent(propertyMessageDecoder, PipelineStage.Decode);
            pipeline.AddComponent(new Microsoft.BizTalk.Component.XmlDasmComp(), PipelineStage.Disassemble);
            pipeline.AddDocSpec(typeof(PropertyMessage));

            return pipeline;
        }

        #endregion
    }
}
