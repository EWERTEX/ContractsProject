using ContractsProject.Models;
using ContractsProject.Models.Persons;
using Microsoft.EntityFrameworkCore;

namespace ContractsProject;

//Контекст приложения
public class ApplicationContext: DbContext
{
    //Физические лица
    public DbSet<NaturalPerson> NaturalPersons { get; set; }
    
    //Юридические лица
    public DbSet<LegalPerson> LegalPersons { get; set; }
    
    //Договоры
    public DbSet<Contract> Contracts { get; set; }
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Задаётся строка подключения
        optionsBuilder.UseSqlServer(@"Server=localhost;Database=contractsprojectdb;Trusted_Connection=Yes;Integrated Security=true;encrypt=false;");
    }
}