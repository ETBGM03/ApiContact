using ApiContact.Data;
using ApiContact.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var connectionString = builder.Configuration.GetConnectionString("ConnectionPostgresDb");
builder.Services.AddDbContext<ContactListDb>(options => options.UseNpgsql(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()) ;


//We get the contact list of the user
app.MapGet("/contact-list", async (ContactListDb db) => { 

    var contactList = await db.Contacts.ToListAsync();

    if (contactList.Count != 0) return Results.Ok(contactList);

    return Results.NotFound("Your list contacts is empty");

});

//We get the contact by ID
app.MapGet("/contact/{id:int}", async (int id, ContactListDb db) =>
{
    //add validation when the id does not positive

    var contactById = await db.Contacts.FindAsync(id);

    if (contactById == null) return Results.NotFound("Contact was not found with this Id");

    return Results.Ok(contactById);
});

//We create a contact
app.MapPost("/create-contact", async (ContactList newContact, ContactListDb db) => {

    db.Contacts.Add(newContact);
    await db.SaveChangesAsync();

    return Results.Created($"/contact/{newContact.Id}", newContact);
});

//We update the contact by id
app.MapPut("/update-contact/{id:int}", async (int id, ContactList currentContact, ContactListDb db) => {

    //update the message
    if (id != currentContact.Id) return Results.BadRequest("The id does not same");


    var contact = await db.Contacts.FindAsync(currentContact.Id);

    if (contact == null) return Results.NotFound("The contact with this Id was not found");

    contact.Name = currentContact.Name;
    contact.Phone = currentContact.Phone;
    contact.Comments = currentContact.Comments;
    //add the type contact


    await db.SaveChangesAsync();
    return Results.Ok(contact);
});

//We delete a contact by Id
app.MapDelete("/delete-contact/{id:int}", async (int id, ContactListDb db) => {
    var contact = await db.Contacts.FindAsync(id);

    if (contact == null) return Results.NotFound("The contact with this id, was not found. We cannot delete it.");

    db.Contacts.Remove(contact);
    await db.SaveChangesAsync();

    return Results.Ok("Contact delete it!");
});
































app.Run();
