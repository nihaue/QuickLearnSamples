namespace QuickLearn.Json.XmlGenerator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.jsonBox = new System.Windows.Forms.TextBox();
            this.SaveXmlButton = new System.Windows.Forms.Button();
            this.rootNodeBox = new System.Windows.Forms.TextBox();
            this.namespaceBox = new System.Windows.Forms.TextBox();
            this.namespaceLabel = new System.Windows.Forms.Label();
            this.rootNodeLabel = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // jsonBox
            // 
            this.jsonBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jsonBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jsonBox.Location = new System.Drawing.Point(13, 13);
            this.jsonBox.Multiline = true;
            this.jsonBox.Name = "jsonBox";
            this.jsonBox.Size = new System.Drawing.Size(659, 313);
            this.jsonBox.TabIndex = 0;
            this.jsonBox.Text = resources.GetString("jsonBox.Text");
            // 
            // SaveXmlButton
            // 
            this.SaveXmlButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveXmlButton.Location = new System.Drawing.Point(557, 330);
            this.SaveXmlButton.Name = "SaveXmlButton";
            this.SaveXmlButton.Size = new System.Drawing.Size(116, 23);
            this.SaveXmlButton.TabIndex = 1;
            this.SaveXmlButton.Text = "Save As XML...";
            this.SaveXmlButton.UseVisualStyleBackColor = true;
            this.SaveXmlButton.Click += new System.EventHandler(this.SaveXmlButton_Click);
            // 
            // rootNodeBox
            // 
            this.rootNodeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rootNodeBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rootNodeBox.Location = new System.Drawing.Point(81, 332);
            this.rootNodeBox.Name = "rootNodeBox";
            this.rootNodeBox.Size = new System.Drawing.Size(100, 20);
            this.rootNodeBox.TabIndex = 2;
            this.rootNodeBox.Text = "JsonData";
            // 
            // namespaceBox
            // 
            this.namespaceBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.namespaceBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.namespaceBox.Location = new System.Drawing.Point(282, 332);
            this.namespaceBox.Name = "namespaceBox";
            this.namespaceBox.Size = new System.Drawing.Size(269, 20);
            this.namespaceBox.TabIndex = 3;
            this.namespaceBox.Text = "http://schemas.quicklearn.com/json/2013/09";
            // 
            // namespaceLabel
            // 
            this.namespaceLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.namespaceLabel.AutoSize = true;
            this.namespaceLabel.Location = new System.Drawing.Point(187, 335);
            this.namespaceLabel.Name = "namespaceLabel";
            this.namespaceLabel.Size = new System.Drawing.Size(89, 13);
            this.namespaceLabel.TabIndex = 4;
            this.namespaceLabel.Text = "Namespace URI:";
            // 
            // rootNodeLabel
            // 
            this.rootNodeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rootNodeLabel.AutoSize = true;
            this.rootNodeLabel.Location = new System.Drawing.Point(13, 335);
            this.rootNodeLabel.Name = "rootNodeLabel";
            this.rootNodeLabel.Size = new System.Drawing.Size(62, 13);
            this.rootNodeLabel.TabIndex = 5;
            this.rootNodeLabel.Text = "Root Node:";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "xml";
            this.saveFileDialog.FileName = "JsonXml.xml";
            this.saveFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            this.saveFileDialog.Title = "Save Converted JSON Data";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 361);
            this.Controls.Add(this.rootNodeLabel);
            this.Controls.Add(this.namespaceLabel);
            this.Controls.Add(this.namespaceBox);
            this.Controls.Add(this.rootNodeBox);
            this.Controls.Add(this.SaveXmlButton);
            this.Controls.Add(this.jsonBox);
            this.Name = "MainForm";
            this.Text = "JSON > WFX Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox jsonBox;
        private System.Windows.Forms.Button SaveXmlButton;
        private System.Windows.Forms.TextBox rootNodeBox;
        private System.Windows.Forms.TextBox namespaceBox;
        private System.Windows.Forms.Label namespaceLabel;
        private System.Windows.Forms.Label rootNodeLabel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

