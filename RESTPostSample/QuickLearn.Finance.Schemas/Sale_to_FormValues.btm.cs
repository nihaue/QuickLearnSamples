namespace QuickLearn.Finance.Schemas {
    
    
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"QuickLearn.Finance.Schemas.Sale", typeof(global::QuickLearn.Finance.Schemas.Sale))]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"QuickLearn.Finance.Schemas.FormValues", typeof(global::QuickLearn.Finance.Schemas.FormValues))]
    public sealed class Sale_to_FormValues : global::Microsoft.XLANGs.BaseTypes.TransformBase {
        
        private const string _strMap = @"<?xml version=""1.0"" encoding=""UTF-16""?>
<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" xmlns:msxsl=""urn:schemas-microsoft-com:xslt"" xmlns:var=""http://schemas.microsoft.com/BizTalk/2003/var"" exclude-result-prefixes=""msxsl var s0 userCSharp"" version=""1.0"" xmlns:ns0=""http://schemas.quicklearn.com/Rest/2013/09"" xmlns:s0=""http://QuickLearn.Finance.Schemas.Sale"" xmlns:userCSharp=""http://schemas.microsoft.com/BizTalk/2003/userCSharp"">
  <xsl:output omit-xml-declaration=""yes"" method=""xml"" version=""1.0"" />
  <xsl:template match=""/"">
    <xsl:apply-templates select=""/s0:Sale"" />
  </xsl:template>
  <xsl:template match=""/s0:Sale"">
    <xsl:variable name=""var:v1"" select=""userCSharp:StringConcat(&quot;Amount&quot;)"" />
    <ns0:HttpRequestBody>
      <FormValue>
        <Id>
          <xsl:value-of select=""$var:v1"" />
        </Id>
        <Value>
          <xsl:value-of select=""Amount/text()"" />
        </Value>
      </FormValue>
    </ns0:HttpRequestBody>
  </xsl:template>
  <msxsl:script language=""C#"" implements-prefix=""userCSharp""><![CDATA[
public string StringConcat(string param0)
{
   return param0;
}



]]></msxsl:script>
</xsl:stylesheet>";
        
        private const string _strArgList = @"<ExtensionObjects />";
        
        private const string _strSrcSchemasList0 = @"QuickLearn.Finance.Schemas.Sale";
        
        private const global::QuickLearn.Finance.Schemas.Sale _srcSchemaTypeReference0 = null;
        
        private const string _strTrgSchemasList0 = @"QuickLearn.Finance.Schemas.FormValues";
        
        private const global::QuickLearn.Finance.Schemas.FormValues _trgSchemaTypeReference0 = null;
        
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
                _SrcSchemas[0] = @"QuickLearn.Finance.Schemas.Sale";
                return _SrcSchemas;
            }
        }
        
        public override string[] TargetSchemas {
            get {
                string[] _TrgSchemas = new string [1];
                _TrgSchemas[0] = @"QuickLearn.Finance.Schemas.FormValues";
                return _TrgSchemas;
            }
        }
    }
}
