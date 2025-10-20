public class OrderService
{
    public decimal SumCurrentMonthOrders(List<Order> orders)
    {
        decimal sum = 0m;
        foreach (var o in orders)
        {
            if (o.OrderDate.Month == DateTime.Now.Month)
            {
                sum += o.Total;
            }
        }
        return sum;
    }
}
