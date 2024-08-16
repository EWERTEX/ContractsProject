using System.ComponentModel.DataAnnotations;
using ContractsProject.Models.Enums;
using ContractsProject.Models.Persons;

namespace ContractsProject.Models;

public class Contract
{
    [Key] public int Id { get; set; } //Id как первичный ключ
    public LegalPerson Counterparty { get; set; } = new(); //Контрагент
    public NaturalPerson AuthorizedPerson { get; set; } = new(); //Уполномоченное лицо
    public decimal ContractSum { get; set; } //Сумма договора
    public ContractStatus Status { get; set; } //Статус
    public DateOnly DateOfSigning { get; set; } //Дата подписания
}