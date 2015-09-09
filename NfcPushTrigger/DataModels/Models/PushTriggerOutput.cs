using TRex.Metadata;

namespace DataModels
{
    public class PushTriggerOutput
    {
        [Metadata("Base64 NFC Tag Read")]
        public string TagReadB64 { get; set; }
    }
}