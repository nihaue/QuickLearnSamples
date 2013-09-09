namespace QuickLearn.BizTalk.JsonPipelines
{
    using System;
    using System.Collections.Generic;
    using Microsoft.BizTalk.PipelineOM;
    using Microsoft.BizTalk.Component;
    using Microsoft.BizTalk.Component.Interop;
    
    
    public sealed class JsonReceive : Microsoft.BizTalk.PipelineOM.ReceivePipeline
    {
        
        private const string _strPipeline = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Document xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instanc"+
"e\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" MajorVersion=\"1\" MinorVersion=\"0\">  <Description /> "+
" <CategoryId>f66b9f5e-43ff-4f5f-ba46-885348ae1b4e</CategoryId>  <FriendlyName>Receive</FriendlyName>"+
"  <Stages>    <Stage>      <PolicyFileStage _locAttrData=\"Name\" _locID=\"1\" Name=\"Decode\" minOccurs=\""+
"0\" maxOccurs=\"-1\" execMethod=\"All\" stageId=\"9d0e4103-4cce-4536-83fa-4a5040674ad6\" />      <Component"+
"s>        <Component>          <Name>QuickLearn.BizTalk.JsonDecoder,JsonDecoder, Version=1.0.0.0, Cu"+
"lture=neutral, PublicKeyToken=bab2a9ea69a59de0</Name>          <ComponentName>JsonDecoder</Component"+
"Name>          <Description>Converts an incoming JSON message into an XML representation</Descriptio"+
"n>          <Version>1.0</Version>          <Properties>            <Property Name=\"Namespace\">     "+
"         <Value xsi:type=\"xsd:string\">http://schemas.quicklearn.com/json/2013/09</Value>            "+
"</Property>            <Property Name=\"RootNode\">              <Value xsi:type=\"xsd:string\" />      "+
"      </Property>          </Properties>          <CachedDisplayName>JsonDecoder</CachedDisplayName>"+
"          <CachedIsManaged>true</CachedIsManaged>        </Component>      </Components>    </Stage>"+
"    <Stage>      <PolicyFileStage _locAttrData=\"Name\" _locID=\"2\" Name=\"Disassemble\" minOccurs=\"0\" ma"+
"xOccurs=\"-1\" execMethod=\"FirstMatch\" stageId=\"9d0e4105-4cce-4536-83fa-4a5040674ad6\" />      <Compone"+
"nts>        <Component>          <Name>Microsoft.BizTalk.Component.XmlDasmComp,Microsoft.BizTalk.Pip"+
"eline.Components, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35</Name>          "+
"<ComponentName>XML disassembler</ComponentName>          <Description>Streaming XML disassembler</De"+
"scription>          <Version>1.0</Version>          <Properties>            <Property Name=\"Envelope"+
"SpecNames\">              <Value xsi:type=\"xsd:string\" />            </Property>            <Property"+
" Name=\"EnvelopeSpecTargetNamespaces\">              <Value xsi:type=\"xsd:string\" />            </Prop"+
"erty>            <Property Name=\"DocumentSpecNames\">              <Value xsi:type=\"xsd:string\" />   "+
"         </Property>            <Property Name=\"DocumentSpecTargetNamespaces\">              <Value x"+
"si:type=\"xsd:string\" />            </Property>            <Property Name=\"AllowUnrecognizedMessage\">"+
"              <Value xsi:type=\"xsd:boolean\">false</Value>            </Property>            <Propert"+
"y Name=\"ValidateDocument\">              <Value xsi:type=\"xsd:boolean\">false</Value>            </Pro"+
"perty>            <Property Name=\"RecoverableInterchangeProcessing\">              <Value xsi:type=\"x"+
"sd:boolean\">false</Value>            </Property>            <Property Name=\"HiddenProperties\">      "+
"        <Value xsi:type=\"xsd:string\">EnvelopeSpecTargetNamespaces,DocumentSpecTargetNamespaces</Valu"+
"e>            </Property>          </Properties>          <CachedDisplayName>XML disassembler</Cache"+
"dDisplayName>          <CachedIsManaged>true</CachedIsManaged>        </Component>      </Components"+
">    </Stage>    <Stage>      <PolicyFileStage _locAttrData=\"Name\" _locID=\"3\" Name=\"Validate\" minOcc"+
"urs=\"0\" maxOccurs=\"-1\" execMethod=\"All\" stageId=\"9d0e410d-4cce-4536-83fa-4a5040674ad6\" />      <Comp"+
"onents />    </Stage>    <Stage>      <PolicyFileStage _locAttrData=\"Name\" _locID=\"4\" Name=\"ResolveP"+
"arty\" minOccurs=\"0\" maxOccurs=\"-1\" execMethod=\"All\" stageId=\"9d0e410e-4cce-4536-83fa-4a5040674ad6\" /"+
">      <Components />    </Stage>  </Stages></Document>";
        
        private const string _versionDependentGuid = "5471172a-bfe9-4185-9ac0-6513146ad51b";
        
        public JsonReceive()
        {
            Microsoft.BizTalk.PipelineOM.Stage stage = this.AddStage(new System.Guid("9d0e4103-4cce-4536-83fa-4a5040674ad6"), Microsoft.BizTalk.PipelineOM.ExecutionMode.all);
            IBaseComponent comp0 = Microsoft.BizTalk.PipelineOM.PipelineManager.CreateComponent("QuickLearn.BizTalk.JsonDecoder,JsonDecoder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=bab2a9ea69a59de0");;
            if (comp0 is IPersistPropertyBag)
            {
                string comp0XmlProperties = "<?xml version=\"1.0\" encoding=\"utf-16\"?><PropertyBag xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-inst"+
"ance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">  <Properties>    <Property Name=\"Namespace\">    "+
"  <Value xsi:type=\"xsd:string\">http://schemas.quicklearn.com/json/2013/09</Value>    </Property>    "+
"<Property Name=\"RootNode\">      <Value xsi:type=\"xsd:string\" />    </Property>  </Properties></Prope"+
"rtyBag>";
                PropertyBag pb = PropertyBag.DeserializeFromXml(comp0XmlProperties);;
                ((IPersistPropertyBag)(comp0)).Load(pb, 0);
            }
            this.AddComponent(stage, comp0);
            stage = this.AddStage(new System.Guid("9d0e4105-4cce-4536-83fa-4a5040674ad6"), Microsoft.BizTalk.PipelineOM.ExecutionMode.firstRecognized);
            IBaseComponent comp1 = Microsoft.BizTalk.PipelineOM.PipelineManager.CreateComponent("Microsoft.BizTalk.Component.XmlDasmComp,Microsoft.BizTalk.Pipeline.Components, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");;
            if (comp1 is IPersistPropertyBag)
            {
                string comp1XmlProperties = "<?xml version=\"1.0\" encoding=\"utf-16\"?><PropertyBag xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-inst"+
"ance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">  <Properties>    <Property Name=\"EnvelopeSpecNam"+
"es\">      <Value xsi:type=\"xsd:string\" />    </Property>    <Property Name=\"EnvelopeSpecTargetNamesp"+
"aces\">      <Value xsi:type=\"xsd:string\" />    </Property>    <Property Name=\"DocumentSpecNames\">   "+
"   <Value xsi:type=\"xsd:string\" />    </Property>    <Property Name=\"DocumentSpecTargetNamespaces\"> "+
"     <Value xsi:type=\"xsd:string\" />    </Property>    <Property Name=\"AllowUnrecognizedMessage\">   "+
"   <Value xsi:type=\"xsd:boolean\">false</Value>    </Property>    <Property Name=\"ValidateDocument\"> "+
"     <Value xsi:type=\"xsd:boolean\">false</Value>    </Property>    <Property Name=\"RecoverableInterc"+
"hangeProcessing\">      <Value xsi:type=\"xsd:boolean\">false</Value>    </Property>    <Property Name="+
"\"HiddenProperties\">      <Value xsi:type=\"xsd:string\">EnvelopeSpecTargetNamespaces,DocumentSpecTarge"+
"tNamespaces</Value>    </Property>  </Properties></PropertyBag>";
                PropertyBag pb = PropertyBag.DeserializeFromXml(comp1XmlProperties);;
                ((IPersistPropertyBag)(comp1)).Load(pb, 0);
            }
            this.AddComponent(stage, comp1);
        }
        
        public override string XmlContent
        {
            get
            {
                return _strPipeline;
            }
        }
        
        public override System.Guid VersionDependentGuid
        {
            get
            {
                return new System.Guid(_versionDependentGuid);
            }
        }
    }
}
