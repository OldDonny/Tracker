namespace AppTracker.ServiceLayer.DTOS
{
    public class UserAppRowDTO
    {
        public int Id { get; set; }
        public int AppId { get; set; }
        public string AppName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }

    }
}
