namespace Api.Models
{
    public class Client
    { // id nome cpf telefone
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Cpf { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
