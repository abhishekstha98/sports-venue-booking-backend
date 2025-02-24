using System.Threading.Tasks;
using Domain.Entities;

public interface IHomeService
{
    Task<HomeResponse> GetHomeDataAsync(int userId);
}
