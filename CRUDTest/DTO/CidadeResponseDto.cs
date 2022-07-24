namespace CRUDTest.DTO
{
    public class CidadeResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }

        public List<PessoaListResponseDto> Pessoas { get; set; }
    }
}
