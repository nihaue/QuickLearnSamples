using Microsoft.Azure.AppService.ApiApps.Service;
using QuickLearn.ButtonPress.Models;
using QuickLearn.ButtonPress.PushTrigger.DataAccess;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TRex.Metadata;

namespace QuickLearn.ButtonPress.PushTrigger.Controllers
{
    [RoutePrefix("callbacks")]
    public class CallbacksController : ApiController
    {
        private AzureStorageCallbackStore<PushTriggerConfiguration> _callbackStore;

        #region Constructors

        public CallbacksController() : this(new AzureStorageCallbackStore<PushTriggerConfiguration>())
        {

        }

        public CallbacksController(AzureStorageCallbackStore<PushTriggerConfiguration> azureStorageCallbackStore)
        {
            this._callbackStore = azureStorageCallbackStore;
        }

        #endregion

        // PUT api/values/5
        [Trigger(TriggerType.Push)]
        [Metadata("Push Button Pressed", "Fires whenever the push button has been pressed")]
        [Route("{triggerId}")][HttpPut]
        
        public async Task<HttpResponseMessage> RegisterCallback(string triggerId, [FromBody]TriggerInput<PushTriggerConfiguration> parameters)
        {
            // Store the callback
            await _callbackStore.WriteCallbackAsync(triggerId, parameters.GetCallback().CallbackUri, parameters.inputs);

            // Report success
            return Request.PushTriggerRegistered(parameters.GetCallback());
        }

        // DELETE api/values/5
        [UnregisterCallback]
        [Route("{triggerId}")][HttpDelete]
        public async Task<HttpResponseMessage> Delete(string triggerId)
        {
            // Delete the callback
            await _callbackStore.DeleteCallbackAsync(triggerId);

            // Report success
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
