using System.ComponentModel.DataAnnotations;

namespace AppCrud1.Models
{
    public class Cliente
    {

        [Display (Name = "Código")]
        public int? Id { get; set; }

        [Display (Name = "Nome do Cliente")]
        [Required (ErrorMessage = "O campo Nome é obrigatório")]
        public string NomeCli { get; set; }

        [Display (Name = "Data do Cadastro")]
        [DataType(DataType.Date)]
        public DateTime? DataCadastrada { get; set; }

        [Display (Name = "CEP")]
        [Required (ErrorMessage = "O campo CEP é obrigatório")]
        public int CEP { get; set; }


        [Display (Name = "Telefone")]
        [Required (ErrorMessage = "O campo Telefone é obrigatório")]
        public int Telefone { get; set; }
    }
}
