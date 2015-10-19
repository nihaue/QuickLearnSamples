using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json.Linq;
using QuickLearn.LogicApps;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using QuickLearn.ButtonPress.DataAccess;

namespace QuickLearn.ButtonPress.App.DataAccess
{

    internal class AzureStorageClientCallbackStore<TConfiguration> : IClientCallbackStore<TConfiguration>
    {

        public async Task<IEnumerable<Callback<TConfiguration>>> ReadCallbacksAsync()
        {
            CloudTable callbacksTable = await GetCallbacksTable();

            var query = new TableQuery<CallbackItem<TConfiguration>>()
                                .Where(TableQuery.GenerateFilterCondition("PartitionKey",
                                        QueryComparisons.Equal,
                                        AzureStorageConfig.PartitionKey));

            var token = new TableContinuationToken();

            var results = new List<Callback<TConfiguration>>();

            var segment = await callbacksTable.ExecuteQuerySegmentedAsync(query, token);

            while (token != null)
            {
                results.AddRange(from c in segment.Results
                                 select new Callback<TConfiguration>(
                                    new Uri(c.CallbackUrl),
                                    JToken.Parse(c.JsonConfig).ToObject<TConfiguration>()));

                token = segment.ContinuationToken;

                segment = await callbacksTable.ExecuteQuerySegmentedAsync(query, token);
            }

            return results;
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

    internal class CallbackItem<TConfiguration> : TableEntity
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
