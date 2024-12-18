using System.ComponentModel.DataAnnotations;

namespace CadastroDeProcessos.Domain.Entities;

public class Processo
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do processo é obrigatório.")]
    public string NomeProcesso { get; set; }

    [Required(ErrorMessage = "O NPU é obrigatório.")]
    [RegularExpression(@"\d{7}-\d{2}\.\d{4}\.\d{1}\.\d{2}\.\d{4}", ErrorMessage = "Formato de NPU inválido.")]
    public string NPU { get; set; }

    public DateTime DataCadastro { get; set; }
    public DateTime? DataVisualizacao { get; set; }

    [Required(ErrorMessage = "A UF é obrigatória.")]
    public string UF { get; set; }

    [Required(ErrorMessage = "O município é obrigatório.")]
    public string Municipio { get; set; }

    public string CodigoMunicipio { get; set; }
}
