namespace api.Models.Contacts;
public class Contact(string name, string email, string message)
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public string Message { get; private set; } = message;
    public DateTime DateSent { get; private set; } = DateTime.UtcNow;
}