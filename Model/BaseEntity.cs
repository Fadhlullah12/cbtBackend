namespace cbtBackend.Model
{
    public class BaseEntity
    {
        public string Id { get; set; } = new Guid().ToString();
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; } = new DateTime().Date;
    }
}