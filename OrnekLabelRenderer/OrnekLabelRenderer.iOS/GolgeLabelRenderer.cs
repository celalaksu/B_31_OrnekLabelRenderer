using CoreGraphics;
using OrnekLabelRenderer.iOS;
using OrnekLabelRenderer.OzelRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GolgeLabel), typeof(GolgeLabelRenderer))]
namespace OrnekLabelRenderer.iOS
{
    public class GolgeLabelRenderer : Xamarin.Forms.Platform.iOS.LabelRenderer
    {
        public GolgeLabelRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            Control.Layer.ShadowColor = UIColor.DarkGray.CGColor;
            Control.Layer.ShadowOpacity = 1.0f;
            Control.Layer.ShadowRadius = 2f;
            Control.Layer.ShadowOffset = new CGSize(4, 4);
            Control.Layer.MasksToBounds = false;
        }
    }
}