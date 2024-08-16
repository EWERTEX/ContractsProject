using System.ComponentModel.DataAnnotations;
using ContractsProject.Models.Enums;

namespace ContractsProject.Models.Persons;

public class NaturalPerson
{
    [Key] public int Id { get; set; } //Id как первичный ключ
    [MaxLength(150)] public string FirstName { get; set; } = string.Empty; //Имя
    [MaxLength(150)] public string LastName { get; set; } = string.Empty; //Фамилия
    [MaxLength(150)] public string Patronymic { get; set; } = string.Empty; //Отчество
    [MaxLength(150)] public string PlaceOfWork { get; set; } = string.Empty; //Место работы
    [MaxLength(150)] public string Country { get; set; } = string.Empty; //Страна
    [MaxLength(150)] public string Place { get; set; } = string.Empty; //Город (населённый пункт)
    [MaxLength(300)] public string Address { get; set; } = string.Empty; //Адрес
    [MaxLength(320)] public string Email { get; set; } = string.Empty; //E-mail
    [MaxLength(15)] public string PhoneNumber { get; set; } = string.Empty; //Телефон
    
    //Закомментировано, так как нежелательное использование регулярно изменяющегося свойства в БД
    //Возраст легко вычисляется с помощью даты рождения
    //public int Age {get; set;} 
    
    public DateOnly Birthdate { get; set; } //Дата рождения
    public Genders Gender { get; set; } //Пол
}