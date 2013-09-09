namespace QuickLearn.BizTalk.ItemListProcessor.Schemas {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://schemas.quicklearn.com/json/2013/09",@"ItemAverageResponse")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"ItemAverageResponse"})]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"QuickLearn.BizTalk.ItemListProcessor.Schemas.ItemAverageResponseTypes", typeof(global::QuickLearn.BizTalk.ItemListProcessor.Schemas.ItemAverageResponseTypes))]
    public sealed class ItemAverageResponse : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" xmlns:ns0=""http://schemas.quicklearn.com/json/2013/09"" attributeFormDefault=""unqualified"" elementFormDefault=""qualified"" targetNamespace=""http://schemas.quicklearn.com/json/2013/09"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:import schemaLocation=""QuickLearn.BizTalk.ItemListProcessor.Schemas.ItemAverageResponseTypes"" />
  <xs:element name=""ItemAverageResponse"">
    <xs:complexType>
      <xs:sequence>
        <xs:element ref=""itemCount"" />
        <xs:element ref=""itemAmountAvg"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public ItemAverageResponse() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "ItemAverageResponse";
                return _RootElements;
            }
        }
        
        protected override object RawSchema {
            get {
                return _rawSchema;
            }
            set {
                _rawSchema = value;
            }
        }
    }
}
