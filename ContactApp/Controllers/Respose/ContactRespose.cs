namespace ContactApp.Controllers.Respose
{
    public class ContactRespose
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public int Age { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
}
