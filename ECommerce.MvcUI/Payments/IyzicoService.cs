using ECommerce.MvcUI.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using System.Globalization;


namespace ECommerce.MvcUI.Payments
{
    public class IyzicoService
    {
        private readonly Options _option;

        public IyzicoService(IConfiguration configuration , bool isSandbox =true)
        {
            var apiKey = configuration["Payment:Iyzico:ApiKey"];
            var secretKey = configuration["Payment:Iyzico:SecretKey"];
            var baseUrl = configuration["Payment:Iyzico:BaseUrl"] ??
                          (isSandbox ? "https://sandbox-api.iyzipay.com" : "https://api.iyzipay.com");
            _option =new Options
            {
                ApiKey = apiKey,
                SecretKey = secretKey,
                BaseUrl = baseUrl

            };

        }


        public async Task<string> InitializeCheckOutForm(CartModel cartModel,string email,string callbackUrl)
        {

            var totalPrice = cartModel.TotalPrice();
            var request = new CreateCheckoutFormInitializeRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = Guid.NewGuid().ToString(),
                Price = totalPrice.ToString("0.00", CultureInfo.InvariantCulture),
                PaidPrice = totalPrice.ToString("0.00",CultureInfo.InvariantCulture),

                Currency = Currency.TRY.ToString(),
                BasketId = Guid.NewGuid().ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                CallbackUrl = callbackUrl,
                EnabledInstallments = new List<int> { 2, 3, 6, 9 },
            };
            request.Buyer = new Buyer
            {
                Id = "BY789",
                Name = "John",
                Surname = "Doe",
                GsmNumber = "+905350000000",
                Email = email,
                IdentityNumber = "74300864791",
                RegistrationAddress = "İstanbul, Türkiye",
                Ip = "85.34.78.112",
                City = "Istanbul",
                Country = "Turkey"
            };
            request.ShippingAddress = new Address
            {
                ContactName = "Jane Doe",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Altunizade Mah. Üsküdar",
                ZipCode = "34742"
            };
            request.BillingAddress = request.ShippingAddress;
            var basketItems = new List<BasketItem>();
            foreach (var item in cartModel.CartItems)
            {
                 var basketItem = new BasketItem
                {
                    Id = item.ProductId.ToString(),
                    Name = item.Name,
                    Category1 = "General",
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = (item.Quantity*item.DiscountedPrice).ToString("0.00",CultureInfo.InvariantCulture)
                };
                basketItems.Add(basketItem);
            }
            request.BasketItems = basketItems;

            var checkoutFormInitialize = await CheckoutFormInitialize.Create(request, _option);
             var err = checkoutFormInitialize.ErrorMessage;
              return checkoutFormInitialize.CheckoutFormContent;
        }

    }
}
