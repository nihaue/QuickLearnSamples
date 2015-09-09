using DataModels;
using Microsoft.Azure.AppService.ApiApps.Service;
using NfcPushTrigger.DataAccess;
using QuickLearn.LogicApps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TRex.Metadata;

namespace NfcPushTrigger.Controllers
{
    [RoutePrefix("callbacks")]
    public class CallbacksController : ApiController
    {
      
        // PUT callbacks/{triggerId}
        [HttpPut] [Route("{triggerId}")]
        [Metadata("NFC Tag Read")]
        [Trigger(TriggerType.Push, typeof(PushTriggerOutput))]
        public async Task<HttpResponseMessage> RegisterCallback(string triggerId,
            [FromBody]TriggerInput<PushTriggerConfiguration, PushTriggerOutput> parameters)
        {

            // Store the callback
            ICallbackStore<PushTriggerConfiguration> callbackStore = new AzureMobileAppCallbackStore();

            await callbackStore.WriteCallbackAsync(triggerId,
                parameters.GetCallback().CallbackUri,
                parameters.inputs);

            // Report that everything is happy
            return Request.PushTriggerRegistered(parameters.GetCallback());

        }

    }
}
