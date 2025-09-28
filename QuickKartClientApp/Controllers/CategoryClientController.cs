using Microsoft.AspNetCore.Mvc;
using QuickKartClientApp.Models;
using System.Text.Json;

namespace QuickKartClientApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryClientController : Controller
    {
        IHttpClientFactory httpClientFactory;
        JsonSerializerOptions jsonSerializerOptions;
        public CategoryClientController(IHttpClientFactory _httpClientFactory)
        {
            httpClientFactory = _httpClientFactory;
            jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
        }

        [HttpGet]
        public JsonResult FetchAllCategoryDetails()
        {
            HttpClient httpClient = httpClientFactory.CreateClient("apiGatewayServices");

            List<Category> listOfCategorys = new List<Category>();
            Task<HttpResponseMessage> httpResponseMessageTask = httpClient.GetAsync("/apiGateway/GatewayForCategory");
            httpResponseMessageTask.Wait();
            HttpResponseMessage httpResponseMessage = httpResponseMessageTask.Result;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                HttpContent httpContent = httpResponseMessage.Content;

                Task<string> httpContentTask = httpContent.ReadAsStringAsync();
                httpContentTask.Wait();
                string serializedData = httpContentTask.Result;

                listOfCategorys = JsonSerializer
                    .Deserialize<List<Category>>(serializedData, jsonSerializerOptions);
            }
            else
            {
                listOfCategorys = null;
            }

            return Json(listOfCategorys);
        }

        [HttpPost]
        public JsonResult AddNewCategoryDetails(Category category)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("apiGatewayServices");
            bool postResult = false;
            string message = string.Empty;
            Task<HttpResponseMessage> httpResponseMessageTask = httpClient.PostAsJsonAsync("/apiGateway/GatewayForCategory", category);
            httpResponseMessageTask.Wait();
            HttpResponseMessage httpResponseMessage = httpResponseMessageTask.Result;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                HttpContent httpContent = httpResponseMessage.Content;
                Task<string> httpContentTask = httpContent.ReadAsStringAsync();
                httpContentTask.Wait();
                string serializedData = httpContentTask.Result;
                postResult = JsonSerializer.Deserialize<bool>(serializedData, jsonSerializerOptions);
            }
            else
            {
                postResult = false;
            }

            if (postResult)
            {
                message = "New category details addition in the database was successful!";
            }
            else
            {
                message = "Unsuccessful addition operation!!";
            }
            return Json(message);
        }

        [HttpPut]
        public JsonResult UpdateCategoryDetails(Category category)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("apiGatewayServices");
            int status = -1;
            string message = string.Empty;
            Task<HttpResponseMessage> httpResponseMessageTask = httpClient.PutAsJsonAsync("/apiGateway/GatewayForCategory", category);
            httpResponseMessageTask.Wait();
            HttpResponseMessage httpResponseMessage = httpResponseMessageTask.Result;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                HttpContent httpContent = httpResponseMessage.Content;
                Task<string> httpContentTask = httpContent.ReadAsStringAsync();
                httpContentTask.Wait();
                string serializedData = httpContentTask.Result;
                status = JsonSerializer.Deserialize<int>(serializedData, jsonSerializerOptions);
            }
            else
            {
                status = -99;
            }

            if (status == 1)
            {
                message = "Category details update in the database was successful!";
            }
            else
            {
                message = "Unsuccessful update operation!!";
            }
            return Json(message);
        }

        [HttpDelete("{categoryId}")]
        public JsonResult DeleteCategoryDetails(byte categoryId)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("apiGatewayServices");
            bool deleteResult;
            Task<HttpResponseMessage> httpResponseMessageTask = httpClient.DeleteAsync("/apiGateway/GatewayForCategory/" + categoryId);
            httpResponseMessageTask.Wait();
            HttpResponseMessage httpResponseMessage = httpResponseMessageTask.Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                HttpContent httpContent = httpResponseMessage.Content;
                Task<string> httpContentTask = httpContent.ReadAsStringAsync();
                httpContentTask.Wait();
                string serializedData = httpContentTask.Result;
                deleteResult = JsonSerializer
                    .Deserialize<bool>(serializedData, jsonSerializerOptions);
            }
            else
            {
                deleteResult = false;
            }

            return Json(deleteResult);
        }

        [HttpGet("{categoryId}")]
        public JsonResult FetchAllCategoryDetails(byte categoryId)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("apiGatewayServices");
            Category category;

            Task<HttpResponseMessage> httpResponseMessageTask = httpClient.GetAsync("/apiGateway/GatewayForCategory/" + categoryId);
            httpResponseMessageTask.Wait();

            HttpResponseMessage httpResponseMessage = httpResponseMessageTask.Result;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                HttpContent httpContent = httpResponseMessage.Content;

                Task<string> httpContentTask = httpContent.ReadAsStringAsync();
                httpContentTask.Wait();
                string serializedData = httpContentTask.Result;

                category = JsonSerializer
                    .Deserialize<Category>(serializedData, jsonSerializerOptions);
            }
            else
            {
                category = null;
            }

            return Json(category);
        }



    }
}
