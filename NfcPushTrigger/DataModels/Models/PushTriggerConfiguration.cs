using TRex.Metadata;

namespace DataModels
{
    public class PushTriggerConfiguration
    {
        [Metadata("Suppress Duplicates")]
        public bool SuppressDuplicates { get; set; }
    }
}