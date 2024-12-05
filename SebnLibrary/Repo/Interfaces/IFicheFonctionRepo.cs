using SebnLibrary.ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SebnLibrary.Repo.Interfaces
{
    public interface IFicheFonctionRepo
    {
        Task<List<FicheFonction>> GetAll();
        Task<FicheFonction> GetById(int id);
        Task<bool> AddFFAsync(FicheFonction ficheFonction);
        Task<bool> DeleteFFAsync(int id);
        Task<bool> UpdateFFAsync(int id, FicheFonction updatedFF);
    }
}
