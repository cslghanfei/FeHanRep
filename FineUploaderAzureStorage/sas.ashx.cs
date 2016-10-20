using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FineUploaderTest
{
    /// <summary>
    /// Summary description for sas
    /// </summary>
    public class sas : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {


            var blobUri = context.Request.QueryString.Get("bloburi");

            var verb = context.Request.QueryString.Get("_method");


            var accountAndKey = new StorageCredentials("accountname", "accountkey");

            CloudBlockBlob blob = new CloudBlockBlob(new Uri(blobUri), accountAndKey);

            var permission = SharedAccessBlobPermissions.Write;


            var sas = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy()

            {

                Permissions = permission,

                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(15),

            });


            context.Response.ContentType = "text/plain";
            context.Response.Write(string.Format(CultureInfo.InvariantCulture, "{0}{1}", blob.Uri, sas));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
