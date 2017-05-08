
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MvcApplication1.Controllers
{
    public class PerfilController : Controller
    {

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        int intIdPerfil = 0;

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Inicio()
        {
            try
            {
                if (Session["IdPerfil"].ToString() == "") { return RedirectToAction("Index", "Login"); }


                //return this.RedirectAndPost("Inicio", "Perfil", new { idPerfil = intIdPerfil }); 

                return RedirectToAction("Index", "Login"); 

            }catch(Exception ex){
                
                return RedirectToAction("Index", "Login"); 
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Inicio(int idPerfil)
        {
            try
            {
                if (Session["IdPerfil"].ToString() == "") { return RedirectToAction("Index", "Login"); }

                if (Session["IdPerfil"].ToString() != idPerfil.ToString()) { return RedirectToAction("Index", "Login"); }

                intIdPerfil = idPerfil;
                ViewBag.IdPerfil = idPerfil.ToString();
                return View();

             }catch(Exception ex){
                
                return RedirectToAction("Index", "Login"); 
            }
        }




        #region "PERFIL AVANCE"
        [HttpGet]
        public ActionResult ConsultarPerfilAvance(int idPerfil)
        {

            var baseAddress = string.Format(@"http://services.inteek.mx/connect/Services/InteekConnect.svc/json/ConsultarPerfilAvance/{0}", idPerfil);


            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "GET";

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            return Content(content);


            /*
            var client = new RestClient();
            client.EndPoint = string.Format(@"http://services.inteek.mx/connect/Services/InteekConnect.svc/json/ConsultarPerfilAvance/{0}", idPerfil);
            client.Method = HttpVerb.GET;
            //client.PostData = "{postData: value}";
            var json = client.MakeRequest();

            return Content(json);
            */
        }

        [HttpPost]
        public ActionResult EditarPerfilAvance(int idPerfil, string seccion, int porcentaje)
        {
            var baseAddress = "http://services.inteek.mx/connect/Services/InteekConnect.svc/json/EditarPerfilAvance";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            dynamic param = new { idPerfil = idPerfil, seccion = seccion, porcentaje = porcentaje };

            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(serializer.Serialize(param));

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            return Content(content);
        }
        #endregion

        #region "PERFIL"
        [HttpGet]
        public ActionResult ConsultarPerfil(int idPerfil)
        {

            var baseAddress = string.Format(@"http://services.inteek.mx/connect/Services/InteekConnect.svc/json/ConsultarPerfil/{0}", idPerfil);

           
            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "GET";
            

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            return Content(content);

            /*
            var client = new RestClient();
            client.EndPoint = string.Format(@"http://services.inteek.mx/connect/Services/InteekConnect.svc/json/ConsultarPerfil/{0}", idPerfil);
            client.Method = HttpVerb.GET;
            //client.PostData = "{postData: value}";
            var json = client.MakeRequest();

            return Content(json); 
            */
        }


        [HttpPost]
        public ActionResult EditarPerfilFoto(int idPerfil, string urlFoto)
        {
            var client = new RestClient();
            client.EndPoint = @"http://services.inteek.mx/connect/Services/InteekConnect.svc/json/EditarFotoPerfil";
            client.Method = HttpVerb.POST;
            client.PostData = "{ idPerfil: '" + idPerfil + "', urlFoto: '" + urlFoto + "'}";
            var json = client.MakeRequest();

            return Content(json);
        }


        [HttpPost]
        public ActionResult EditarDatosPersonales(int idPerfil, string nombre, string apPaterno, string apMaterno, char sexo, string fechaNacimiento)
        {
            var baseAddress = "http://services.inteek.mx/connect/Services/InteekConnect.svc/json/EditarDatosPersonales";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            dynamic param = new { idPerfil = idPerfil, nombre = nombre, apPaterno = apPaterno, apMaterno = apMaterno, sexo = sexo, fechaNacimiento = fechaNacimiento };
                       
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(serializer.Serialize(param));

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            return Content(content);
        }


        [HttpPost]
        public ActionResult EditarDatosContacto(int idPerfil, string telCasa, string celular, string correo, string facebook)
        {
            var baseAddress = "http://services.inteek.mx/connect/Services/InteekConnect.svc/json/EditarDatosContacto";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            dynamic param = new { idPerfil = idPerfil, telCasa = telCasa, celular = celular, correo = correo, facebook = facebook };

            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(serializer.Serialize(param));

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            return Content(content);
        }


        [HttpPost]
        public ActionResult EditarDatosUniversidad(int idPerfil, string universidad, string carrera, int semestre, string turno)
        {
            var baseAddress = "http://services.inteek.mx/connect/Services/InteekConnect.svc/json/EditarDatosUniversidad";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            dynamic param = new { idPerfil = idPerfil, universidad = universidad, carrera = carrera, semestre = semestre, turno = turno };

            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(serializer.Serialize(param));

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            return Content(content);
        }



        [HttpPost]
        public ActionResult EditarDatosTrabajo(int idPerfil, bool actualmenteTrabaja, string empresa, string puesto, string horario)
        {
            var baseAddress = "http://services.inteek.mx/connect/Services/InteekConnect.svc/json/EditarDatosTrabajo";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            dynamic param = new { idPerfil = idPerfil, actualmenteTrabaja = actualmenteTrabaja, empresa = empresa, puesto = puesto, horario = horario };

            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(serializer.Serialize(param));

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            return Content(content);
        }


        [HttpPost]
        public ActionResult EditarDatosInformacion(int idPerfil, string descProyecto, string interesCurso)
        {
            var baseAddress = "http://services.inteek.mx/connect/Services/InteekConnect.svc/json/EditarDatosInformacion";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            dynamic param = new { idPerfil = idPerfil, descProyecto = descProyecto, interesCurso = interesCurso };

            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(serializer.Serialize(param));

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            return Content(content);
        }
        #endregion


        #region "PERFIL CONOCIMIENTOS"
        [HttpGet]
        public ActionResult ConsultarPerfilConocimiento(int idPerfil)
        {

            var client = new RestClient();
            client.EndPoint = string.Format(@"http://services.inteek.mx/connect/Services/InteekConnect.svc/json/ConsultarPerfilConocimiento/{0}", idPerfil);
            //client.EndPoint = string.Format(@"http://localhost/inteek/Services/InteekConnect.svc/json/ConsultarPerfilConocimiento/{0}", idPerfil);
            client.Method = HttpVerb.GET;
            //client.PostData = "{postData: value}";
            var json = client.MakeRequest();

            return Content(json);
        }


        [HttpPost]
        public ActionResult EditarPerfilProgramacion(int idPerfil, string cadena)
        {
            var baseAddress = "http://services.inteek.mx/connect/Services/InteekConnect.svc/json/EditarPerfilProgramacion";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            dynamic param = new { idPerfil = idPerfil, cadena = cadena };

            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(serializer.Serialize(param));

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            return Content(content);
        }


        [HttpPost]
        public ActionResult EditarPerfilBase(int idPerfil, string cadena)
        {
            var baseAddress = "http://services.inteek.mx/connect/Services/InteekConnect.svc/json/EditarPerfilBase";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            dynamic param = new { idPerfil = idPerfil, cadena = cadena };

            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(serializer.Serialize(param));

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            return Content(content);
        }


        [HttpPost]
        public ActionResult EditarPerfilFramework(int idPerfil, string cadena)
        {
            var baseAddress = "http://services.inteek.mx/connect/Services/InteekConnect.svc/json/EditarPerfilFramework";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            dynamic param = new { idPerfil = idPerfil, cadena = cadena };

            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(serializer.Serialize(param));

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            return Content(content);
        }


        [HttpPost]
        public ActionResult EditarComentarioConocimiento(int idPerfil, string conocimiento)
        {
            var baseAddress = "http://services.inteek.mx/connect/Services/InteekConnect.svc/json/EditarComentarioConocimiento";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            dynamic param = new { idPerfil = idPerfil, conocimiento = conocimiento };

            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(serializer.Serialize(param));

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            return Content(content);
        }
        #endregion


        #region "PERFIL CURSOS"
        [HttpGet]
        public ActionResult ConsultarPerfilCursos(int idPerfil)
        {

            var client = new RestClient();
            client.EndPoint = string.Format(@"http://services.inteek.mx/connect/Services/InteekConnect.svc/json/ConsultarPerfilCurso/{0}", idPerfil);
            client.Method = HttpVerb.GET;
            //client.PostData = "{postData: value}";
            var json = client.MakeRequest();

            return Content(json);
        }
        
        [HttpPost]
        public ActionResult EditarPerfilCurso(int idPerfil, string titulo, string institucion, string duracion, string mesAnio, int idTipo, string comentarios, HttpPostedFileBase file)
        //public ActionResult EditarPerfilCurso()
        {

            string path = string.Empty, fileName = string.Empty, url = string.Empty;
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    DateTime dateNow = DateTime.Now;
                    string dayHrs = dateNow.Day.ToString() + dateNow.Month.ToString() + dateNow.Hour.ToString() + dateNow.Minute.ToString() + dateNow.Second.ToString() + dateNow.Millisecond.ToString();

                    fileName = idPerfil.ToString() + "_" + dayHrs + "_" + Path.GetFileName(file.FileName).Replace(" ", string.Empty);

                    path = Path.Combine(Server.MapPath("~/Data/Documentos"), fileName);
                    file.SaveAs(path);
                }
                url = Request.Url.ToString().Replace("Perfil/EditarPerfilCurso", "Data/Documentos/" + fileName);
            }



            var baseAddress = "http://services.inteek.mx/connect/Services/InteekConnect.svc/json/EditarPerfilCurso";
            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            dynamic param = new { idPerfil = idPerfil, titulo = titulo, institucion = institucion, duracion = duracion, mesAnio = mesAnio, urlDocumento = url, idTipo = idTipo, comentarios = comentarios };


            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(serializer.Serialize(param));

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            return Content(content);
        }

        [HttpPost]
        public ActionResult EliminarPerfilCurso(int idCurso)
        {
            var baseAddress = "http://services.inteek.mx/connect/Services/InteekConnect.svc/json/EliminarPerfilCurso";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            dynamic param = new { idCurso = idCurso };

            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(serializer.Serialize(param));

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            return Content(content);
        }

        #endregion


        #region "PERFIL COMPETENCIAS"
        [HttpGet]
        public ActionResult ConsultarPerfilCompetencia(int idPerfil)
        {

            var client = new RestClient();
            client.EndPoint = string.Format(@"http://services.inteek.mx/connect/Services/InteekConnect.svc/json/ConsultarPerfilCompetencia/{0}", idPerfil);
            client.Method = HttpVerb.GET;
            //client.PostData = "{postData: value}";
            var json = client.MakeRequest();

            return Content(json);
        }


        [HttpPost]
        public ActionResult EditarPerfilCompetencia(int idPerfil, string cadena)
        {
            var baseAddress = "http://services.inteek.mx/connect/Services/InteekConnect.svc/json/EditarPerfilCompetencia";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            dynamic param = new { idPerfil = idPerfil, cadena1 = cadena };

            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(serializer.Serialize(param));

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            return Content(content);
        }

        #endregion





    }
}