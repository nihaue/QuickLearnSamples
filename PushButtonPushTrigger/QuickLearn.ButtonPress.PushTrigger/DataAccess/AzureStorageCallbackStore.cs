using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json.Linq;
using QuickLearn.LogicApps;
using System;
using System.Threading.Tasks;

namespace QuickLearn.ButtonPress.PushTrigger.DataAccess
{

    public class AzureStorageCallbackStore<TConfiguration> : ICallbackStore<TConfiguration>
    {
        public async Task WriteCallbackAsync(string triggerId, Uri callbackUri, TConfiguration triggerConfig)
        {
            CloudTable callbacksTable = await GetCallbacksTable();

            await callbacksTable.ExecuteAsync(TableOperation.InsertOrReplace(
                new CallbackItem<TConfiguration>(triggerId, callbackUri.ToString(), triggerConfig)));

            return;
        }

        public async Task DeleteCallbackAsync(string triggerId)
        {
            CloudTable callbacksTable = await GetCallbacksTable();

            var matchingItem = await callbacksTable.ExecuteAsync(TableOperation.Retrieve<CallbackItem<TConfiguration>>(
                                                                    AzureStorageConfig.PartitionKey,
                                                                    triggerId));

            if (matchingItem.Result != null)
                await callbacksTable.ExecuteAsync(TableOperation.Delete(matchingItem.Result as CallbackItem<TConfiguration>));

            return;
        }

        private async Task<CloudTable> GetCallbacksTable()
        {
            var callbacksTableName = "callbacks";
            var client = AzureStorageConfig.StorageAccount.CreateCloudTableClient();
            var callbacksTable = client.GetTableReference(callbacksTableName);
            await callbacksTable.CreateIfNotExistsAsync();
            return callbacksTable;
        }
    }

    public class CallbackItem<TConfiguration> : TableEntity
    {
        public CallbackItem(string triggerId, string callbackUrl, TConfiguration config)
        {
            this.PartitionKey = AzureStorageConfig.PartitionKey;
            this.RowKey = triggerId;
            this.JsonConfig = JToken.FromObject(config).ToString();
            this.CallbackUrl = callbackUrl;
        }

        public CallbackItem()
        {

        }

        public string JsonConfig { get; set; }

        public string CallbackUrl { get; set; }
    }


}
