using TRex.Metadata;

namespace QuickLearn.ButtonPress.Models
{
    public class PushTriggerConfiguration
    {
        [Metadata("Enabled", "Specifies whether or not the trigger is enabled")]
        public bool Enabled { get; set; }
    }
}
