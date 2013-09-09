namespace QuickLearn.BizTalk.ItemListProcessor {
    
    
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"QuickLearn.BizTalk.ItemListProcessor.Schemas.ItemAverageRequest", typeof(global::QuickLearn.BizTalk.ItemListProcessor.Schemas.ItemAverageRequest))]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"QuickLearn.BizTalk.ItemListProcessor.Schemas.ItemAverageResponse", typeof(global::QuickLearn.BizTalk.ItemListProcessor.Schemas.ItemAverageResponse))]
    public sealed class AvgRequest_to_AvgResp : global::Microsoft.XLANGs.BaseTypes.TransformBase {
        
        private const string _strMap = @"<?xml version=""1.0"" encoding=""UTF-16""?>
<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" xmlns:msxsl=""urn:schemas-microsoft-com:xslt"" xmlns:var=""http://schemas.microsoft.com/BizTalk/2003/var"" exclude-result-prefixes=""msxsl var userCSharp"" version=""1.0"" xmlns:ns0=""http://schemas.quicklearn.com/json/2013/09"" xmlns:userCSharp=""http://schemas.microsoft.com/BizTalk/2003/userCSharp"">
  <xsl:output omit-xml-declaration=""yes"" method=""xml"" version=""1.0"" />
  <xsl:template match=""/"">
    <xsl:apply-templates select=""/ns0:ItemAverageRequest"" />
  </xsl:template>
  <xsl:template match=""/ns0:ItemAverageRequest"">
    <ns0:ItemAverageResponse>
      <itemCount>
        <xsl:value-of select=""itemCount/text()"" />
      </itemCount>
      <xsl:variable name=""var:v1"" select=""userCSharp:InitCumulativeAvg(0)"" />
      <xsl:for-each select=""/ns0:ItemAverageRequest/items/item"">
        <xsl:variable name=""var:v2"" select=""userCSharp:AddToCumulativeAvg(0,string(amount/text()),&quot;1000&quot;)"" />
      </xsl:for-each>
      <xsl:variable name=""var:v3"" select=""userCSharp:GetCumulativeAvg(0)"" />
      <itemAmountAvg>
        <xsl:value-of select=""$var:v3"" />
      </itemAmountAvg>
    </ns0:ItemAverageResponse>
  </xsl:template>
  <msxsl:script language=""C#"" implements-prefix=""userCSharp""><![CDATA[
public string InitCumulativeAvg(int index)
{
	if (index >= 0)
	{
		if (index >= myCumulativeAvgArray.Count)
		{
			int i = myCumulativeAvgArray.Count;
			for (; i<=index; i++)
			{
				myCumulativeAvgArray.Add("""");
				myCumulativeAvgCountArray.Add(0);
			}
		}
		else
		{
			myCumulativeAvgArray[index] = """";
			myCumulativeAvgCountArray[index] = 0;
		}
	}
	return """";
}

	private System.Collections.ArrayList myCumulativeAvgArray = new System.Collections.ArrayList();
private System.Collections.ArrayList myCumulativeAvgCountArray = new System.Collections.ArrayList();

public string AddToCumulativeAvg(int index, string val, string notused)
{
	if (index < 0 || index >= myCumulativeAvgArray.Count)
	{
		return """";
	}
	double d = 0;
	if (IsNumeric(val, ref d))
	{
		if (myCumulativeAvgArray[index] == """")
		{
			myCumulativeAvgArray[index] = d;
		}
		else
		{
			myCumulativeAvgArray[index] = (double)(myCumulativeAvgArray[index]) + d;
		}
	}
	myCumulativeAvgCountArray[index] = (int)(myCumulativeAvgCountArray[index]) + 1;
	return (myCumulativeAvgArray[index] is double) ? ((double)myCumulativeAvgArray[index]).ToString(System.Globalization.CultureInfo.InvariantCulture) : """";
}

public string GetCumulativeAvg(int index)
{
	if (index < 0 || index >= myCumulativeAvgArray.Count)
	{
		return """";
	}
	if ((int)(myCumulativeAvgCountArray[index]) == 0 || myCumulativeAvgArray[index] == """")
	{
		return """";
	}
	else
	{
		double numer = (double)(myCumulativeAvgArray[index]);
		double denom = Convert.ToDouble((int)(myCumulativeAvgCountArray[index]), System.Globalization.CultureInfo.InvariantCulture);
		double d = numer / denom;
		return d.ToString(System.Globalization.CultureInfo.InvariantCulture);
	}
}

public bool IsNumeric(string val)
{
	if (val == null)
	{
		return false;
	}
	double d = 0;
	return Double.TryParse(val, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out d);
}

public bool IsNumeric(string val, ref double d)
{
	if (val == null)
	{
		return false;
	}
	return Double.TryParse(val, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out d);
}


]]></msxsl:script>
</xsl:stylesheet>";
        
        private const string _strArgList = @"<ExtensionObjects />";
        
        private const string _strSrcSchemasList0 = @"QuickLearn.BizTalk.ItemListProcessor.Schemas.ItemAverageRequest";
        
        private const global::QuickLearn.BizTalk.ItemListProcessor.Schemas.ItemAverageRequest _srcSchemaTypeReference0 = null;
        
        private const string _strTrgSchemasList0 = @"QuickLearn.BizTalk.ItemListProcessor.Schemas.ItemAverageResponse";
        
        private const global::QuickLearn.BizTalk.ItemListProcessor.Schemas.ItemAverageResponse _trgSchemaTypeReference0 = null;
        
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
                _SrcSchemas[0] = @"QuickLearn.BizTalk.ItemListProcessor.Schemas.ItemAverageRequest";
                return _SrcSchemas;
            }
        }
        
        public override string[] TargetSchemas {
            get {
                string[] _TrgSchemas = new string [1];
                _TrgSchemas[0] = @"QuickLearn.BizTalk.ItemListProcessor.Schemas.ItemAverageResponse";
                return _TrgSchemas;
            }
        }
    }
}
