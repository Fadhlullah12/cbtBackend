using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Model;
using cbtBackend.Repositories.Interfaces;
using cbtBackend.Services.Interfaces;

namespace cbtBackend.Services.Implementations
{
    public class ResultService : BaseResponse<Result>, IResultService
    {

        IResultRepository _resultRepository;

        public ResultService(IResultRepository resultRepository)
        {

            _resultRepository = resultRepository;
        }

        public async Task<BaseResponse<ICollection<StudentResultsDto>>> GetSubjectResultAsync(string subjectId)
        {
            var results = await _resultRepository.GetAll(a => a.Subject.Id == subjectId);

            var listOfResults = results.Select(a => new StudentResultsDto
            {
                Score = a.Score,
                StudentName = $"{a.Student.User.FirstName} {a.Student.User.LastName}",
                DateCreated = a.DateCreated
            }).ToList();
            return new BaseResponse<ICollection<StudentResultsDto>>()
            {
                Message = "Success",
                Status = true,
                Data = listOfResults
            };
        }
        public async Task<BaseResponse<ICollection<StudentResultsDto>>> GetStudentResultAsync(string studentId)
        {
            var results = await _resultRepository.GetAll(a => a.StudentId == studentId);

            var listOfResults = results.Select(a => new StudentResultsDto
            {
                Score = a.Score,
                Questions = a.Exam.MaxQuestion,
                StudentName = $"{a.Student.User.FirstName} {a.Student.User.LastName}",
                SubjectName = a.Subject.SubjectName,
                DateCreated = a.DateCreated
            }).ToList();
            return new BaseResponse<ICollection<StudentResultsDto>>()
            {
                Message = "Success",
                Status = true,
                Data = listOfResults
            };
        }

         public async Task<BaseResponse<ICollection<ExamResultsDto>>> GetExamResultsAsync(string examId)
        {
            var results = await _resultRepository.GetAll(a => a.ExamId == examId);

            var listOfResults = results.Select(a => new ExamResultsDto
            {
                FullName = $"{a.Student.User.FirstName} {a.Student.User.LastName}",
                Score = a.Score,
                NoOFQuestions = a.Exam.MaxQuestion,
                StudentName = $"{a.Student.User.FirstName} {a.Student.User.LastName}",
            }).ToList();
            return new BaseResponse<ICollection<ExamResultsDto>>()
            {
                Message = "Success",
                Status = true,
                Data = listOfResults
            };
        }
    }
}
