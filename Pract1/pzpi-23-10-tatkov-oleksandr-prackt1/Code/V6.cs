try
{
    int result = 10 / divisor;
}
catch (DivideByZeroException ex)
{
    Console.WriteLine("Помилка: ділення на нуль.");
    throw; // повторно пробросити виняток після логування
}
