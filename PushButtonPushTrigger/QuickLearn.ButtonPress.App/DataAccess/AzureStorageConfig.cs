using Microsoft.WindowsAzure.Storage;

namespace QuickLearn.ButtonPress.DataAccess
{
    internal static class AzureStorageConfig
    {
        internal const string PartitionKey = "ButtonPress";

        // TODO: Retrieve your credentials from whatever configuration source that you plase
        private static CloudStorageAccount storageAccount =
            CloudStorageAccount.Parse("" /* TODO: Connection String Goes Here */);

        internal static CloudStorageAccount StorageAccount
        {
            get
            {
                return storageAccount;
            }

            set
            {
                storageAccount = value;
            }
        }
    }
}
