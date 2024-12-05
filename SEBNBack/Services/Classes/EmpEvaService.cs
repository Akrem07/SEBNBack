using SEBNBack.Models;
using SEBNBack.Services.Interfaces;
using SebnLibrary.Abstract;
using SebnLibrary.ModelEF;
using SebnLibrary.Repo.Interfaces;
using System.Data;

public class EmpEvaService : IEmpEvaService
{
    private readonly IEmpEvaRepo _empEvaRepo;

    public EmpEvaService(IEmpEvaRepo empEvaRepo)
    {
        _empEvaRepo = empEvaRepo;
    }

    public async Task<bool> AddEvaAsync(EmpEvaModel empEva)
    {
        try
        {
            bool res = false;

            EmpEva evaadd = new EmpEva();
            evaadd.IdEva = empEva.IdEva;
            evaadd.AccueilQualite = empEva.AccueilQualite;
            evaadd.LivretInfos = empEva.LivretInfos; 
            evaadd.ServiceAccueil = empEva.ServiceAccueil;
            evaadd.AccueilCommentaires = empEva.AccueilCommentaires;
            evaadd.GeneriqueFormationQualite = empEva.GeneriqueFormationQualite;
            evaadd.GeneriqueEncadrement = empEva.GeneriqueEncadrement;
            evaadd.GeneriqueConditions = empEva.GeneriqueConditions;
            evaadd.GeneriqueCommentaires = empEva.GeneriqueCommentaires;
            evaadd.SpecifiqueFormationQualite = empEva.SpecifiqueFormationQualite;
            evaadd.SpecifiqueEncadrement = empEva.SpecifiqueEncadrement;
            evaadd.SpecifiqueConditions = empEva.SpecifiqueConditions;
            evaadd.ObjectifsClairs = empEva.ObjectifsClairs;
            evaadd.PerimetrePoste = empEva.PerimetrePoste;
            evaadd.SpecifiqueCommentaires = empEva.SpecifiqueCommentaires;
            evaadd.Mat = empEva.Mat;

            res = await _empEvaRepo.AddEvaAsync(evaadd);


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
            return await _empEvaRepo.DeleteEvaluationAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the Evaluation", ex);
        }
    }

    public async Task<ICollection<EmpEvaModel>> GetAll()
    {
        try
        {
        var evas = await _empEvaRepo.GetEvaluationsAsync();
        var empEvaModels = evas.Select(empEva => new EmpEvaModel
            {
            IdEva = empEva.IdEva,
            AccueilQualite = empEva.AccueilQualite,
            LivretInfos = empEva.LivretInfos,
            ServiceAccueil = empEva.ServiceAccueil,
            AccueilCommentaires = empEva.AccueilCommentaires,
            GeneriqueFormationQualite = empEva.GeneriqueFormationQualite,
            GeneriqueEncadrement = empEva.GeneriqueEncadrement,
            GeneriqueConditions = empEva.GeneriqueConditions,
            GeneriqueCommentaires = empEva.GeneriqueCommentaires,
            SpecifiqueFormationQualite = empEva.SpecifiqueFormationQualite,
            SpecifiqueEncadrement = empEva.SpecifiqueEncadrement,
            SpecifiqueConditions = empEva.SpecifiqueConditions,
            ObjectifsClairs = empEva.ObjectifsClairs,
            PerimetrePoste = empEva.PerimetrePoste,
            SpecifiqueCommentaires = empEva.SpecifiqueCommentaires,
            Mat = empEva.Mat
            }).ToList();

            return empEvaModels;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving all users", ex);
        }

    }



    public async Task<EmpEvaModel> GetBymat(int mat)
    {
        try
        {
            var empEva = await _empEvaRepo.GetByMat(mat);
            if (empEva == null)
            {
                return null;
            }

            return new EmpEvaModel
            {
                IdEva = empEva.IdEva,
                AccueilQualite = empEva.AccueilQualite,
                LivretInfos = empEva.LivretInfos,
                ServiceAccueil = empEva.ServiceAccueil,
                AccueilCommentaires = empEva.AccueilCommentaires,
                GeneriqueFormationQualite = empEva.GeneriqueFormationQualite,
                GeneriqueEncadrement = empEva.GeneriqueEncadrement,
                GeneriqueConditions = empEva.GeneriqueConditions,
                GeneriqueCommentaires = empEva.GeneriqueCommentaires,
                SpecifiqueFormationQualite = empEva.SpecifiqueFormationQualite,
                SpecifiqueEncadrement = empEva.SpecifiqueEncadrement,
                SpecifiqueConditions = empEva.SpecifiqueConditions,
                ObjectifsClairs = empEva.ObjectifsClairs,
                PerimetrePoste = empEva.PerimetrePoste,
                SpecifiqueCommentaires = empEva.SpecifiqueCommentaires,
                Mat = empEva.Mat
            };
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving the Employee Evaluation by Mat", ex);
        }
    }


}
