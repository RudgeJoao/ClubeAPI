namespace Api.Models
{
    public class Rent
    {
        public int Id { get; set; }
        public required Court Court { get; set; }
        public required Client Client { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
