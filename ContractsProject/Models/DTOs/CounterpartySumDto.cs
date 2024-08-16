namespace ContractsProject.Models.DTOs;

//Data Transfer Object для получения результатов по задаче 2
public class CounterpartySumDto
{
    public string Name { get; set; } = string.Empty; //Название компании
    public decimal TotalSum { get; set; } //Общая сумма договоров с этой компанией
}