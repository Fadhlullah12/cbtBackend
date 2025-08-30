namespace cbtBackend.Model.Entities
{
        public class Answer : BaseEntity
        {
            public string Label { get; set; } = default!;
            public bool IsCorrect { get; set; } = false;
            public string QuestionId { get; set; } = default!;
            public Question Question { get; set; } = default!;
        }
}