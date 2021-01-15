using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeWebApiJanuary.Data;
using ChallengeWebApiJanuary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.FeatureManagement;

namespace ChallengeWebApiJanuary.Services
{
    public interface IContratoService
    {
        Task<List<Contrato>> GetContratos();
        Task<Contrato> DeleteContrato ( int id);
        Task<Contrato> EditarContrato (int id, Contrato model);
        Task<Contrato> GetListar(int id,[FromServices] IFeatureManager featureManager, [FromServices] IMemoryCache cache);

    }
}