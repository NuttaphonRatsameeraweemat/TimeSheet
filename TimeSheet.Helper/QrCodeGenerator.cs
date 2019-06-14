using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TimeSheet.Helper
{
    /// <summary>
    /// The QrCode Generate using QRCoder library.
    /// </summary>
    public class QrCodeGenerator
    {

        #region [Methods]

        /// <summary>
        /// Render the qr code by content.
        /// </summary>
        /// <param name="content">The content of qr code.</param>
        /// <returns></returns>
        public string RenderQRCode(string content)
        {
            return this.ConvertBitmapToBase64String(this.GenerateQrCode(content));
        }

        /// <summary>
        /// Generate qr code bitmap.
        /// </summary>
        /// <param name="content">The content of qr code.</param>
        /// <returns></returns>
        private Bitmap GenerateQrCode(string content)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }

        /// <summary>
        /// Convert Bitmap type to base 64 string.
        /// </summary>
        /// <param name="qrCodeImage">The bitmap image.</param>
        /// <returns></returns>
        private string ConvertBitmapToBase64String(Bitmap qrCodeImage)
        {
            byte[] imageBytes;

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                imageBytes = stream.ToArray();
            }
            return Convert.ToBase64String(imageBytes);
        }

        #endregion



    }
}
