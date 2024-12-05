namespace SEBNBack.Models
{
    public class AddUserModel
    {
        public int Id { get; set; }
        public int Mat { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public int IdDep { get; set; }
        public int IdR { get; set; }
    }
}
