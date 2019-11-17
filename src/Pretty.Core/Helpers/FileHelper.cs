using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Pretty.Core.Helpers
{
    public class FileHelper : IFileHelper
    {
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                { return codec; }
            }
            return null;
        }
        public bool CompressionImage(string filePath, string savePath, string fileName, long quality)
        {
            try
            {
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    using (System.Drawing.Image img = System.Drawing.Image.FromStream(fs))
                    {
                        using (Bitmap bitmap = new Bitmap(img))
                        {
                            ImageCodecInfo CodecInfo = GetEncoder(img.RawFormat);
                            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                            EncoderParameters myEncoderParameters = new EncoderParameters(1);
                            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
                            myEncoderParameters.Param[0] = myEncoderParameter;
                            using (MemoryStream ms = new MemoryStream())
                            {
                                bitmap.Save(Path.Combine(savePath, fileName), CodecInfo, myEncoderParameters);
                                myEncoderParameters.Dispose();
                                myEncoderParameter.Dispose();
                            }
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool IsImg(string fileFullName)
        {
            var imgExts = new string[] { ".jpg", ".gif", ".jpeg", ".bmp", ".png" };
            return imgExts.Contains(Path.GetExtension(fileFullName));
        }
    }
}
