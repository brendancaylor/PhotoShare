using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Utilities
{
    public class ImageManipulation
    {
        public bool ThumbnailCallback()
        {
            return false;
        }

        public byte[] Crop(byte[] imageByteArray, int height, int width)
        {
            byte[] data;
            using (Stream imageStream = new MemoryStream(imageByteArray))
            {
                using (Image image = Image.FromStream(imageStream))
                {
                    var img = FixedSize(image, width, height, true);
                    using (MemoryStream m = new MemoryStream())
                    {
                        img.Save(m, image.RawFormat);
                        data = m.ToArray();
                        return data;
                    }
                }
            }
        }


        public byte[] Resize(byte[] imageByteArray, int height, out int newWidth)
        {
            newWidth = 0;
            byte[] data;
            using (Stream imageStream = new MemoryStream(imageByteArray))
            {
                Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

                try
                {

                    using (Image image = Image.FromStream(imageStream))
                    {
                        var newHeight = image.Height < height ? image.Height : height;
                        newWidth = (int)(

                            ((float)image.Width / (float)image.Height)
                            * (float)newHeight
                            );

                        var newImg = new Bitmap(newWidth, newHeight);
                        using (Graphics g = Graphics.FromImage(newImg))
                        {
                            g.DrawImage(image, new Rectangle(0, 0, newWidth, newHeight));
                            
                            //var thumbnail = image.GetThumbnailImage(newWidth, newHeight, myCallback, IntPtr.Zero);
                            using (MemoryStream m = new MemoryStream())
                            {
                                //thumbnail.Save(m, image.RawFormat);
                                newImg.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
                                data = m.ToArray();
                            }
                        }                       
                    }

                    return data;
                }
                catch (Exception)
                {


                }
            }
            return null;
        }

        public static System.Drawing.Image FixedSize(Image image, int Width, int Height, bool needToFill)
        {

            int sourceWidth = image.Width;
            int sourceHeight = image.Height;
            int sourceX = 0;
            int sourceY = 0;
            double destX = 0;
            double destY = 0;

            double nScale = 0;
            double nScaleW = 0;
            double nScaleH = 0;

            nScaleW = ((double)Width / (double)sourceWidth);
            nScaleH = ((double)Height / (double)sourceHeight);
            if (!needToFill)
            {
                nScale = Math.Min(nScaleH, nScaleW);
            }
            else
            {
                nScale = Math.Max(nScaleH, nScaleW);
                destY = (Height - sourceHeight * nScale) / 2;
                destX = (Width - sourceWidth * nScale) / 2;
            }

            if (nScale > 1)
                nScale = 1;

            int destWidth = (int)Math.Round(sourceWidth * nScale);
            int destHeight = (int)Math.Round(sourceHeight * nScale);

            System.Drawing.Bitmap bmPhoto = null;
            try
            {
                bmPhoto = new System.Drawing.Bitmap(destWidth + (int)Math.Round(2 * destX), destHeight + (int)Math.Round(2 * destY));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("destWidth:{0}, destX:{1}, destHeight:{2}, desxtY:{3}, Width:{4}, Height:{5}",
                    destWidth, destX, destHeight, destY, Width, Height), ex);
            }
            using (System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto))
            {

                Rectangle to = new System.Drawing.Rectangle((int)Math.Round(destX), (int)Math.Round(destY), destWidth, destHeight);
                Rectangle from = new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);
                //Console.WriteLine("From: " + from.ToString());
                //Console.WriteLine("To: " + to.ToString());
                grPhoto.DrawImage(image, to, from, System.Drawing.GraphicsUnit.Pixel);

                return bmPhoto;
            }
        }

    }
}
