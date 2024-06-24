using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testandoBancodDo0.Models
{
    [Table("Receita")]
    public class ReceitaModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }

        public string Titulo { get; set; }

         public string Descricao {  get; set; }

         public string Ingredientes { get; set; }

    
        public string ?IdUsuario {  get; set; }

        [ForeignKey(nameof(IdUsuario))]
        public virtual UsuarioModel ?Usuario { get; set; }

        public byte[]? imagem { get; set; }
        public string Dificuldade { get; set; }
        public string Custo { get; set; }
        public int TempoPreparo { get; set; }
        public string UnidadeTempo { get; set; }

    }
}
