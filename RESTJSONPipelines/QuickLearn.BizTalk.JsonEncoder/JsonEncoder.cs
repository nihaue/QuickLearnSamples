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
    [System.Runtime.InteropServices.Guid("22906c95-04fa-41c3-aea5-d835fcff9c1e")]
    [ComponentCategory(CategoryTypes.CATID_Encoder)]
    public class JsonEncoder : Microsoft.BizTalk.Component.Interop.IComponent, IBaseComponent, IPersistPropertyBag, IComponentUI
    {
        
        private System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("QuickLearn.BizTalk.JsonEncoder", Assembly.GetExecutingAssembly());
        
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
            classid = new System.Guid("22906c95-04fa-41c3-aea5-d835fcff9c1e");
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
        }
        
        /// <summary>
        /// Saves the current component configuration into the property bag
        /// </summary>
        /// <param name="pb">Configuration property bag</param>
        /// <param name="fClearDirty">not used</param>
        /// <param name="fSaveAllProperties">not used</param>
        public virtual void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, bool fClearDirty, bool fSaveAllProperties)
        {
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

        private const string JSON_SCHEMAS_NS = "http://schemas.quicklearn.com/json/2013/09";
        private const string JSONP_CALLBACK_PROPNAME = "JsonpCallback";

        private const string WCF_PROPERTIES_NS = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
        private const string HTTP_METHOD_PROPNAME = "InboundHttpMethod";
        private const string OPTIONS_METHOD = "OPTIONS";

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

            object httpMethod = null;
            httpMethod = inmsg.Context.Read(HTTP_METHOD_PROPNAME, WCF_PROPERTIES_NS);

            if (httpMethod != null && (httpMethod as string) == OPTIONS_METHOD)
            {
                // Remove the message body before returning
                var emptyOutputStream = new VirtualStream();
                inmsg.BodyPart.Data = emptyOutputStream;

                return inmsg;
            }

            #endregion


            // Make message seekable
            if (!inmsg.BodyPart.Data.CanSeek)
            {
                var originalStream = inmsg.BodyPart.Data;
                Stream seekableStream = new ReadOnlySeekableStream(originalStream);
                inmsg.BodyPart.Data = seekableStream;
                pc.ResourceTracker.AddResource(originalStream);
            }

            // Here again we are loading the entire document into memory
            // this is still a bad plan, and shouldn't be done in production
            // if you expect larger message sizes

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(inmsg.BodyPart.Data);

            if (xmlDoc.FirstChild.LocalName == "xml")
                xmlDoc.RemoveChild(xmlDoc.FirstChild);

            // Remove any root-level attributes added in the process of creating the XML
            // (Think xmlns attributes that have no meaning in JSON)

            xmlDoc.DocumentElement.Attributes.RemoveAll();

            string jsonString = JsonConvert.SerializeXmlNode(xmlDoc, Newtonsoft.Json.Formatting.Indented, true);

            #region Handle JSONP Request

            // Here we are detecting if there has been any value promoted to the jsonp callback property
            // which will contain the name of the function that should be passed the JSON data returned
            // by the service.

            object jsonpCallback = inmsg.Context.Read(JSONP_CALLBACK_PROPNAME, JSON_SCHEMAS_NS);
            string jsonpCallbackName = (jsonpCallback ?? (object)string.Empty) as string;

            if (!string.IsNullOrWhiteSpace(jsonpCallbackName))
                jsonString = string.Format("{0}({1});", jsonpCallbackName, jsonString);

            #endregion

            var outputStream = new VirtualStream(new MemoryStream(Encoding.UTF8.GetBytes(jsonString)));
            inmsg.BodyPart.Data = outputStream;

            return inmsg;
        }
        #endregion
    }
}
