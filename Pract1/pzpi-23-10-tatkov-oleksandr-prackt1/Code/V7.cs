public class UserCreator   // відповідає за створення користувачів
{
    public User CreateUser(string name) { /* ... */ }
}

public class UserRepository  // відповідає за збереження користувача в БД
{
    public void SaveUser(User user) { /* ... */ }
}
