
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MvcApplication1.Controllers
{
    public class LoginController : Controller
    {

        JavaScriptSerializer serializer = new JavaScriptSerializer();

        public ActionResult Index()
        {
            Session["IdPerfil"] = "";
            return View();
        }


        [HttpPost]
        public ActionResult Login(string correo, string pwd)
        {

            //var baseAddress = @"http://localhost/inteek/Services/InteekConnect.svc/json/Login";

            var baseAddress = @"http://services.inteek.mx/connect/Services/InteekConnect.svc/json/Login";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            dynamic param = new { correo = correo, pwd = pwd };

            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(serializer.Serialize(param));

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            //return Content(content);
            

            /*
            var client = new RestClient();
            //client.EndPoint = string.Format(@"http://services.inteek.mx/connect/Services/InteekConnect.svc/json/Login/{0}/{1}", correo, pwd);
            client.EndPoint = @"http://localhost/inteek/Services/InteekConnect.svc/json/Login/";
            client.Method = HttpVerb.POST;
            client.PostData = "{ correo: '" + correo + "', pwd: '" + pwd + "'}";
            var json = client.MakeRequest();
            */

            dynamic stuff1 = JObject.Parse(content);

            if ((int)stuff1.LoginResult.List[0].IdPerfil > 0)
            {
                Session["IdPerfil"] = (int)stuff1.LoginResult.List[0].IdPerfil;
            }


            //dynamic result = JObject.Parse(json);
            //if (result.Lista.Count() > 0 && result.Lista[0].IdPerfil > 0)
            //{
                //return RedirectToAction("Inicio", "Perfil", new { area = "Admin" });
            //}
            //else
            //{
                return Content(content);
            //}


        }

	}
}