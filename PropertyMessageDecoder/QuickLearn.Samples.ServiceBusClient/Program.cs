using Microsoft.ServiceBus;

namespace QuickLearn.Samples.ServiceBusClient
{
    class Program
    {
        // TODO: Provide these magic values

        const string SB_NAMESPACE =   /* Your service bus namespace name: */ "";
        const string SB_KEY = /* Your shared access key (in conn string): */ "";
        const string QUEUE_NAME =      /* Name of the sb queue to create  */ "";

        static void Main(string[] args)
        {
            string connString = string.Format("Endpoint=sb://{0}.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey={1}",
                                                SB_NAMESPACE, SB_KEY);
            createQueue(connString);

            var client = Microsoft.ServiceBus.Messaging.QueueClient.CreateFromConnectionString(connString, QUEUE_NAME);

            var message = new Microsoft.ServiceBus.Messaging.BrokeredMessage();

            message.Properties.Add("CustomProperty", "Value1");
            message.Properties.Add("CustomProperty2", "Value2");

            client.Send(message);

        }

        private static void createQueue(string connString)
        {
            var nsMgr = NamespaceManager.CreateFromConnectionString(connString);

            if (!nsMgr.QueueExists(QUEUE_NAME))
                nsMgr.CreateQueue(new Microsoft.ServiceBus.Messaging.QueueDescription(QUEUE_NAME));
        }
    }
}
