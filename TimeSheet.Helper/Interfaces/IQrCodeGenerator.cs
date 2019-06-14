using System;
using System.Collections.Generic;
using System.Text;

namespace TimeSheet.Helper.Interfaces
{
    /// <summary>
    /// The QrCode Generate using QRCoder library.
    /// </summary>
    public interface IQrCodeGenerator
    {

        /// <summary>
        /// Render the qr code by content.
        /// </summary>
        /// <param name="content">The content of qr code.</param>
        /// <returns></returns>
        string RenderQRCode(string content);

    }
}
