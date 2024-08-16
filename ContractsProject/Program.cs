using ContractsProject;
using ContractsProject.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using var db = new ApplicationContext();
Console.CursorVisible = false;
var isOpen = true;

while (isOpen)
{
    Console.WriteLine("\n1. Получить сумму всех договоров за текущий год" + 
                      "\n2. Получить сумму заключённых договоров по контрагентам из России" +
                      "\n3. Получить список e-mail уполномоченных лиц, с договорами не старше 30 дней и на сумму больше 40000" +
                      "\n4. Расторгнуть договоры для физических лиц старше 60 лет" +
                      "\n5. Создать отчёт по физическим лицам, у которых есть действующие договоры с компаниями из Москвы\n" +
                      "\nВыберите цифру нужного действия или нажмите ESC для закрытия приложения\n");
    
    var key = Console.ReadKey(true);

    switch (key.Key)
    {
        case ConsoleKey.NumPad1:
        case ConsoleKey.D1:
            Console.WriteLine($"Сумма договоров за {DateTime.Now.Year} год: {ExecuteTask1()}");
            break;
        case ConsoleKey.NumPad2:
        case ConsoleKey.D2:
            foreach (var item in ExecuteTask2())
            {
                Console.WriteLine($"Компания {item.Name}: {item.TotalSum}");
            }
            break;
        case ConsoleKey.NumPad3:
        case ConsoleKey.D3:
            foreach (var email in ExecuteTask3())
            {
                Console.WriteLine(email);
            }
            break;
        case ConsoleKey.NumPad4:
        case ConsoleKey.D4:
            Console.WriteLine($"Успешно выполнено. \nСтрок затронуто: {ExecuteTask4()}");
            break;
        case ConsoleKey.NumPad5:
        case ConsoleKey.D5: 
            Console.WriteLine($"Файл успешно сохранён по пути: {ExecuteTask5()}");
            break;
        case ConsoleKey.Escape: 
            Console.WriteLine("Выход...");
            isOpen = false; 
            break;
        default: 
            Console.WriteLine("Некорректный ввод, повторитe попытку");
            break;
    }
    
    Console.WriteLine("\nНажмите любую клавишу чтобы продолжить...");
    Console.ReadKey(true);
    Console.Clear();
}

//Метод для задачи 1
decimal ExecuteTask1()
{
    //Строка SQL-запроса для задачи 1
    const string sqlQuery = "SELECT SUM(ContractSum) FROM Contracts " +
                            "WHERE YEAR(DateOfSigning) = YEAR(GETDATE())";

    //Выполнение SQL-запроса и получение результата
    var totalSum = db.Database.SqlQueryRaw<decimal>(sqlQuery).AsEnumerable().FirstOrDefault();
    
    return totalSum;
}

//Метод для задачи 2
IEnumerable<CounterpartySumDto> ExecuteTask2()
{
    //Строка SQL-запроса для задачи 2
    const string sqlQuery = "SELECT Name, SUM(ContractSum) AS TotalSum FROM Contracts " +
                            "JOIN LegalPersons ON Contracts.CounterpartyId = LegalPersons.Id " +
                            "WHERE LegalPersons.Country = 'Россия' " +
                            "GROUP BY LegalPersons.Name";
    
    //Выполнение SQL-запроса и получение результата
    var result = db.Database.SqlQueryRaw<CounterpartySumDto>(sqlQuery).AsEnumerable();

    return result;
}

//Метод для задачи 3
List<string> ExecuteTask3()
{
    //Строка SQL-запроса для задачи 3
    const string sqlQuery = "SElECT Email FROM Contracts " +
                            "JOIN NaturalPersons ON Contracts.AuthorizedPersonId = NaturalPersons.Id " +
                            "WHERE DATEDIFF(day, Contracts.DateOfSigning, GETDATE()) <= 30 AND ContractSum > 40000";
    
    //Выполнение SQL-запроса и получение результата
    var emails = db.Database.SqlQueryRaw<string>(sqlQuery).ToList();

    return emails;
}

//Метод для задачи 4
int ExecuteTask4()
{
    //Строка SQL-запроса для задачи 4
    const string sqlQuery = "UPDATE Contracts SET Status = 0 " +
                            "WHERE Status = 1 AND " +
                            "DATEDIFF(year, (SELECT Birthdate FROM NaturalPersons WHERE Contracts.AuthorizedPersonId = NaturalPersons.Id), GETDATE()) > 60";
    
    //Версия SQL-запроса для БД, где используется возраст (нежелательно)
    //const string sqlQuery = "UPDATE Contracts SET Status = 0 " +
    //                        "WHERE Status = 1 AND " +
    //                        "(SELECT Age FROM NaturalPersons WHERE Contracts.AuthorizedPersonId = NaturalPersons.Id) > 60";
    
    //Выполнение SQL-запроса и получение результата
    var result = db.Database.ExecuteSqlRaw(sqlQuery);

    return result;
}

//Метод для задачи 5
string ExecuteTask5()
{
    //Путь сохранения файла
    const string path = @"d:\savedpersons.json";
    
    //Строка SQL-запроса для задачи 5
    const string sqlQuery = "SELECT FirstName + ' ' + LastName + ' ' + Patronymic AS FullName, NaturalPersons.Email, NaturalPersons.PhoneNumber, Birthdate FROM LegalPersons " +
                            "JOIN Contracts ON LegalPersons.Id = Contracts.CounterpartyId " +
                            "JOIN NaturalPersons ON Contracts.AuthorizedPersonId = NaturalPersons.Id " +
                            "WHERE Contracts.Status = 1 AND LegalPersons.Place = 'Москва'";
    
    //Выполнение SQL-запроса и получение результата
    var persons = db.Database.SqlQueryRaw<NaturalPersonFileDto>(sqlQuery).ToList();
    //Сериализация списка объектов в строку (использование фреймворка Newtonsoft.Json) и её запись в файл по указанному пути
    File.WriteAllText(path, JsonConvert.SerializeObject(persons));

    return path;
}
