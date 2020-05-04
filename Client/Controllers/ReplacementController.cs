﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_IAP.Models;
using Project_IAP.ViewModels;

namespace Client.Controllers
{
    public class ReplacementController : Controller
    {
        private HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44379/api/")
        };
        public IActionResult Index()
        {
            return View(LoadReplacement());
        }
        public JsonResult LoadReplacement()
        {
            IEnumerable<ReplacementVM> replacement = null;
            var responseTask = client.GetAsync("Replacement/List");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<ReplacementVM>>();
                readTask.Wait();
                replacement = readTask.Result;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(replacement);
        }
        public JsonResult InsertOrUpdate(Replacement replacement)
        {
            var myContent = JsonConvert.SerializeObject(replacement);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (replacement.Id.Equals(0))
            {
                var result = client.PostAsync("Replacement/", byteContent).Result;
                return Json(result);
            }
            else
            {
                var result = client.PutAsync("Replacement/" + replacement.Id, byteContent).Result;
                return Json(result);
            }
        }
        public JsonResult GetById(int id)
        {
            Replacement replacement = null;
            var responseTask = client.GetAsync("Replacement/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                replacement = JsonConvert.DeserializeObject<Replacement>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "server error, try after some time");
            }
            return Json(replacement);
        }
        public JsonResult Delete(int id)
        {
            var result = client.DeleteAsync("Replacement/" + id).Result;
            return Json(result);
        }
        public JsonResult Confirm0(int id)
        {
            var result = client.DeleteAsync("Replacement/Confirm0/" + id).Result;
            return Json(result);
        }
        public JsonResult Confirm1(int id)
        {
            var result = client.DeleteAsync("Replacement/Confirm1/" + id).Result;
            return Json(result);
        }
    }
}