namespace api.Models.Contacts;

public record ContactDto(Guid Id, string Name, string Email, string Message, DateTime d);