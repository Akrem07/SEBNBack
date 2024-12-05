using SEBNBack.Models;
using SEBNBack.Services.Interfaces;
using SebnLibrary.ModelEF;
using SebnLibrary.Repo.Classes;
using SebnLibrary.Repo.Interfaces;

namespace SEBNBack.Services.Classes
{
    public class RespEvaService : IRespEvaService
    {

        private readonly IRespEvaRepo _respEvaRepo;

        public RespEvaService(IRespEvaRepo respEvaRepo)
        {
            _respEvaRepo = respEvaRepo;
        }

        public async Task<bool> AddEvaAsync(RespEvaModel respEva)
        {
            try
            {
                bool res = false;

                RespEva evaadd = new RespEva();
                evaadd.IdReva = respEva.IdReva;
                evaadd.Aspects = respEva.Aspects;
                evaadd.Adaptation = respEva.Adaptation;
                evaadd.Autonomy = respEva.Autonomy;
                evaadd.Quality = respEva.Quality;
                evaadd.Goals = respEva.Goals;
                evaadd.Others = respEva.Others;
                evaadd.SpecifiqueCommentaire1 = respEva.SpecifiqueCommentaire1;
                evaadd.Evaluation = respEva.Evaluation;
                evaadd.Evaluation1 = respEva.Evaluation1;
                evaadd.ProbationReussie = respEva.ProbationReussie;
                evaadd.ProbationNonReussie = respEva.ProbationNonReussie;
                evaadd.Mat=respEva.Mat;

                

                res = await _respEvaRepo.AddEvaAsync(evaadd);


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

        public async Task<bool> DeleteEvaluationAsync(int id)
        {
            try
            {
                return await _respEvaRepo.DeleteEvaluationAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the Evaluation", ex);
            }
        }

        public async Task<ICollection<RespEvaModel>> GetAll()
        {
            try
            {
                var evas = await _respEvaRepo.GetEvaluationsAsync();
                var respEvaModels = evas.Select(respEva => new RespEvaModel
                {
                    IdReva = respEva.IdReva,
                    Aspects = respEva.Aspects,
                    Adaptation = respEva.Adaptation,
                    Autonomy = respEva.Autonomy,
                    Quality = respEva.Quality,
                    Goals = respEva.Goals,
                    Others = respEva.Others,
                    SpecifiqueCommentaire1 = respEva.SpecifiqueCommentaire1,
                    Evaluation = respEva.Evaluation,
                    Evaluation1 = respEva.Evaluation1,
                    ProbationReussie = respEva.ProbationReussie,
                    ProbationNonReussie = respEva.ProbationNonReussie,
                    Mat = respEva.Mat
                }).ToList();

                return respEvaModels;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all users", ex);
            }

        }



        public async Task<RespEvaModel> GetBymat(int mat)
        {
            try
            {
                var respEva = await _respEvaRepo.GetByMat(mat);
                if (respEva == null)
                {
                    return null;
                }

                return new RespEvaModel
                {
                    IdReva = respEva.IdReva,
                    Aspects = respEva.Aspects,
                    Adaptation = respEva.Adaptation,
                    Autonomy = respEva.Autonomy,
                    Quality = respEva.Quality,
                    Goals = respEva.Goals,
                    Others = respEva.Others,
                    SpecifiqueCommentaire1 = respEva.SpecifiqueCommentaire1,
                    Evaluation = respEva.Evaluation,
                    Evaluation1 = respEva.Evaluation1,
                    ProbationReussie = respEva.ProbationReussie,
                    ProbationNonReussie = respEva.ProbationNonReussie,
                    Mat = respEva.Mat
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the Responsable Evaluation by Mat", ex);
            }
        }

    }
}
