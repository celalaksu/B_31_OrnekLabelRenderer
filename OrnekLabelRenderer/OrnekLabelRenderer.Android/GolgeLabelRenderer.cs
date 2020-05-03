using Android.Content;
using OrnekLabelRenderer.Droid;
using OrnekLabelRenderer.OzelRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(GolgeLabel), typeof(GolgeLabelRenderer))]
namespace OrnekLabelRenderer.Droid
{
    public class GolgeLabelRenderer : Xamarin.Forms.Platform.Android.LabelRenderer
    {
        public GolgeLabelRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            Control.SetShadowLayer(10, 5, 5, Android.Graphics.Color.DarkGray);
        }
    }
}