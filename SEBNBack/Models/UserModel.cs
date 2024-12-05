namespace SEBNBack.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public int? Mat { get; set; }        
        public string? FirstName { get; set; }       
        public string? LastName { get; set; }       
        public string? Password { get; set; }
        public int IdDep { get; set; }
        public int IdR { get; set; }
        public int? IdFf { get; set; }
        public int? IdIp { get; set; }

        public string? Token { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }
        public int? MatResp { get; set; }

    }
}
