namespace MonthlyClaimsApp.Models
{
    public class Lecturer
    {
        public int LecturerID { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }
}
