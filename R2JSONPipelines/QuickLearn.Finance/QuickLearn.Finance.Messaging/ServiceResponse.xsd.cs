namespace QuickLearn.Finance.Messaging {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://schemas.finance.yahoo.com/API/2014/08/",@"ServiceResponse")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"ServiceResponse"})]
    public sealed class ServiceResponse : Microsoft.BizTalk.TestTools.Schema.TestableSchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" attributeFormDefault=""unqualified"" elementFormDefault=""unqualified"" targetNamespace=""http://schemas.finance.yahoo.com/API/2014/08/"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:element name=""ServiceResponse"">
    <xs:complexType>
      <xs:sequence>
        <xs:element name=""query"">
          <xs:complexType>
            <xs:sequence>
              <xs:element name=""count"" type=""xs:unsignedByte"" />
              <xs:element name=""created"" type=""xs:dateTime"" />
              <xs:element name=""lang"" type=""xs:string"" />
              <xs:element name=""results"">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element minOccurs=""0"" maxOccurs=""unbounded"" name=""quote"">
                      <xs:complexType>
                        <xs:sequence>
                          <xs:element minOccurs=""0"" name=""symbol"" type=""xs:string"" />
                          <xs:element minOccurs=""0"" name=""Ask"" type=""xs:string"" />
                          <xs:element minOccurs=""0"" name=""Bid"" type=""xs:decimal"" />
                        </xs:sequence>
                      </xs:complexType>
                    </xs:element>
                  </xs:sequence>
                </xs:complexType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public ServiceResponse() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "ServiceResponse";
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
