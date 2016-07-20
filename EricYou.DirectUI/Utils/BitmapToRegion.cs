using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace EricYou.DirectUI.Utils
{
    public static class BitmapToRegion
    {
        public static Region Convert(Bitmap bitmap, Color transparencyKey)
        {
            return Convert(bitmap, transparencyKey, TransparencyMode.ColorKeyTransparent);
        }

        public static unsafe Region Convert(Bitmap bitmap, Color transparencyKey, TransparencyMode mode)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("Bitmap", "Bitmap cannot be null!");
            }
            bool flag = mode == TransparencyMode.ColorKeyOpaque;
            GraphicsUnit pixel = GraphicsUnit.Pixel;
            RectangleF bounds = bitmap.GetBounds(ref pixel);
            Rectangle rect = new Rectangle((int)bounds.Left, (int)bounds.Top, (int)bounds.Width, (int)bounds.Height);
            uint num = (uint)((((transparencyKey.A << 0x18) | (transparencyKey.R << 0x10)) | (transparencyKey.G << 8)) | transparencyKey.B);
            BitmapData bitmapdata = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            uint* numPtr = (uint*)bitmapdata.Scan0.ToPointer();
            int height = (int)bounds.Height;
            int width = (int)bounds.Width;
            GraphicsPath path = new GraphicsPath();
            for (int i = 0; i < height; i++)
            {
                byte* numPtr2 = (byte*)numPtr;
                int num5 = 0;
                while (num5 < width)
                {
                    if (!(flag ^ (numPtr[0] == num)))
                    {
                        int x = num5;
                        while ((num5 < width) && !(flag ^ (numPtr[0] == num)))
                        {
                            num5++;
                            numPtr++;
                        }
                        path.AddRectangle(new Rectangle(x, i, num5 - x, 1));
                    }
                    num5++;
                    numPtr++;
                }
                numPtr = (uint*)(numPtr2 + bitmapdata.Stride);
            }
            Region region = new Region(path);
            path.Dispose();
            bitmap.UnlockBits(bitmapdata);
            return region;
        }

        public static unsafe Region Convert(Bitmap bitmap, Color transparencyKey, TransparencyMode mode, int orignX, int orignY)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("Bitmap", "Bitmap cannot be null!");
            }
            bool flag = mode == TransparencyMode.ColorKeyOpaque;
            GraphicsUnit pixel = GraphicsUnit.Pixel;
            RectangleF bounds = bitmap.GetBounds(ref pixel);
            Rectangle rect = new Rectangle((int)bounds.Left, (int)bounds.Top, (int)bounds.Width, (int)bounds.Height);
            uint num = (uint)((((transparencyKey.A << 0x18) | (transparencyKey.R << 0x10)) | (transparencyKey.G << 8)) | transparencyKey.B);
            BitmapData bitmapdata = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            uint* numPtr = (uint*)bitmapdata.Scan0.ToPointer();
            int height = (int)bounds.Height;
            int width = (int)bounds.Width;
            GraphicsPath path = new GraphicsPath();
            for (int i = 0; i < height; i++)
            {
                byte* numPtr2 = (byte*)numPtr;
                int num5 = 0;
                while (num5 < width)
                {
                    if (!(flag ^ (numPtr[0] == num)))
                    {
                        int num6 = num5;
                        while ((num5 < width) && !(flag ^ (numPtr[0] == num)))
                        {
                            num5++;
                            numPtr++;
                        }
                        path.AddRectangle(new Rectangle(num6 + orignX, i + orignY, num5 - num6, 1));
                    }
                    num5++;
                    numPtr++;
                }
                numPtr = (uint*)(numPtr2 + bitmapdata.Stride);
            }
            Region region = new Region(path);
            path.Dispose();
            bitmap.UnlockBits(bitmapdata);
            return region;
        }
    }

    public enum TransparencyMode
    {
        ColorKeyOpaque = 2,
        ColorKeyTransparent = 1
    }

}

