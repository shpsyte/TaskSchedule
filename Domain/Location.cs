using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskSchedule.Domain {
  public class Location {
    public int Id { get; set; }

    [Required]
    [DataType (DataType.Text)]
    [Display (Name = "Instituição")]
    public string FundationName { get; set; }

    [Required]
    [DataType (DataType.Text)]
    [Display (Name = "Endereço")]
    public string Address { get; set; }

    [Required]
    [DataType (DataType.Text)]
    [Display (Name = "Número")]
    public string Number { get; set; }

    [Required]
    [DataType (DataType.Text)]
    [Display (Name = "Bairro")]
    public string Neighborhood { get; set; }

    [Required]
    [DataType (DataType.Text)]
    [Display (Name = "CEP")]
    public string PostalCode { get; set; }

    [Required]
    [DataType (DataType.Text)]
    [Display (Name = "Telefone")]
    public string Phone { get; set; }

    [Required]
    [DataType (DataType.Text)]
    [Display (Name = "Responsável")]
    public string Responsible { get; set; }

    public virtual List<TaskUser> TaskUser { get; set; }

    public string FullAddress () => this.PostalCode + " " + this.Address + ", " + this.Number + " " + this.Neighborhood;

  }
}
