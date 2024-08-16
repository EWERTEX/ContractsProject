using System.ComponentModel.DataAnnotations;

namespace ContractsProject.Models.Persons;

public class LegalPerson
{
    [Key] public int Id { get; set; } //Id как первичный ключ
    [MaxLength(150)] public string Name { get; set; } = string.Empty; //Наименование компании
    [MaxLength(10)] public string ITN { get; set; } = string.Empty; //ИНН (Individual Taxpayer Number)
    [MaxLength(13)] public string PSRN { get; set; } = string.Empty; //ОГРН (Primary State Registration Number)
    [MaxLength(150)] public string Country { get; set; } = string.Empty; //Страна
    [MaxLength(150)] public string Place { get; set; } = string.Empty; //Город
    [MaxLength(300)]public string Address { get; set; } = string.Empty; //Адрес
    [MaxLength(320)] public string Email { get; set; } = string.Empty; //E-mail
    [MaxLength(15)] public string PhoneNumber { get; set; } = string.Empty; //Телефон
}