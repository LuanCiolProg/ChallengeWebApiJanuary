using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChallengeWebApiJanuary.Data;
using ChallengeWebApiJanuary.Models;
using System;
using Microsoft.FeatureManagement;
using Microsoft.Extensions.Caching.Memory;
using ChallengeWebApiJanuary.Services;

namespace ChallengeWebApiJanuary.Controllers
{
    [ApiController]
    [Route("v1/contrato")]

    public class ContratoController : ControllerBase
    {
       private IMemoryCache cache;
       private DataContext _context;
        private IContratoService contratoService;
       private IFeatureManager featureManager;

      
       public ContratoController(DataContext context, IMemoryCache cache, IFeatureManager featureManager, IContratoService contratoService)
       {
           _context=context;
           this.cache = cache;
           this.featureManager = featureManager;
           this.contratoService = contratoService;
       }
       
       [HttpGet]
       [Route("")]
       public async Task<ActionResult<List<Contrato>>> Get([FromServices] DataContext context)
       {
           return await contratoService.GetContratos();
       } 

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Contrato>> Post(
            [FromServices] DataContext context,
            [FromBody]Contrato model)
            {
                if(ModelState.IsValid)
                {
                    context.Contratos.Add(model);
                    await context.SaveChangesAsync();
                    return model;
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }

            [HttpDelete]
            [Route("Deletar/{id}")]

            public async Task<Contrato> Deletar ([FromServices] DataContext context, int id) 
            {

                return await contratoService.DeleteContrato(id);
            }

            [HttpPut]
            [Route("Editar/{id}")]

            public async Task<Contrato> Editar ([FromServices] DataContext context, [FromBody]Contrato model, int id) 
            {
                
                return await contratoService.EditarContrato(id, model);
            }

            [HttpGet]
            [Route("Listar/{id}")]
            
            public async Task<ActionResult<Contrato>> Get([FromServices] DataContext context, int id,[FromServices] IFeatureManager featureManager, [FromServices] IMemoryCache cache)
            {
                
                return await contratoService.GetListar(id, featureManager, cache);
            }
        }
}