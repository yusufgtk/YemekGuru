using System.Globalization;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using YemekGuru.entity;
using YemekGuru.webapp.Models;
using Address = Iyzipay.Model.Address;
using Payment = Iyzipay.Model.Payment;

namespace YemekGuru.webapp.Services.IyzicoServices;

public class IyzicoService : IIyzicoService
{
    public void Pay(Order order, PaymentModel paymentModel)
    {
        Options options = new Options();
        options.ApiKey = "sandbox-bpFwedsIuyLkDe5JkeULkdYcaWkIi5DL";
        options.SecretKey = "sandbox-p12FNDtrvVmkT8Z9eRgDCW5Xzg8qN4ik";
        options.BaseUrl = "https://sandbox-api.iyzipay.com";
                
        CreatePaymentRequest request = new CreatePaymentRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = "123456789";
        request.Price = String.Format(new CultureInfo("tr-TR"), "{0:N2}", order.TotalPrice).Replace(",", ".");
        request.PaidPrice = String.Format(new CultureInfo("tr-TR"), "{0:N2}", order.TotalPrice).Replace(",", ".");
        request.Currency = Currency.TRY.ToString();
        request.Installment = 1;
        request.BasketId = "B67832";
        request.PaymentChannel = PaymentChannel.WEB.ToString();
        request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

        PaymentCard paymentCard = new PaymentCard();
        paymentCard.CardHolderName = paymentModel.CardName; //John Doe
        paymentCard.CardNumber = paymentModel.CardNumber; //5528790000000008
        paymentCard.ExpireMonth = paymentModel.Month.ToString();
        paymentCard.ExpireYear = paymentModel.Year.ToString();
        paymentCard.Cvc = paymentModel.CVV.ToString();
        paymentCard.RegisterCard = 0;
        request.PaymentCard = paymentCard;

        Buyer buyer = new Buyer();
        buyer.Id = "BY789";
        buyer.Name = "John";
        buyer.Surname = "Doe";
        buyer.GsmNumber = "+905350000000";
        buyer.Email = "email@email.com";
        buyer.IdentityNumber = "74300864791";
        buyer.LastLoginDate = "2015-10-05 12:43:35";
        buyer.RegistrationDate = "2013-04-21 15:12:09";
        buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        buyer.Ip = "85.34.78.112";
        buyer.City = "Istanbul";
        buyer.Country = "Turkey";
        buyer.ZipCode = "34732";
        request.Buyer = buyer;

        Address shippingAddress = new Address();
        shippingAddress.ContactName = "Jane Doe";
        shippingAddress.City = "Istanbul";
        shippingAddress.Country = "Turkey";
        shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        shippingAddress.ZipCode = "34742";
        request.ShippingAddress = shippingAddress;

        Address billingAddress = new Address();
        billingAddress.ContactName = "Jane Doe";
        billingAddress.City = "Istanbul";
        billingAddress.Country = "Turkey";
        billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        billingAddress.ZipCode = "34742";
        request.BillingAddress = billingAddress;

        List<BasketItem> basketItems = new List<BasketItem>();
        if(order.OrderItems!=null)
        {
            foreach(var orderItem in order.OrderItems)
            {
                BasketItem firstBasketItem = new BasketItem();
                firstBasketItem.Id = orderItem.Id.ToString();
                firstBasketItem.Name = orderItem.Product?.Name ?? "Yemek";
                firstBasketItem.Category1 = "Boş";
                firstBasketItem.Category2 = "Boş";
                firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                firstBasketItem.Price = String.Format(new CultureInfo("tr-TR"), "{0:N2}", orderItem.TotalPrice*orderItem.Amount).Replace(",", ".");
                basketItems.Add(firstBasketItem);
            }
        }   
        
        request.BasketItems = basketItems;

        Payment payment = Payment.Create(request, options);
    }
}
