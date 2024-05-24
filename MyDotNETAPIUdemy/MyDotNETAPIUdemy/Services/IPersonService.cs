using MyDotNETAPIUdemy.Model;

namespace MyDotNETAPIUdemy.Services;

public interface IPersonService
{
    List<Person> FindAll();
    Person FindById(long id);
    Person Create(Person person);
    Person Udate(Person person);
    void Delete(long id);
}
