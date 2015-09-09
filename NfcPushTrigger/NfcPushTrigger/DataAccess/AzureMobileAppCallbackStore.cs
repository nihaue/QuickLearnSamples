using DataModels;
using DataModels.MobileAppEntities;
using Microsoft.WindowsAzure.MobileServices;
using QuickLearn.LogicApps;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NfcPushTrigger.DataAccess
{
    public class AzureMobileAppCallbackStore : ICallbackStore<PushTriggerConfiguration>
    {

        public async Task WriteCallbackAsync(string triggerId, Uri callbackUri, PushTriggerConfiguration triggerConfig)
        {

            var callbackItemsTable = client.GetTable<CallbackItem>();
            var existingCallback = await callbackItemsTable.Where(c => c.TriggerId == triggerId).ToEnumerableAsync();

            if (existingCallback.Any())
            {
                var currentCallbackItem = existingCallback.FirstOrDefault();
                currentCallbackItem.CallbackUri = callbackUri.ToString();
                currentCallbackItem.SuppressDuplicates = triggerConfig.SuppressDuplicates;
                await callbackItemsTable.UpdateAsync(currentCallbackItem);
            }
            else
            {
                await callbackItemsTable.InsertAsync(new CallbackItem()
                {
                    CallbackUri = callbackUri.ToString(),
                    SuppressDuplicates = triggerConfig.SuppressDuplicates,
                    Id = Guid.NewGuid().ToString("N"),
                    TriggerId = triggerId
                });
            }

            return;
        }

        public async Task DeleteCallbackAsync(string triggerId)
        {
            var callbackItemsTable = client.GetTable<CallbackItem>();
            var existingCallback = await callbackItemsTable.Where(c => c.TriggerId == triggerId).ToEnumerableAsync();

            if (existingCallback.Any())
            {
                await callbackItemsTable.DeleteAsync(existingCallback.FirstOrDefault());
            }

            return;
        }


	// TODO: Include your Azure Mobile App credentials below
        MobileServiceClient client = new MobileServiceClient(
        #region Credentials
            mobileAppUri: "",
            gatewayUri: "",
            applicationKey: ""
        #endregion
            );


    }
}