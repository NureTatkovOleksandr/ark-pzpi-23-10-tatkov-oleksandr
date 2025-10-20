public interface IDateTimeProvider
{
    DateTime Now { get; }
}

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}

public class OrderService
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public OrderService(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    /// <summary>
    /// Повертає суму Total для замовлень, дата яких — у тому самому місяці та році, що й поточна дата.
    /// </summary>
    public decimal SumOrdersInCurrentMonth(IEnumerable<Order> orders)
    {
        if (orders is null) throw new ArgumentNullException(nameof(orders));

        var now = _dateTimeProvider.Now;
        int currentYear = now.Year;
        int currentMonth = now.Month;

        decimal sum = 0m;

        foreach (var order in orders)
        {
            // Опціонально: перевірка, чи order не null
            if (order != null && order.OrderDate.Year == currentYear && order.OrderDate.Month == currentMonth)
            {
                sum += order.Total;
            }
        }

        return sum;
    }
}
