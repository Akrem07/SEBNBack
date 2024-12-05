using SEBNBack.Models;

namespace SEBNBack.Services.Interfaces
{
    public interface IFicheFonctionService
    {
        Task<ICollection<FicheFonctionModel>> GetAll();
        Task<FicheFonctionModel> GetById(int id);
        Task<bool> AddFFAsync(FicheFonctionModel ficheFonction);
        Task<bool> DeleteFFAsync(int id);
        Task<bool> UpdateFFAsync(UpdateFF ficheFonction);
    }
}
