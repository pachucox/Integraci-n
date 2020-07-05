using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Email.Models;

namespace Email.Controllers
{
    
    public class StudentController : Controller
    {
        private IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "rxlenEV4Vs07GQFcjL9cJ5aNTR4uLprQ1DKdghaG",
            BasePath = "https://asp-mvc-51022.firebaseio.com/"
        };

        private IFirebaseClient client;
        // GET: Student
        public ActionResult Index()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Students");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Student>();

            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<Student>(((JProperty)item).Value.ToString()));
            }

            return View(list);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {

            var data = student;
            if (data.Nombre == null || data.Email ==null || data.Apellido == null || 
                data.Direccion ==null || data.Rut ==null || data.Telefono < 11111111 || data.Telefono > 999999999)
            {
                
                ModelState.AddModelError(string.Empty, "Es necesario llenar todos los campos correctamente!");
            }
            else
            {
                addStudentToFirebase(student);
                ModelState.AddModelError(string.Empty, "Registro completo!");
            }

            return View();

        }

        private void addStudentToFirebase(Student student)
        {
            client = new FireSharp.FirebaseClient(config);
            var data = student;
            PushResponse response = client.Push("Students/",data);
            data.student_id = response.Result.name;
            SetResponse setResponse = client.Set("Students/"+data.student_id,data);
        }


        [HttpGet]
        public ActionResult Detail(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Students/" + id);
            Student data = JsonConvert.DeserializeObject<Student>(response.Body);
            return View(data);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Students/" + id);
            Student data = JsonConvert.DeserializeObject<Student>(response.Body);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse response = client.Set("Students/" + student.student_id, student);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Delete("Students/" + id);

            return RedirectToAction("Index");
        }

    }
}