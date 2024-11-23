using YemekGuru.entity;
using YemekGuru.webapp.Models;

namespace YemekGuru.webapp.Services.IyzicoServices;

public interface IIyzicoService
{
    void Pay(Order order, PaymentModel paymentModel);
}