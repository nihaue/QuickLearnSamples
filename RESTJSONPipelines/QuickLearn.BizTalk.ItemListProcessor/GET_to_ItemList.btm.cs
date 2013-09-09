namespace QuickLearn.BizTalk.ItemListProcessor {
    
    
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"QuickLearn.BizTalk.JsonSchemas.JsonJSONP", typeof(global::QuickLearn.BizTalk.JsonSchemas.JsonJSONP))]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"QuickLearn.BizTalk.ItemListProcessor.Schemas.ItemAverageRequest", typeof(global::QuickLearn.BizTalk.ItemListProcessor.Schemas.ItemAverageRequest))]
    public sealed class GET_to_ItemList : global::Microsoft.XLANGs.BaseTypes.TransformBase {
        
        private const string _strMap = @"<?xml version=""1.0"" encoding=""UTF-16""?>
<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" xmlns:msxsl=""urn:schemas-microsoft-com:xslt"" xmlns:var=""http://schemas.microsoft.com/BizTalk/2003/var"" exclude-result-prefixes=""msxsl var userCSharp"" version=""1.0"" xmlns:ns0=""http://schemas.quicklearn.com/json/2013/09"" xmlns:userCSharp=""http://schemas.microsoft.com/BizTalk/2003/userCSharp"">
  <xsl:output omit-xml-declaration=""yes"" method=""xml"" version=""1.0"" />
  <xsl:template match=""/"">
    <xsl:apply-templates select=""/ns0:JSONP"" />
  </xsl:template>
  <xsl:template match=""/ns0:JSONP"">
    <xsl:variable name=""var:v1"" select=""userCSharp:StringConcat(&quot;2&quot;)"" />
    <xsl:variable name=""var:v2"" select=""'1'"" />
    <xsl:variable name=""var:v3"" select=""'500'"" />
    <xsl:variable name=""var:v4"" select=""'2'"" />
    <xsl:variable name=""var:v5"" select=""'1000'"" />
    <ns0:ItemAverageRequest>
      <itemCount>
        <xsl:value-of select=""$var:v1"" />
      </itemCount>
      <items>
        <item>
          <id>
            <xsl:value-of select=""$var:v2"" />
          </id>
          <amount>
            <xsl:value-of select=""$var:v3"" />
          </amount>
        </item>
        <item>
          <id>
            <xsl:value-of select=""$var:v4"" />
          </id>
          <amount>
            <xsl:value-of select=""$var:v5"" />
          </amount>
        </item>
      </items>
    </ns0:ItemAverageRequest>
  </xsl:template>
  <msxsl:script language=""C#"" implements-prefix=""userCSharp""><![CDATA[
public string StringConcat(string param0)
{
   return param0;
}



]]></msxsl:script>
</xsl:stylesheet>";
        
        private const string _strArgList = @"<ExtensionObjects />";
        
        private const string _strSrcSchemasList0 = @"QuickLearn.BizTalk.JsonSchemas.JsonJSONP";
        
        private const global::QuickLearn.BizTalk.JsonSchemas.JsonJSONP _srcSchemaTypeReference0 = null;
        
        private const string _strTrgSchemasList0 = @"QuickLearn.BizTalk.ItemListProcessor.Schemas.ItemAverageRequest";
        
        private const global::QuickLearn.BizTalk.ItemListProcessor.Schemas.ItemAverageRequest _trgSchemaTypeReference0 = null;
        
        public override string XmlContent {
            get {
                return _strMap;
            }
        }
        
        public override string XsltArgumentListContent {
            get {
                return _strArgList;
            }
        }
        
        public override string[] SourceSchemas {
            get {
                string[] _SrcSchemas = new string [1];
                _SrcSchemas[0] = @"QuickLearn.BizTalk.JsonSchemas.JsonJSONP";
                return _SrcSchemas;
            }
        }
        
        public override string[] TargetSchemas {
            get {
                string[] _TrgSchemas = new string [1];
                _TrgSchemas[0] = @"QuickLearn.BizTalk.ItemListProcessor.Schemas.ItemAverageRequest";
                return _TrgSchemas;
            }
        }
    }
}
