using QuickLearn.ButtonPress.App.DataAccess;
using QuickLearn.ButtonPress.Models;
using QuickLearn.LogicApps;
using System;
using Windows.ApplicationModel.Background;
using Windows.Devices.Gpio;

namespace QuickLearn.ButtonPress
{
    public sealed class StartupTask : IBackgroundTask
    {
        GpioPin buttonPin = null;
        BackgroundTaskDeferral deferral = null;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var gpio = GpioController.GetDefault();
            buttonPin = gpio.OpenPin(4, GpioSharingMode.SharedReadOnly);
            buttonPin.DebounceTimeout = TimeSpan.FromMilliseconds(50);
            buttonPin.ValueChanged += ButtonPin_ValueChanged;
            deferral = taskInstance.GetDeferral();
        }

        private async void ButtonPin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            if (args.Edge == GpioPinEdge.FallingEdge)
            {
                // Get Registered Callbacks
                IClientCallbackStore<PushTriggerConfiguration> callbackStore = new AzureStorageClientCallbackStore<PushTriggerConfiguration>();
                var callbacks = await callbackStore.ReadCallbacksAsync();

                // Trigger Logic App
                foreach (var callback in callbacks)
                {
                    if (!callback.Configuration.Enabled) continue;
                    await callback.InvokeAsync();
                }

            }
        }
    }
}
