namespace ShoppingSite.api.Data.DataModels.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Sub { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
