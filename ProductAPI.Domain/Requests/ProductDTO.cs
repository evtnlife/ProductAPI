using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Domain.Requests
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do produto não pode exceder 100 caracteres.")]
        [MinLength(4, ErrorMessage = "O nome do produto deve ter mais de 3 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O preço de custo é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço de custo deve ser maior que zero.")]
        public decimal CostPrice { get; set; }

        [Required(ErrorMessage = "O preço de venda é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço de venda deve ser maior que zero.")]
        public decimal SellPrice { get; set; }
    }
}
