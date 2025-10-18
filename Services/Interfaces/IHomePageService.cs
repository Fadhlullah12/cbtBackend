using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Services.Interfaces
{
    public interface IHomePageService
    {
        Task<BaseResponse<HomePageDto>> SubAdminDahsboardData();
    }
}