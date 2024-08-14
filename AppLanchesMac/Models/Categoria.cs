using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppLanchesMac.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }


        [StringLength(100, ErrorMessage = "The max size is 100 characters")]
        [Required(ErrorMessage = "enter the caregory name")]
        [Display(Name = "Nome")]
        public string CategoriaNome { get; set; }


        [StringLength(200, ErrorMessage = "The max size is 200 characters")]
        [Required(ErrorMessage = "enter the caregory description")]
        [Display(Name = "Descrição")]
        public string Descricao{ get; set; }
        
        
        
        public List<Lanche> Lanches { get; set; }
    }
}
