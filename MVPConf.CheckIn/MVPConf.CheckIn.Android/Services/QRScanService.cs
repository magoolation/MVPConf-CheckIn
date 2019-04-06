using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MVPConf.CheckIn.Services;
using ZXing;
using ZXing.Mobile;

namespace MVPConf.CheckIn.Droid.Services
{
    class QRScanService : IQRScanService
    {
        public async Task<string> Scan()
        {
            var options = new MobileBarcodeScanningOptions()
            {
                UseFrontCameraIfAvailable = false,
                PossibleFormats = new List<BarcodeFormat>() { BarcodeFormat.QR_CODE }
            };

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Aproxime o crachá para fazer a leitura",
                BottomText = "Toque na tela para focar"
            };

            var results = await scanner.Scan(options);

            return results?.Text ?? string.Empty;
        }
    }
}