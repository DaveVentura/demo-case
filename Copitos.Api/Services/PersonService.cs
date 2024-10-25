using System.Collections.Generic;
using System.Threading.Tasks;
using CopitosCase.Database;
using CopitosCase.Models;
using Microsoft.EntityFrameworkCore;

public class PersonService
{
    private readonly CopitosDbContext _context;

    public PersonService(CopitosDbContext context)
    {
        _context = context;
    }

    public async Task<Person> CreatePersonAsync(Person person)
    {
        _context.Persons.Add(person);
        await _context.SaveChangesAsync();
        return person;
    }

    public async Task<List<Person>> GetAllPersonsAsync()
    {
        return await _context.Persons.ToListAsync();
    }

    public async Task<Person> GetPersonByIdAsync(int id)
    {
        return await _context.Persons.FindAsync(id);
    }

    public async Task<bool> UpdatePersonAsync(Person person)
    {
        var existingPerson = await _context.Persons.FindAsync(person.Id);
        if (existingPerson == null) return false;

        existingPerson.Salutation = person.Salutation;
        existingPerson.Firstname = person.Firstname;
        existingPerson.Lastname = person.Lastname;
        existingPerson.Birthdate = person.Birthdate;
        existingPerson.Address = person.Address;
        existingPerson.Zipcode = person.Zipcode;
        existingPerson.City = person.City;
        existingPerson.Country = person.Country;

        await _context.SaveChangesAsync();
        return true;
    }

    // Delete - Entfernt eine Person aus der Datenbank
    public async Task<bool> DeletePersonAsync(int id)
    {
        var person = await _context.Persons.FindAsync(id);
        if (person == null) return false;

        _context.Persons.Remove(person);
        await _context.SaveChangesAsync();
        return true;
    }
}
