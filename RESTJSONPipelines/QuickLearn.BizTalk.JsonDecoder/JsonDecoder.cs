using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Streaming;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace QuickLearn.BizTalk
{
    
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("92ceae90-5c84-443e-9dac-e63ecf4e9df8")]
    [ComponentCategory(CategoryTypes.CATID_Decoder)]
    public class JsonDecoder : Microsoft.BizTalk.Component.Interop.IComponent, IBaseComponent, IPersistPropertyBag, IComponentUI
    {
        
        private System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("QuickLearn.BizTalk.JsonDecoder", Assembly.GetExecutingAssembly());
        
        #region IBaseComponent members
        /// <summary>
        /// Name of the component
        /// </summary>
        [Browsable(false)]
        public string Name
        {
            get
            {
                return resourceManager.GetString("COMPONENTNAME", System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        
        /// <summary>
        /// Version of the component
        /// </summary>
        [Browsable(false)]
        public string Version
        {
            get
            {
                return resourceManager.GetString("COMPONENTVERSION", System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        
        /// <summary>
        /// Description of the component
        /// </summary>
        [Browsable(false)]
        public string Description
        {
            get
            {
                return resourceManager.GetString("COMPONENTDESCRIPTION", System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        #endregion
        
        #region IPersistPropertyBag members
        /// <summary>
        /// Gets class ID of component for usage from unmanaged code.
        /// </summary>
        /// <param name="classid">
        /// Class ID of the component
        /// </param>
        public void GetClassID(out System.Guid classid)
        {
            classid = new System.Guid("92ceae90-5c84-443e-9dac-e63ecf4e9df8");
        }
        
        /// <summary>
        /// not implemented
        /// </summary>
        public void InitNew()
        {
        }
        
        /// <summary>
        /// Loads configuration properties for the component
        /// </summary>
        /// <param name="pb">Configuration property bag</param>
        /// <param name="errlog">Error status</param>
        public virtual void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, int errlog)
        {
            object val = null;
            val = this.ReadPropertyBag(pb, "Namespace");
            if ((val != null))
            {
                this._Namespace = ((string)(val));
            }

            val = this.ReadPropertyBag(pb, "RootNode");
            if ((val != null))
            {
                this._RootNode = ((string)(val));
            }
        }
        
        /// <summary>
        /// Saves the current component configuration into the property bag
        /// </summary>
        /// <param name="pb">Configuration property bag</param>
        /// <param name="fClearDirty">not used</param>
        /// <param name="fSaveAllProperties">not used</param>
        public virtual void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, bool fClearDirty, bool fSaveAllProperties)
        {
            this.WritePropertyBag(pb, "Namespace", this.Namespace);
            this.WritePropertyBag(pb, "RootNode", this.RootNode);
        }
        
        #region utility functionality
        /// <summary>
        /// Reads property value from property bag
        /// </summary>
        /// <param name="pb">Property bag</param>
        /// <param name="propName">Name of property</param>
        /// <returns>Value of the property</returns>
        private object ReadPropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName)
        {
            object val = null;
            try
            {
                pb.Read(propName, out val, 0);
            }
            catch (System.ArgumentException )
            {
                return val;
            }
            catch (System.Exception e)
            {
                throw new System.ApplicationException(e.Message);
            }
            return val;
        }
        
        /// <summary>
        /// Writes property values into a property bag.
        /// </summary>
        /// <param name="pb">Property bag.</param>
        /// <param name="propName">Name of property.</param>
        /// <param name="val">Value of property.</param>
        private void WritePropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName, object val)
        {
            try
            {
                pb.Write(propName, ref val);
            }
            catch (System.Exception e)
            {
                throw new System.ApplicationException(e.Message);
            }
        }
        #endregion
        #endregion
        
        #region IComponentUI members
        /// <summary>
        /// Component icon to use in BizTalk Editor
        /// </summary>
        [Browsable(false)]
        public IntPtr Icon
        {
            get
            {
                return ((System.Drawing.Bitmap)(this.resourceManager.GetObject("COMPONENTICON", System.Globalization.CultureInfo.InvariantCulture))).GetHicon();
            }
        }
        
        /// <summary>
        /// The Validate method is called by the BizTalk Editor during the build 
        /// of a BizTalk project.
        /// </summary>
        /// <param name="obj">An Object containing the configuration properties.</param>
        /// <returns>The IEnumerator enables the caller to enumerate through a collection of strings containing error messages. These error messages appear as compiler error messages. To report successful property validation, the method should return an empty enumerator.</returns>
        public System.Collections.IEnumerator Validate(object obj)
        {
            // example implementation:
            // ArrayList errorList = new ArrayList();
            // errorList.Add("This is a compiler error");
            // return errorList.GetEnumerator();
            return null;
        }
        #endregion

        #region Properties

        private string _Namespace;
        public string Namespace
        {
            get
            {
                return _Namespace;
            }
            set
            {
                _Namespace = value;
            }
        }

        private string _RootNode;
        public string RootNode
        {
            get
            {
                return _RootNode;
            }
            set
            {
                _RootNode = value;
            }
        }

        private const string DEFAULT_PREFIX = "ns0";

        private const string JSON_SCHEMAS_NS = "http://schemas.quicklearn.com/json/2013/09";
        private const string CORS_MSG_ROOT = "CORS";
        private const string JSONP_MSG_ROOT = "JSONP";
        private const string JSONP_CALLBACK_PROPNAME = "JsonpCallback";

        private const string WCF_PROPERTIES_NS = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
        private const string HTTP_METHOD_PROPNAME = "InboundHttpMethod";
        private const string OPTIONS_METHOD = "OPTIONS";
        
        private const string SYSTEM_PROPERTIES_NS = "http://schemas.microsoft.com/BizTalk/2003/system-properties";
        private const string ROUTE_DIRECT_TO_TP_PROPNAME = "RouteDirectToTP";
        private const string EPM_RR_CORRELATION_TOKEN_PROPNAME = "EpmRRCorrelationToken";
        private const string OPERATION_NAME_PROPNAME = "Operation";

        #endregion

        #region IComponent members
        /// <summary>
        /// Implements IComponent.Execute method.
        /// </summary>
        /// <param name="pc">Pipeline context</param>
        /// <param name="inmsg">Input message</param>
        /// <returns>Original input message</returns>
        /// <remarks>
        /// IComponent.Execute method is used to initiate
        /// the processing of the message in this pipeline component.
        /// </remarks>
        public Microsoft.BizTalk.Message.Interop.IBaseMessage Execute(Microsoft.BizTalk.Component.Interop.IPipelineContext pc, Microsoft.BizTalk.Message.Interop.IBaseMessage inmsg)
        {

            #region Handle CORS Requests

            // Detect of the incoming message is an HTTP CORS request
            // http://www.w3.org/TR/cors/
            
            // If it is, we will promote both the RouteDirectToTP property and the
            // EpmRRCorrelationToken so that the request is immediately routed
            // back to the send pipeline of this receive port

            object httpMethod = null;
            httpMethod = inmsg.Context.Read(HTTP_METHOD_PROPNAME, WCF_PROPERTIES_NS);

            if (httpMethod != null && (httpMethod as string) == OPTIONS_METHOD)
            {
                object curCorrToken = inmsg.Context.Read(EPM_RR_CORRELATION_TOKEN_PROPNAME, SYSTEM_PROPERTIES_NS);
                inmsg.Context.Promote(EPM_RR_CORRELATION_TOKEN_PROPNAME, SYSTEM_PROPERTIES_NS, curCorrToken);
                inmsg.Context.Promote(ROUTE_DIRECT_TO_TP_PROPNAME, SYSTEM_PROPERTIES_NS, true);

                var corsDoc = new XmlDocument();
                corsDoc.AppendChild(corsDoc.CreateElement(DEFAULT_PREFIX, CORS_MSG_ROOT, JSON_SCHEMAS_NS));
                writeMessage(inmsg, corsDoc);

                return inmsg;
            }

            #endregion


            #region Handle JSONP Request

            // Here we are detecting if there has been any value promoted to the jsonp callback property
            // which will contain the name of the function that should be passed the JSON data returned
            // by the service.

            object jsonpCallback = inmsg.Context.Read(JSONP_CALLBACK_PROPNAME, JSON_SCHEMAS_NS);
            string jsonpCallbackName = (jsonpCallback ?? (object)string.Empty) as string;

            if (!string.IsNullOrWhiteSpace(jsonpCallbackName))
            {
                var jsonpDoc = new XmlDocument();
                jsonpDoc.AppendChild(jsonpDoc.CreateElement(DEFAULT_PREFIX, JSONP_MSG_ROOT, JSON_SCHEMAS_NS));
                writeMessage(inmsg, jsonpDoc);

                return inmsg;
            }

            #endregion

            // Determine the current operation. We will use this as the root node
            // of the document if there isn't one specified as a property on the
            // component
            object operation = null;
            operation = inmsg.Context.Read(OPERATION_NAME_PROPNAME, SYSTEM_PROPERTIES_NS);
            string operationName = (operation ?? (object)string.Empty) as string;

            // Make message seekable
            if (!inmsg.BodyPart.Data.CanSeek)
            {
                var originalStream = inmsg.BodyPart.Data;
                Stream seekableStream = new ReadOnlySeekableStream(originalStream);
                inmsg.BodyPart.Data = seekableStream;
                pc.ResourceTracker.AddResource(originalStream);
            }

            // We are going to do something terrible and bring the entire message in memory.
            // Then we are going to create a string out of it, because that's how the library
            // wants it.
            
            // Please don't use this code for production purposes with large messages.
            // You have been warned.

            MemoryStream jsonStream = new MemoryStream();
            inmsg.BodyPart.Data.CopyTo(jsonStream);
            inmsg.BodyPart.Data.Seek(0, SeekOrigin.Begin);

            // I am not going to assume any specific encoding. The BodyPart tells us
            // which encoding to use, so we will use that to get the string -- except
            // when it doesn't, then I will be a horrible person and assume an encoding.
            
            var jsonString = jsonStream.Length == 0
                                    ? string.Empty
                                    : Encoding.GetEncoding(inmsg.BodyPart.Charset ?? Encoding.UTF8.WebName).GetString(jsonStream.ToArray());
           
            var rawDoc = JsonConvert.DeserializeXmlNode(jsonString, string.IsNullOrWhiteSpace(this.RootNode) ? operationName : this.RootNode, true);

            // Here we are ensuring that the custom namespace shows up on the root node
            // so that we have a nice clean message type on the request messages
            var xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateElement(DEFAULT_PREFIX, rawDoc.DocumentElement.LocalName, this.Namespace));
            xmlDoc.DocumentElement.InnerXml = rawDoc.DocumentElement.InnerXml;
                       
            // All of the heavy lifting has been done, now we just have to shuffle this
            // new data over to the output message
            writeMessage(inmsg, xmlDoc);

            return inmsg;

        }

        private static void writeMessage(Microsoft.BizTalk.Message.Interop.IBaseMessage inmsg, XmlDocument xmlDoc)
        {
            var outputStream = new VirtualStream();

            using (var writer = XmlWriter.Create(outputStream, new XmlWriterSettings()
            {
                CloseOutput = false,
                Encoding = Encoding.UTF8
            }))
            {
                xmlDoc.WriteTo(writer);
                writer.Flush();
            }

            outputStream.Seek(0, SeekOrigin.Begin);

            inmsg.BodyPart.Charset = Encoding.UTF8.WebName;
            inmsg.BodyPart.Data = outputStream;
        }

        #endregion
    }
}
