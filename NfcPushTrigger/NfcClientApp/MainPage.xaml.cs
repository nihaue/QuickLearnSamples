using NfcClientApp;
using QuickLearn.LogicApps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Proximity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DataModels
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public string lastTagRead { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            var nfcReader = ProximityDevice.GetDefault();

            nfcReader.SubscribeForMessage("NDEF",
                async (rdr, msg) =>
                {
                    var tagData = msg.Data.ToArray();
                    var currentTagRead = Encoding.ASCII.GetString(tagData);

                    var isDuplicate = currentTagRead == lastTagRead;

                    lastTagRead = currentTagRead;

                    var tagDataB64 = Convert.ToBase64String(tagData);

                    IClientCallbackStore<PushTriggerConfiguration, PushTriggerOutput> callbackStore = new AzureMobileAppCallbackStore();

                    var allCallbacks = await callbackStore.ReadCallbacksAsync();

                    foreach (var callback in allCallbacks)
                    {
                        if (callback.Configuration.SuppressDuplicates && isDuplicate)
                            continue;

                        await callback.InvokeAsync(new PushTriggerOutput()
                        {
                            TagReadB64 = tagDataB64
                        });
                    }
                }
                );
        }
    }
}
