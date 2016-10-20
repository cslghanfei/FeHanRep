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


            var accountAndKey = new StorageCredentials("fehanstoragetest", "d5qSSNsS1ic3RuLCJ52p8KMP+GyrtIF4S8TftjN51ndd7OuIhMWlaunvoBbNJ5/ISW3nD5lfagVOsVopqMm4lA==");

            CloudBlockBlob blob = new CloudBlockBlob(new Uri(blobUri), accountAndKey);

            var permission = SharedAccessBlobPermissions.Write;


            var sas = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy()

            {

                Permissions = permission,

                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(15),

            });


            //if (context.Request.HttpMethod == "GET")

            //{

            //    var blobUri = context.Request.QueryString.Get("bloburi");

            //    var verb = context.Request.QueryString.Get("_method");



            //    var sas = getSasForBlob(accountAndKey, blobUri, verb);



            //    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sas);

            //    //context.Response.con.ContentLength64 = buffer.Length;

            //    //System.IO.Stream output = response.OutputStream;

            //    //output.Write(buffer, 0, buffer.Length);

            //    //output.Close();

            //}

            //else if (context.Request.HttpMethod == "POST")

            //{

            //    context.Response.StatusCode = 200;

            //    // TODO insert uploadSuccess handling logic here

            //    context.Response.Close();

            //}

            //else

            //{

            //    context.Response.StatusCode = 405;

            //}

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