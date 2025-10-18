using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Repositories.Interfaces;
using cbtBackend.Services.Interfaces;

namespace cbtBackend.Services.Implementations
{
    public class HomePageService : IHomePageService
    {
        IGetCurrentUser _getCurrentUser;
        ISubAdminRepository _subAdminRepository;
        public HomePageService(IGetCurrentUser getCurrentUser,ISubAdminRepository subAdminRepository)
        {
            _subAdminRepository = subAdminRepository;
            _getCurrentUser = getCurrentUser;
        }

        public async Task<BaseResponse<HomePageDto>> SubAdminDahsboardData()
        {
            var userId = _getCurrentUser.GetCurrentUserId();
            var subAdmin = await _subAdminRepository.Get(a => a.UserId == userId);
            int students = subAdmin.Students.Where(a => a.IsDeleted == false).ToList().Count;
            int subjectCount = subAdmin.Subjects.Where(a => a.IsDeleted == false).ToList().Count;
            int exams = subAdmin.Exams.Where(a => a.IsDeleted == false).ToList().Count;
            var subjectStudents = new Dictionary<string, int>();
            var subjects = subAdmin.Subjects.Where(a => a.IsDeleted == false);
            foreach (var item in subjects)
            {
                int noOfstudents = item.StudentSubjects.Where(a => a.IsDeleted == false).ToList().Count;
                subjectStudents.Add(item.SubjectName, noOfstudents);
            }
            return new BaseResponse<HomePageDto>
            {
                Data = new HomePageDto
                {
                    NoOfExamsAdministered = exams,
                    NoOfStudents = students,
                    NoOfSubjects = subjectCount,
                    SubjectStudents = subjectStudents,
                },
                Status = true,

            };

        }
    }
}