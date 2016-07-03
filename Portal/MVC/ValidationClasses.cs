using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC
{
    public class ValidateFileAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            bool isValid = false;
            var file = value as HttpPostedFileBase;

            if (file == null || file.ContentLength > 3 * 1024 * 1024 || !file.ContentType.Contains("image"))
            {
                return isValid;
            }

            var img = Image.FromStream(file.InputStream);
            var allowedFormats = new List<ImageFormat>()
            {
                ImageFormat.Png,
                ImageFormat.Jpeg,
                ImageFormat.Gif,
            };

            if (allowedFormats.Contains(img.RawFormat))
            {
                isValid = true;
            }

            return isValid;
        }
    }
}