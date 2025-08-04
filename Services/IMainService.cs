using ViltrapportenApi.Data.CustomModels;

namespace ViltrapportenApi.Services
{
    public interface IMainService
    {
        Task<IList<UserUnit>> GetUserUnitAsync(int dnnUserId, int isAdmin);
        Task<IList<ChildUnit>> GetChildNodesAsync(int parentId, bool isUserOnlyOnMunicipality, bool isGuest);
    }
}
