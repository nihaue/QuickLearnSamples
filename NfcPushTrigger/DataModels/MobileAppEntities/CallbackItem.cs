// TODO: Create a new Azure Mobile App
// TODO: Download the Quick Start project (make sure to enable the Data feature so that you have a SQL database)
// TODO: In the Azure Mobile App Replace all instances of "Todo" with "Callback"
// TODO: Update the CallbackItem class in the Azure Mobile App so that it has only the following properties (you don't have to include Id since it will already be present from the base class)
// TODO: Download and import the publishing profile for you Azure Mobile App and deploy it
// TODO: Update the data access classes that reference this class with the gateway URL and the Mobile App URL for your Mobile App

namespace DataModels.MobileAppEntities
{
    public class CallbackItem
    {
        
        public string Id { get; set; }

        public string TriggerId { get; set; }

        public string CallbackUri { get; set; }

        public bool SuppressDuplicates { get; set; }

    }
}
