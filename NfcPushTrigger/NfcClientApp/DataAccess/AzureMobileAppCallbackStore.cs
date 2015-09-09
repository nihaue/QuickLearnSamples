using DataModels;
using DataModels.MobileAppEntities;
using Microsoft.WindowsAzure.MobileServices;
using QuickLearn.LogicApps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcClientApp
{
    public class AzureMobileAppCallbackStore : IClientCallbackStore<PushTriggerConfiguration, PushTriggerOutput>
    {


        public async Task<IEnumerable<Callback<PushTriggerConfiguration, PushTriggerOutput>>> ReadCallbacksAsync()
        {
            var callbackItemsTable = client.GetTable<CallbackItem>();

            var allCallbacks = await callbackItemsTable.ToEnumerableAsync();

            return from cb in allCallbacks
                   select new Callback<PushTriggerConfiguration, PushTriggerOutput>()
                   {
                       Configuration = new PushTriggerConfiguration()
                       {
                           SuppressDuplicates = cb.SuppressDuplicates
                       },
                       UriWithCredentials = new Uri(cb.CallbackUri)
                   };
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
