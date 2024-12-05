using SEBNBack.Models;
using SEBNBack.Services.Interfaces;
using SebnLibrary.ModelEF;
using SebnLibrary.Repo.Interfaces;

namespace SEBNBack.Services.Classes
{
    public class FicheFonctionService: IFicheFonctionService
    {
        private readonly IFicheFonctionRepo _ficheFonctionRepository;

        public FicheFonctionService(IFicheFonctionRepo ficheFonctionRepository)
        {
            _ficheFonctionRepository = ficheFonctionRepository;
        }

        public async Task<bool> AddFFAsync(FicheFonctionModel ficheFonction)
        {
            try
            {
                bool res = false;

                FicheFonction ffadd = new FicheFonction();

                ffadd.IdFf = ficheFonction.IdFf;
                ffadd.NameFf = ficheFonction.NameFf;
               
                res = await _ficheFonctionRepository.AddFFAsync(ffadd);

                if (res)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteFFAsync(int id)
        {
            try
            {
                return await _ficheFonctionRepository.DeleteFFAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the fiche fonction", ex);
            }
        }

        public async Task<ICollection<FicheFonctionModel>> GetAll()
        {
            
            try
            {
                var ffs = await _ficheFonctionRepository.GetAll();
                var ffModels = ffs.Select(ff => new FicheFonctionModel
                {
                    IdFf = ff.IdFf,
                    NameFf = ff.NameFf,
                    FileData = ff.FileData,
                    ContentType = ff.ContentType,
                    Mresp = ff.Mresp,
                }).ToList();

                return ffModels;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all fiches fonction", ex);
            }
        }

        public async Task<FicheFonctionModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateFFAsync(UpdateFF ficheFonction)
        {
            try
            {
                // Create a new User entity object
                FicheFonction updatedff = new FicheFonction
                {
                    IdFf = ficheFonction.IdFf,
                    Mresp=ficheFonction.Mresp
                };

                // Call the UserRepo method to update the user
                return await _ficheFonctionRepository.UpdateFFAsync(ficheFonction.IdFf, updatedff);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while editing the fiches fonction", ex);
            }
        }
    }
}
