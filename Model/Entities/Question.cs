namespace cbtBackend.Model.Entities
{
    public class Question : BaseEntity
    {
        public string Text { get; set; } = default!;
        public string SubjectId { get; set; } = default!;
        public Subject Subject { get; set; } = default!;
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
   }
}