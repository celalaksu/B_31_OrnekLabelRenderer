
using System;
using System.ComponentModel;
using Android.Content;
using OrnekLabelRenderer.Droid;
using OrnekLabelRenderer.OzelRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CizimGorunumu), typeof(CizimGorunumuRenderer))]
namespace OrnekLabelRenderer.Droid
{
    class CizimGorunumuRenderer : ViewRenderer<CizimGorunumu, AndroidCizimGorunumu>
    {
        Context context;

        public CizimGorunumuRenderer(Context context) : base(context)
        {
            this.context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CizimGorunumu> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var cizimGorunumu = new AndroidCizimGorunumu(context);
                cizimGorunumu.CizimRengiAyarla(Element.CizimRengi.ToAndroid());
                SetNativeControl(cizimGorunumu);

                MessagingCenter.Subscribe<CizimGorunumu>(this, "Temizle", TemizleMetoduMesaji);

                MessagingCenter.Subscribe<CizimGorunumu>(this, "Kaydet", KaydetMetoduMesaji);

                cizimGorunumu.CizimBasladi += CizimGorunumuCizimBasladi;
            }
        }

        private void KaydetMetoduMesaji(CizimGorunumu obj)
        {
            if (obj == Element)
                Control.Kaydet();
        }

        private void CizimGorunumuCizimBasladi(object sender, EventArgs e)
        {
            var cizimgorunumuBaglanti = (ICizimGorunumu)Element;

            if (cizimgorunumuBaglanti == null)
                return;

            cizimgorunumuBaglanti.CizimBasladiBildir();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CizimGorunumu.CizimRengiOzelligi.PropertyName)
            {
                Control.CizimRengiAyarla(Element.CizimRengi.ToAndroid());
            }
        }

        void TemizleMetoduMesaji(CizimGorunumu sender)
        {
            if (sender == Element)
                Control.Temizle();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                MessagingCenter.Unsubscribe<CizimGorunumu>(this, "Temizle");
                MessagingCenter.Unsubscribe<CizimGorunumu>(this, "Kaydet");
            }

            base.Dispose(disposing);
        }
    }
}