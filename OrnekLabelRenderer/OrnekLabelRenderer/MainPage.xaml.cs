using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OrnekLabelRenderer
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            temizleTB.IsEnabled = false;
        }

        private void RenkDegistir(object sender, EventArgs e)
        {
            Random rand = new Random();
            var yeniRenk = new Color(rand.NextDouble(), rand.NextDouble(), rand.NextDouble());
            cizimGorunumu.CizimRengi = yeniRenk;
            
        }

        private void TemizlemeyiEtkinlestir(object sender, EventArgs e)
        {
            temizleTB.IsEnabled = true;
        }

        private  async void YeniCizimOlustur(object sender, EventArgs e)
        {
            
            cizimGorunumu.Temizle();
            
        }

        private async void kaydetTB_Clicked(object sender, EventArgs e)
        {
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            var response = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
            cizimGorunumu.Kaydet();

        }
    }
}
