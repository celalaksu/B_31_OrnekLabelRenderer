using System;
using System.Collections.Generic;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Java.IO;
using Plugin.Permissions;


namespace OrnekLabelRenderer.Droid
{
    class AndroidCizimGorunumu :View
    {
        public event EventHandler CizimBasladi;

        Dictionary<int, MotionEvent.PointerCoords> koordinatlar = new Dictionary<int, MotionEvent.PointerCoords>();

        Canvas cizimAlani;
        Bitmap cizilenResimAlani;
        
        Paint cizilenResim;

        public AndroidCizimGorunumu(Context context) : base(context, null, 0)
        {
            cizilenResim = new Paint() { Color = Color.Blue, StrokeWidth = 5f, AntiAlias = true };
            cizilenResim.SetStyle(Paint.Style.Stroke);
        }
        /*
        public AndroidCizimGorunumu(Context context, IAttributeSet attrs) : base(context, attrs) { }
        public AndroidCizimGorunumu(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle) { }
        */

            
        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);

            
            cizilenResimAlani = Bitmap.CreateBitmap(w, h, Bitmap.Config.Argb4444); // full-screen bitmap
            
            cizimAlani = new Canvas(cizilenResimAlani); // the canvas will draw into the bitmap
            cizimAlani.DrawColor(Color.White);
        }
        

        public override bool OnTouchEvent(MotionEvent e)
        {
            switch (e.ActionMasked)
            {
                case MotionEventActions.Down:
                    {
                        int id = e.GetPointerId(0);

                        var start = new MotionEvent.PointerCoords();
                        e.GetPointerCoords(id, start);
                        koordinatlar.Add(id, start);

                        return true;
                    }

                case MotionEventActions.PointerDown:
                    {
                        int id = e.GetPointerId(e.ActionIndex);

                        var start = new MotionEvent.PointerCoords();
                        e.GetPointerCoords(id, start);
                        koordinatlar.Add(id, start);

                        return true;
                    }

                case MotionEventActions.Move:
                    {
                        
                        for (int index = 0; index < e.PointerCount; index++)
                        {
                            var id = e.GetPointerId(index);

                            float x = e.GetX(index);
                            float y = e.GetY(index);

                            cizimAlani.DrawLine(koordinatlar[id].X, koordinatlar[id].Y, x, y, cizilenResim);

                            koordinatlar[id].X = x;
                            koordinatlar[id].Y = y;

                            CizimBasladi?.Invoke(this, EventArgs.Empty);
                        }

                        Invalidate();

                        return true;
                    }

                case MotionEventActions.PointerUp:
                    {
                        int id = e.GetPointerId(e.ActionIndex);
                        koordinatlar.Remove(id);
                        return true;
                    }

                case MotionEventActions.Up:
                    {
                        int id = e.GetPointerId(0);
                        koordinatlar.Remove(id);
                        return true;
                    }

                default:
                    return false;
            }
        }
        
        protected override void OnDraw(Canvas canvas)
        {
            // Copy the off-screen canvas data onto the View from it's associated Bitmap (which stores the actual drawn data)
            canvas.DrawBitmap(cizilenResimAlani, 0, 0, null);
        }

        public void Kaydet()
        {
            try
            {

                String storagePath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
                File dir = new File(storagePath, "resimler");
                dir.Mkdir();
                var filePath = System.IO.Path.Combine(storagePath + "/resimler/", "test8.png");

                var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create);
                //BufferedOutputStream bos = new BufferedOutputStream(fos);
                cizilenResimAlani.Compress(Bitmap.CompressFormat.Png, 100, stream);
                //bos.Flush();
                stream.Close();
                /*
                var stream = new System.IO.FileStream("cizim.jpg", System.IO.FileMode.Create);
                cizilenResimAlani.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                stream.Close();
                */
            }
            catch (Exception e)
            {
                string hata = e.Message.ToString();
            }
        }
        
        public void Temizle()
        {
           
            
            
          
            //-----------------------
            cizimAlani.DrawColor(Color.White, PorterDuff.Mode.Clear); // Paint the off-screen buffer black

            Invalidate(); // Call Invalidate to redraw the view
        }
        
        public void CizimRengiAyarla(Color color)
        {
            cizilenResim.Color = color;
        }
    }
}