namespace Dashboard.Models
{
    public class DashboardUser
    {
        public string Email { get; set; }
        public string[] Roles { get; set; }
        public int[] BranchIds { get; set; }
    }
}
