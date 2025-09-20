using PurchaseService.Models;

namespace PurchaseService.Repository
{
    public class PurchaseRepository
    {
        private readonly PurchaseDBContext dbContext;

        public PurchaseRepository(PurchaseDBContext context)
        {
            dbContext = context;
        }

        public List<Purchase> GetAllPurchases()
        {
            List<Purchase> listOfPurchases = dbContext.Purchases.ToList();
            return listOfPurchases;
        }

        public bool AddNewPurchase(Purchase Purchase)
        {
            bool status;
            try
            {
                dbContext.Purchases.Add(Purchase);
                dbContext.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public int UpdatePurchaseDetails(Purchase Purchase)
        {
            int status = -1;
            Purchase purchaseObj = dbContext.Purchases.Find(Purchase.PurchaseId);
            try
            {
                if (purchaseObj != null)
                {
                    purchaseObj.EmailId = Purchase.EmailId;
                    purchaseObj.ProductId = Purchase.ProductId;
                    purchaseObj.QuantityPurchased = Purchase.QuantityPurchased;
                    purchaseObj.TotalPrice = Purchase.TotalPrice;
                    dbContext.Purchases.Update(purchaseObj);
                    dbContext.SaveChanges();
                    status = 1;
                }
            }
            catch (Exception)
            {
                status = -99;
            }
            return status;
        }

        public bool DeletePurchase(int purchaseId)
        {
            bool status = false;
            Purchase purchaseObj = dbContext.Purchases.Find(purchaseId);
            try
            {
                if (purchaseObj != null)
                {
                    dbContext.Purchases.Remove(purchaseObj);
                    dbContext.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
    }
}
