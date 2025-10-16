using cbtBackend.Dtos.RequestModels.UpdateRequstModels;
using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Model.Entities;
using cbtBackend.Repositories.Interfaces;
using cbtBackend.Services.Interfaces;

namespace cbtBackend.Services.Implementations
{
    public class AnswerService : BaseResponse<Answer>, IAnswerService
    {
        IAnswerRepository _answerRepository;
        IQuestionRepository _questionRepository;
        public AnswerService(IAnswerRepository answerRepository, IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }

        public async Task<bool> Delete(string answerId)
        {
            var answer = await _answerRepository.Get(answerId);
            if (answer == null)
            {
                return false;
            }
            answer.IsDeleted = true;
            await _answerRepository.Save();
            return true;
        }

        public async Task<bool> Update(UpdateAnswerRequestModel model)
        {
            var answer = await _answerRepository.Get(model.AnswerId);
            var question = await _questionRepository.Get(answer.QuestionId);
            if (model.IsCorrect == true)
            {
                var correctAnswer = question.Answers.FirstOrDefault(a => a.IsCorrect == true);
                if (correctAnswer == null)
                {
                    return false;
                }
                correctAnswer.IsCorrect = false;
                answer.IsCorrect = model.IsCorrect;
                _answerRepository.Update(answer);
                _answerRepository.Update(correctAnswer);
            }
            answer.Label = model.Label;
            await _answerRepository.Save();
            return true;
        }

        public async Task<BaseResponse<ICollection<AnswerDto>>> GetQuestionAnswers(string questionId)
        {
            var answerObjects = await _answerRepository.GetAll(a => a.QuestionId == questionId && a.IsDeleted == false);
            if (answerObjects == null)
            {
                return new BaseResponse<ICollection<AnswerDto>>
                {
                    Message = "Subject not found.",
                    Status = false
                };
            }
            var answers = answerObjects.Select(a => new AnswerDto
            {
                Id = a.Id,
                Label = a.Label,
                IsCorrect = a.IsCorrect
            }).ToList();
            return new BaseResponse<ICollection<AnswerDto>>
            {
                Data = answers,
                Status = true,
            };


        }
    }
}