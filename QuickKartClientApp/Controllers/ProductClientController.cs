using Microsoft.AspNetCore.Mvc;
using QuickKartClientApp.Models;
using System.Text.Json;

namespace QuickKartClientApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductClientController : Controller
    {
        IHttpClientFactory httpClientFactory;
        JsonSerializerOptions jsonSerializerOptions;
        public ProductClientController(IHttpClientFactory _httpClientFactory)
        {
            httpClientFactory = _httpClientFactory;

            jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
        }

        [HttpGet]
        public JsonResult FetchAllProductDetails()
        {
            HttpClient httpClient = httpClientFactory.CreateClient("apiGatewayServices");

            List<Product> listOfProducts = new List<Product>();
            Task<HttpResponseMessage> httpResponseMessageTask = httpClient.GetAsync("/apiGateway/GatewayForProduct");
            httpResponseMessageTask.Wait();
            HttpResponseMessage httpResponseMessage = httpResponseMessageTask.Result;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                HttpContent httpContent = httpResponseMessage.Content;

                Task<string> httpContentTask = httpContent.ReadAsStringAsync();
                httpContentTask.Wait();
                string serializedData = httpContentTask.Result;

                listOfProducts = JsonSerializer
                    .Deserialize<List<Product>>(serializedData, jsonSerializerOptions);
            }
            else
            {
                listOfProducts = null;
            }

            return Json(listOfProducts);
        }

        [HttpGet("{productId}")]
        public JsonResult FetchAllProductDetails(string productId)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("apiGatewayServices");
            Product product;

            Task<HttpResponseMessage> httpResponseMessageTask = httpClient.GetAsync("/apiGateway/GatewayForProduct/" + productId);
            httpResponseMessageTask.Wait();

            HttpResponseMessage httpResponseMessage = httpResponseMessageTask.Result;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                HttpContent httpContent = httpResponseMessage.Content;

                Task<string> httpContentTask = httpContent.ReadAsStringAsync();
                httpContentTask.Wait();
                string serializedData = httpContentTask.Result;

                product = JsonSerializer
                    .Deserialize<Product>(serializedData, jsonSerializerOptions);
            }
            else
            {
                product = null;
            }

            return Json(product);
        }

        [HttpPost]
        public JsonResult AddNewProductDetails(Product product)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("apiGatewayServices");
            bool postResult = false;
            string message = string.Empty;
            Task<HttpResponseMessage> httpResponseMessageTask = httpClient.PostAsJsonAsync("/apiGateway/GatewayForProduct", product);
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
                message = "New product details addition in the database was successful!";
            }
            else
            {
                message = "Unsuccessful addition operation!!";
            }
            return Json(message);

        }

        [HttpPut]
        public JsonResult UpdateProductDetails(Product product)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("apiGatewayServices");
            int status = -1;
            string message = string.Empty;
            Task<HttpResponseMessage> httpResponseMessageTask = httpClient.PutAsJsonAsync("/apiGateway/GatewayForProduct", product);
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
                message = "Product details update in the database was successful!";
            }
            else
            {
                message = "Unsuccessful update operation!!";
            }
            return Json(message);
        }

        [HttpDelete("{productId}")]
        public JsonResult DeleteProductDetails(string productId)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("apiGatewayServices");
            bool deleteResult;
            Task<HttpResponseMessage> httpResponseMessageTask = httpClient.DeleteAsync("/apiGateway/GatewayForProduct/" + productId);
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


    }
}

