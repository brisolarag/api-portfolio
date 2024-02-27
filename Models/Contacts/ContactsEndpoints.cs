using api.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Models.Contacts;
public static class ContactsEndpoints
{
    private static bool ReqDoesntHaveEmpty(AddContactMessage req)
    {
        if (req.Name == "" || req.Name == null)
        {
            return false;
        }
        if (req.Email == "" || req.Email == null)
        {
            return false;
        }
        if (req.Message == "" || req.Message == null)
        {
            return false;
        }

        return true;
    }
    public static void AddContactEndpoints(this WebApplication app)
    {
        // hello world:
        app.MapGet("hello-world", () => "Hello World");

        // Group: Contact => /contact
        var contactRoutes = app.MapGroup("contact");
        
        // Get : Contact => /contact
        contactRoutes.MapGet("", async (AppDbContext context, CancellationToken ct) =>
        {
            var allMessages = await context.Mensagens.ToListAsync(ct);
            return Results.Ok(allMessages);
        });
        
        // Post : Contact => /contact
        // "Name": "",
        // "Email": "",
        // "Message": ""
        contactRoutes.MapPost("", async (AddContactMessage req, AppDbContext context, CancellationToken ct) =>
        {
            if (!ReqDoesntHaveEmpty(req))
            {
                return Results.BadRequest(new {error= true, msg="Error in the body requisition (reject empty spaces)"});
            }

            if (!(req.Email.Contains('@')))
            {
                return Results.BadRequest(new { err = true, msg = "Must be a valid email" });
            }
            var newMessage = new Contact(req.Name, req.Email, req.Message);
            await context.AddAsync(newMessage, ct);
            await context.SaveChangesAsync(ct);
            return Results.Ok(new {err=false,idAdded = newMessage.Id});
        });
        
        // Delete: Contact => /contact/{id}
        contactRoutes.MapDelete("{id:guid}", async (Guid id, AppDbContext context, CancellationToken ct) =>
        {
            var contactToDelete = await context.Mensagens.SingleOrDefaultAsync(msg => msg.Id == id, ct);
            if (contactToDelete == null)
            {
                return Results.NotFound();
            }
            context.Mensagens.Remove(contactToDelete);
            await context.SaveChangesAsync(ct);
            return Results.Ok(new {err=false, idRemoved=contactToDelete.Id});
        });
    }
}