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
        private readonly MobileBarcodeScanningOptions options;
        private readonly MobileBarcodeScanner scanner;

        public QRScanService()
        {
            options = new MobileBarcodeScanningOptions()
            {
                UseFrontCameraIfAvailable = false,
                PossibleFormats = new List<BarcodeFormat>() { BarcodeFormat.QR_CODE }
            };
            scanner = new MobileBarcodeScanner()
            {
                TopText = "Aproxime o crachá para fazer a leitura",
                BottomText = "Toque na tela para focar"
            };
        }

        public async Task<string> Scan()
        {
            var result = await scanner.Scan(options);

            return result?.Text ?? string.Empty;
        }

        public void ScanContinuously(Action<string> onRead, Action onError = null)
        {
            scanner.ScanContinuously(options, result =>
            {
                if (result == null || string.IsNullOrEmpty(result?.Text))
                {
                    if (onError != null)
                    {
                        onError();
                    }
                }
                else
                {
                    onRead(result.Text);
                }
            });
        }
    }
}