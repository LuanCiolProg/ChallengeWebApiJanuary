using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeWebApiJanuary.Data;
using ChallengeWebApiJanuary.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeWebApiJanuary.Services
{
    public interface IContratoService
    {
        Task<ActionResult<List<Contrato>>> GetContratos();
        Task<Contrato> DeleteContrato ( int id);
        Task<Contrato> EditarContrato (int id, Contrato model);
        Task<ActionResult<Contrato>> GetListar(int id);

    }
}