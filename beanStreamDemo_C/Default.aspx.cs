using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Text;

namespace beanStreamDemo_C
{
    public partial class _Default : System.Web.UI.Page
    {
        private const string PaymentService = "https://www.beanstream.com/scripts/process_transaction.asp";
        //Replace the values with the ones from your account. NOTE: ALL PARAMETERS ARE CASE SENSITIVE
        private const string MerchantId = "300200075";
        private const string UserName = "stevenwu"; //Get from Administration -&gt; Account Settings -&gt; Order Setting
        private const string Password = "Victor12"; //Get from Administration -&gt; Account Settings -&gt; Order Setting
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["hf_isSubmit"]=="true")
            {
                var builder = new StringBuilder();

                //See documentation for required and optional fields
                builder.AppendFormat("merchant_id={0}&amp;", MerchantId);
                builder.AppendFormat("requestType={0}&amp;", "BACKEND");
                builder.AppendFormat("trnAmount={0}&amp;", "50.00");
                //builder.AppendFormat("trnCardOwner={0}&amp;", Request.Form["trnCardOwner"]);
                builder.AppendFormat("username={0}&amp;", UserName);
                builder.AppendFormat("password={0}&amp;", Password);
                //builder.AppendFormat("SecureXID={0}&amp;","12345678901234567890");
                //builder.AppendFormat("SecureECI={0}&amp;", "9");
                //builder.AppendFormat("SecireCAVV={0}&amp;", "aaaaaaaaaa");
                //builder.AppendFormat("trnOrderNumber={0}&amp;", Request.Form["trnOrderNumber"]);
                builder.AppendFormat("errorPage={0}&amp;", Server.UrlEncode("Error.aspx"));
                builder.AppendFormat("trnCardNumber={0}&amp;", "4012888888881881");
                builder.AppendFormat("trnExpMonth={0}&amp;", "09");
                builder.AppendFormat("trnExpYear={0}&amp;", "15");
                builder.AppendFormat("trnCardCvd={0}&amp;", "123");
                //builder.AppendFormat("singleUseToken={0};", Request.Form["singleUseToken"]);
                //...add as many items required per your requirements

                //Submit the request to Beanstream servers
                var responseString = SubmitPayment(PaymentService, builder.ToString());

                //Handle the response here
                Response.Write(responseString);
                Response.End();
            }
        }
        public string SubmitPayment(string serviceUrl, string data)
        {
            var httpWReq = (HttpWebRequest)WebRequest.Create(serviceUrl);
            var encoding = new ASCIIEncoding();
            var dataBytes = encoding.GetBytes(data);

            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/x-www-form-urlencoded";
            httpWReq.ContentLength = data.Length;

            using (var stream = httpWReq.GetRequestStream())
            {
                stream.Write(dataBytes, 0, data.Length);
            }

            var response = (HttpWebResponse)httpWReq.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
    }
}