using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Streaming;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Xml;

namespace QuickLearn.BizTalk.Components
{

    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("d0e2ec57-2da0-4a48-a2cc-9d5eed1f6836")]
    [ComponentCategory(CategoryTypes.CATID_Decoder)]
    public class PropertyMessageDecoder : Microsoft.BizTalk.Component.Interop.IComponent, IBaseComponent, IPersistPropertyBag, IComponentUI
    {

        private const string SYSTEM_NAMESPACE = "http://schemas.microsoft.com/BizTalk/2003/system-properties";
        private const string ROOT_NODE_NAME = "PropertyMessage";
        private const string PROPERTY_NODE_NAME = "Property";
        private const string NAMESPACE_ATTRIBUTE = "Namespace";
        private const string NAME_ATTRIBUTE = "Name";
        private const string TARGET_NAMESPACE = "http://schemas.quicklearn.com/PropertyMessage/2013/06/";

        private System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("QuickLearn.BizTalk.Components.PropertyMessageDecoder", Assembly.GetExecutingAssembly());

        private bool _ExcludeSystemProperties;

        [Description("Set to True to exclude properties from the namespace http://schemas.microsoft.com/BizTalk/2003/system-properties")]
        public bool ExcludeSystemProperties
        {
            get
            {
                return _ExcludeSystemProperties;
            }
            set
            {
                _ExcludeSystemProperties = value;
            }
        }

        private string _CustomPropertyNamespace;

        [Description("When set, ignores all properties except those defined in the namespace indicated. When left blank, includes all properties in the generated message body.")]
        public string CustomPropertyNamespace
        {
            get
            {
                return _CustomPropertyNamespace;
            }
            set
            {
                _CustomPropertyNamespace = value;
            }
        }

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
            classid = new System.Guid("d0e2ec57-2da0-4a48-a2cc-9d5eed1f6836");
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
            val = this.ReadPropertyBag(pb, "ExcludeSystemProperties");
            if ((val != null))
            {
                this._ExcludeSystemProperties = ((bool)(val));
            }
            val = this.ReadPropertyBag(pb, "CustomPropertyNamespace");
            if ((val != null))
            {
                this._CustomPropertyNamespace = ((string)(val));
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
            this.WritePropertyBag(pb, "ExcludeSystemProperties", this.ExcludeSystemProperties);
            this.WritePropertyBag(pb, "CustomPropertyNamespace", this.CustomPropertyNamespace);
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
            catch (System.ArgumentException)
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

            Stream dataStream = new VirtualStream(VirtualStream.MemoryFlag.AutoOverFlowToDisk);
            pc.ResourceTracker.AddResource(dataStream);

            using (XmlWriter writer = XmlWriter.Create(dataStream))
            {

                // Start creating the message body
                writer.WriteStartDocument();
                writer.WriteStartElement("ns0", ROOT_NODE_NAME, TARGET_NAMESPACE);

                for (int i = 0; i < inmsg.Context.CountProperties; i++)
                {

                    // Read in current property information
                    string propName = null;
                    string propNamespace = null;
                    object propValue = inmsg.Context.ReadAt(i, out propName, out propNamespace);

                    // Skip properties that we don't care about due to configuration (default is to allow all properties)
                    if (ExcludeSystemProperties && propNamespace == SYSTEM_NAMESPACE) continue;

                    if (!String.IsNullOrWhiteSpace(CustomPropertyNamespace) &&
                            propNamespace != CustomPropertyNamespace) continue;

                    // Create Property element
                    writer.WriteStartElement(PROPERTY_NODE_NAME);

                    // Create attributes on Property element
                    writer.WriteStartAttribute(NAMESPACE_ATTRIBUTE);
                    writer.WriteString(propNamespace);
                    writer.WriteEndAttribute();

                    writer.WriteStartAttribute(NAME_ATTRIBUTE);
                    writer.WriteString(propName);
                    writer.WriteEndAttribute();

                    // Write value inside property element
                    writer.WriteString(Convert.ToString(propValue));

                    writer.WriteEndElement();
                }

                // Finish out the message
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();

            }

            dataStream.Seek(0, SeekOrigin.Begin);

            var outmsg = Utility.CloneMessage(inmsg, pc);
            outmsg.BodyPart.Data = dataStream;

            return outmsg;
        }

        #endregion
    }
}
