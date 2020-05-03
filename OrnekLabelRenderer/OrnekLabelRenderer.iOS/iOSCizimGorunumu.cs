using System;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using UIKit;

namespace OrnekLabelRenderer.iOS
{
    class iOSCizimGorunumu:UIImageView
    {
        public event EventHandler CizimBasladi;

        UIColor cizimRengi = UIColor.Blue;

        public iOSCizimGorunumu()
        {
            MultipleTouchEnabled = true;
            UserInteractionEnabled = true;
        }

        public void CizimRengiAyarla(UIColor color)
        {
            cizimRengi = color;
        }

        public void Kaydet()
        {
            try
            {
                TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
                var data = Image.AsJPEG();
                NSData nsdata = NSData.FromData(data);
                UIImage image = new UIImage(nsdata);

                image.SaveToPhotosAlbum((UIImage img, NSError error) => {
                    taskCompletionSource.SetResult(error == null);
                });

            }
            catch (Exception e)
            {
                string hata = e.Message.ToString();
            }
        }
        
        public void Temizle()
        {
           

            if (Image != null)
                Image.Dispose();
            Image = new UIImage();
        }
        
    
        void CizgiCiz(CGPoint pt1, CGPoint pt2, UIColor color)
        {
            UIGraphics.BeginImageContext(Frame.Size);

            using (var g = UIGraphics.GetCurrentContext())
            {
                Layer.RenderInContext(g);

                var path = new CGPath();

                path.AddLines(new CGPoint[] { pt1, pt2 });

                g.SetLineWidth(3);
                color.SetStroke();

                g.AddPath(path);
                g.DrawPath(CGPathDrawingMode.Stroke);

                Image = UIGraphics.GetImageFromCurrentImageContext();
            }

            UIGraphics.EndImageContext();
            
            if (CizimBasladi != null)
                CizimBasladi(this, EventArgs.Empty);
                
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            foreach (UITouch touch in touches)
            {
                CizgiCiz(touch.PreviousLocationInView(this), touch.LocationInView(this), cizimRengi);
            }
        }
    }
}