using System.Text.Json.Serialization;

namespace CRUDTest.DTO
{
    public class PessoaResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public int Idade { get; set; }

        [JsonIgnore]
        public int Id_Cidade { get; set; }
        public CidadeListResponseDto Cidade { get; set; }
    }
}
