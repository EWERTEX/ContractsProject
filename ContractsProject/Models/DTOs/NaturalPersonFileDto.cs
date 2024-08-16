namespace ContractsProject.Models.DTOs;

////Data Transfer Object для получения результатов по задаче 5
public class NaturalPersonFileDto
{
    public string FullName { get; set; } = string.Empty; //ФИО
    public string Email { get; set; } = string.Empty; //E-mail
    public string PhoneNumber { get; set; } = string.Empty; //Телефон
    public DateOnly Birthdate { get; set; } //Дата рождения
}