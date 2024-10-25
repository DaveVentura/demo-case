using CopitosCase.Contracts.Requests.CopitosCase.Models;
using CopitosCase.Database;
using Microsoft.EntityFrameworkCore;
using CopitosCase.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddDbContext<CopitosDbContext>(options =>
    {
        options.UseInMemoryDatabase("Copitos");
    }
);


builder.Services.AddScoped<PersonService>();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CopitosDbContext>();
    dbContext.Persons.Add(
        new Person
        {
            Id = 1,
            Salutation = "Herr",
            Firstname = "Max",
            Lastname = "Mustermann",
            Birthdate = new DateTime(1990, 1, 1),
            Address = "Musterstraße 1",
            Zipcode = "12345",
            City = "Musterstadt",
            Country = "DE"
        });
    dbContext.SaveChanges();
}

app.MapGet("/people", async (PersonService personService) =>
{
    var persons = await personService.GetAllPersonsAsync();
    var personResponse = new List<PersonDto>();
    foreach (var person in persons)
    {
        personResponse.Add(new PersonDto()
        {
            Salutation = person.Salutation,
            Firstname = person.Firstname,
            Lastname = person.Lastname,
            Address = person.Address,
            Birthdate = person.Birthdate,
            Zipcode = person.Zipcode,
            City = person.City,
            Country = person.Country
        });
    }
    return personResponse;
});

app.MapPost("/people", async (PersonService personService, PersonDto request) =>
{
    var person = new Person
    {
        Salutation = request.Salutation,
        Firstname = request.Firstname,
        Lastname = request.Lastname,
        Address = request.Address,
        Birthdate = request.Birthdate,
        Zipcode = request.Zipcode,
        City = request.City,
        Country = request.Country
    };
    var createdPerson = await personService.CreatePersonAsync(person);
    return Results.Created($"/people/{createdPerson.Id}", createdPerson);
});


app.MapControllers();

app.Run();
