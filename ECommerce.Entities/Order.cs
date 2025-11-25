
namespace ECommerce.Entities;

public class Order
{
    public Order()
    {
        OrderItems=new List<OrderItem>();
    }
    public int Id { get; set; }
    public string  OrderNumber { get; set; }
    public string  OrderDate { get; set; }
    public string UserId { get; set; }
    public string  OrderState { get; set; }
    public EnumPaymentTypes PaymentTypes { get; set; }
    public List<OrderItem> OrderItems { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Phone { get; set; }
    public string  Email { get; set; }
    public string  OrderNote { get; set; }
    public bool  Status { get; set; }  //aktif mi / iptal mi

    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string ExpirationMonth { get; set; }
    public string ExpirationYear { get; set; }
    public string Cvv { get; set; }
    public int PaymentId { get; set; }
    public string  PaymentToken { get; set; }
    public string  ConversationId { get; set; }




}
public enum EnumPaymentTypes
{
    CreditCard=0,
    Eft=1,
    PayAtTheDoor=2
}
