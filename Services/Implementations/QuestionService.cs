using cbtBackend.Dtos.RequestModels.UpdateRequstModels;
using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Repositories.Interfaces;
using cbtBackend.Services.Interfaces;

namespace cbtBackend.Services.Implementations
{
    public class QuestionService : IQuestionService
    {
        IQuestionRepository _questionRepository;
        IExamRepository _examRepository;
        ISubjectRepository _subjectRepository;
        IAnswerRepository _answerRepository;
        public QuestionService(IQuestionRepository questionRepository, IExamRepository examRepository, ISubjectRepository subjectRepository,IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
            _subjectRepository = subjectRepository;
            _examRepository = examRepository;
            _questionRepository = questionRepository;
        }

        public async Task<BaseResponse<ExamResponse>> LoadExamQuestions(string examId)
        {
            var exam = await _examRepository.Get(examId);
            var questions = await _questionRepository.GetAll(exam.SubjectId, exam.MaxQuestion);
            return new BaseResponse<ExamResponse>
            {
                Status = true,
                Data = new ExamResponse
                {

                    DurationMinutes = exam.DurationMinutes,
                    SubjectName = exam.Subject.SubjectName,
                    Questions = questions.Select(a => new ExamQuestionsDto
                    {
                        QuestionId = a.Id,
                        Question = a.Text,
                        Options = a.Answers.Select(a => a.Label).ToList()
                    }).ToList()
                },
            };
        }

        public async Task<BaseResponse<ICollection<QuestionDto>>> GetSubjectQuestions(string subjectId)
        {
            var subject = await _subjectRepository.Get(subjectId);
            var subjectQuestions = subject.Questions.Select(s => new QuestionDto
            {
                Label = s.Text,
                Answers = s.Answers.Select(a => a.Label).ToList()
            }).ToList();
            return new BaseResponse<ICollection<QuestionDto>>
            {
                Data = subjectQuestions,
                Message = "Success",
                Status = true,
            };
        }

        public async Task<bool> UpdateQuestion(UpdateQuestionsRequestModel model)
        {
            var question = await _questionRepository.Get(model.QuestionId);
            question.Text = model.Label;
            _questionRepository.Update(question);
            await _questionRepository.Save();
            return true;
        }
        
         public async Task<bool> Delete(string questionId)
        {
            var question = await _questionRepository.Get(questionId);
            question.IsDeleted = true;
            foreach (var answer in question.Answers)
            {
                 answer.IsDeleted = true;
                _answerRepository.Update(answer);
            }
            _questionRepository.Update(question);
            await _questionRepository.Save();
            return true;
        }
    }
}