using MyDotNETAPIUdemy.Model;

namespace MyDotNETAPIUdemy.Services.Implemantations;

public class PersonServiceImplementation : IPersonService
{
    public volatile int count;
    public Person Create(Person person)
    {

        return person;
    }

    public void Delete(long id)
    {
    }

    public List<Person> FindAll()
    {
        List<Person> persons = new List<Person>();
        for (int i = 0; i < 8; i++)
        {
            Person person = MockPerson(i);
            persons.Add(person);
        }
        return persons;
    }

   

    public Person FindById(long id)
    {
        return new Person { 
            Id = InrementeAndGet(),
            FirsName = "Leonildo",
            LastName = "Mazenda",
            Adress = "Angola - Luanda, Mutamba",
            Gender = "Male",        
        };
    }

    public Person Udate(Person person)
    {
        return person;
    }

    private Person MockPerson(int i)
    {
        return new Person
        {
            Id = InrementeAndGet(),
            FirsName = "Person Name" + i,
            LastName = "Person LastName" + i,
            Adress = "Some Adress" + i,
            Gender = "Male",
        };
    }

    private long InrementeAndGet()
    {
        return Interlocked.Increment(ref count);
    }
}
