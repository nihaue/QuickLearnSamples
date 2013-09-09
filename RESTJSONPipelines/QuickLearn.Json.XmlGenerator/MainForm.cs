using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace QuickLearn.Json.XmlGenerator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void SaveXmlButton_Click(object sender, EventArgs e)
        {
            var jsonString = jsonBox.Text;
            
            var rawDoc = JsonConvert.DeserializeXmlNode(jsonString, this.rootNodeBox.Text, true);

            // Here we are ensuring that the custom namespace shows up on the root node
            // so that we have a nice clean message type on the request messages
            var xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateElement("ns0", rawDoc.DocumentElement.LocalName, this.namespaceBox.Text));
            xmlDoc.DocumentElement.InnerXml = rawDoc.DocumentElement.InnerXml;

            var result = saveFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Yes || result == DialogResult.OK)
            {

                var outputFileName = saveFileDialog.FileName;
                using (var writer = XmlWriter.Create(outputFileName))
                {
                    xmlDoc.WriteTo(writer);
                    writer.Flush();
                }

                MessageBox.Show(string.Format("JSON data has been converted to XML and stored at:\r\n{0}", outputFileName),
                    "Success", MessageBoxButtons.OK);

                // Re-load from the XML to verify that the document saved can still be read back as JSON data
                XmlDocument savedDoc = new XmlDocument();
                savedDoc.Load(outputFileName);

                if (savedDoc.FirstChild.LocalName == "xml")
                    savedDoc.RemoveChild(savedDoc.FirstChild);

                savedDoc.DocumentElement.Attributes.RemoveAll();

                this.jsonBox.Text = JsonConvert.SerializeXmlNode(savedDoc, Newtonsoft.Json.Formatting.Indented, true);

            }

        }
    }
}
