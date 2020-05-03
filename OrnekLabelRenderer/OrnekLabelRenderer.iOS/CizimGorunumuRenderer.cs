using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using OrnekLabelRenderer.iOS;
using OrnekLabelRenderer.OzelRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CizimGorunumu), typeof(CizimGorunumuRenderer))]
namespace OrnekLabelRenderer.iOS
{
    class CizimGorunumuRenderer: ViewRenderer<CizimGorunumu, iOSCizimGorunumu>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CizimGorunumu> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var cizimGorunumu = new iOSCizimGorunumu();
                cizimGorunumu.CizimRengiAyarla(this.Element.CizimRengi.ToUIColor());
                SetNativeControl(cizimGorunumu);

                MessagingCenter.Subscribe<CizimGorunumu>(this, "Temizle", TemizleMetoduMesaji);

                MessagingCenter.Subscribe<CizimGorunumu>(this, "Kaydet", KaydetMetoduMesaji);

                cizimGorunumu.CizimBasladi += CizimGorunumuCizimBasladi;
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CizimGorunumu.CizimRengiOzelligi.PropertyName)
            {
                Control.CizimRengiAyarla(Element.CizimRengi.ToUIColor());
            }
        }

        private void CizimGorunumuCizimBasladi(object sender, EventArgs e)
        {
            var cizimgorunumuBaglanti = (ICizimGorunumu)Element;

            if (cizimgorunumuBaglanti == null)
                return;

            cizimgorunumuBaglanti.CizimBasladiBildir();
        }

        private void KaydetMetoduMesaji(CizimGorunumu obj)
        {
            if (obj == Element)
                Control.Kaydet();
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