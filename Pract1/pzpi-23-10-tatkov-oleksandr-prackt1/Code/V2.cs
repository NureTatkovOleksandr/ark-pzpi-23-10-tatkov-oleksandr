public class Person
{
private string _name;  // приватне поле
public string Name     // публічна властивість
{
get => _name;
set
{
if (string.IsNullOrEmpty(value))
throw new ArgumentException("Ім'я не може бути порожнім");
_name = value;
}
}
}
