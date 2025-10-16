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

        public async Task<BaseResponse<ICollection<ResultDto>>> GetSubjectResultAsync(string subjectId)
        {
            var results = await _resultRepository.GetAll(a => a.Subject.Id == subjectId);

            var listOfResults = results.Select(a => new ResultDto
            {
                Score = a.Score,
                StudentName = $"{a.Student.User.FirstName} {a.Student.User.LastName}",
                DateCreated = a.DateCreated
            }).ToList();
            return new BaseResponse<ICollection<ResultDto>>()
            {
                Message = "Success",
                Status = true,
                Data = listOfResults
            };
        }
        public async Task<BaseResponse<ICollection<ResultDto>>> GetStudentResultAsync(string studentId)
        {
            var results = await _resultRepository.GetAll(a => a.StudentId == studentId);

            var listOfResults = results.Select(a => new ResultDto
            {
                Score = a.Score,
                StudentName = $"{a.Student.User.FirstName} {a.Student.User.LastName}",
                SubjectName = a.Subject.SubjectName,
                DateCreated = a.DateCreated
            }).ToList();
            return new BaseResponse<ICollection<ResultDto>>()
            {
                Message = "Success",
                Status = true,
                Data = listOfResults
            };
        }
    }
}