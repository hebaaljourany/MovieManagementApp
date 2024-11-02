using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace MovieManagementApp.Helpers
{
    public class ThumbnailGenerator
    {

        public static string CreateThumbnailFromBase64(string base64String, int height , int width)
        {
            // Step 1: Convert Base64 string to byte array
            byte[] bytes = Convert.FromBase64String(base64String);

            // Step 2: Create a MemoryStream from the byte array
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                // Step 3: Create a new Bitmap for the thumbnail
                using (Bitmap thumb = new Bitmap(width, height))
                {
                    // Step 4: Load the original image from the MemoryStream
                    using (Image bmp = Image.FromStream(ms))
                    {
                        // Step 5: Create a Graphics object to draw on the thumbnail bitmap
                        using (Graphics g = Graphics.FromImage(thumb))
                        {
                            // Set quality settings for drawing
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.CompositingQuality = CompositingQuality.HighQuality;
                            g.SmoothingMode = SmoothingMode.HighQuality;

                            // Draw the original image onto the thumbnail bitmap
                            g.DrawImage(bmp, 0, 0, width, height);
                        }
                    }

                    // Step 6: Save the thumbnail to a MemoryStream in PNG format
                    using (MemoryStream thumbnailStream = new MemoryStream())
                    {
                        thumb.Save(thumbnailStream, System.Drawing.Imaging.ImageFormat.Png);
                        // Convert the thumbnail MemoryStream to a Base64 string and return it
                        return Convert.ToBase64String(thumbnailStream.ToArray());
                    }
                }
            }
        }
        public static byte[] GetBytes(string base64)
        {
            return Convert.FromBase64String(base64);
        }
        public static string GetBase64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
    }
}
