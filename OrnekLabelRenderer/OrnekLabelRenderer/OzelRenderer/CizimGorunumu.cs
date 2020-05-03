using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace OrnekLabelRenderer.OzelRenderer
{
    public class CizimGorunumu:View, ICizimGorunumu
    {
        public static readonly BindableProperty CizimRengiOzelligi = BindableProperty.Create(nameof(CizimRengi), typeof(Color), typeof(CizimGorunumu), Color.Blue);

        public Color CizimRengi
        {
            get { return (Color)GetValue(CizimRengiOzelligi); }
            set { SetValue(CizimRengiOzelligi, value); }
        }

        public void Temizle()
        {
            MessagingCenter.Send(this, "Temizle");
        }

        public void Kaydet()
        {
            MessagingCenter.Send(this, "Kaydet");
        }

        public void CizimBasladiBildir()
        {
            if (CizimBasladi != null)
                CizimBasladi(this, EventArgs.Empty);
        }

        public event EventHandler CizimBasladi;
    }
}
