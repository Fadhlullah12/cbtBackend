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
        public AnswerService(IAnswerRepository answerRepository,IQuestionRepository questionRepository)
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
    }
}