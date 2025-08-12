namespace cbtBackend.Model
{
    public class BaseEntity
    {
        public string Id { get; set; } = default!;
        public bool IsDeleted { get; set; }
    }
}