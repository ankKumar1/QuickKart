using Microsoft.AspNetCore.Mvc;
using PurchaseService.Models;
using PurchaseService.Repository;

namespace PurchaseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseServiceController : Controller
    {
        private readonly PurchaseRepository repository;

        public PurchaseServiceController(PurchaseRepository _repository)
        {
            repository = _repository;
        }

        [HttpGet]
        public JsonResult GetAllPurchasesDetails()
        {
            List<Purchase> listOfPurchases = repository.GetAllPurchases();
            return Json(listOfPurchases);
        }

        [HttpGet("{id}")]
        public JsonResult GetPurchaseById(int id)
        {
            List<Purchase> listOfPurchases = repository.GetAllPurchases();
            Purchase purchase = listOfPurchases.Find(p => p.PurchaseId == id);
            return Json(purchase);
        }

        [HttpPost]
        public JsonResult AddNewPurchase(Purchase purchase)
        {
            return Json(repository.AddNewPurchase(purchase));
        }

        [HttpPut]
        public JsonResult UpdatePurchase(Purchase purchase)
        {
            int result = repository.UpdatePurchaseDetails(purchase);
            return Json(result);
        }

        [HttpDelete]
        public JsonResult DeletePurchase(int id)
        {
            bool result = repository.DeletePurchase(id);
            return Json(result);
        }
    }
}
