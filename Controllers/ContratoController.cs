using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChallengeWebApiJanuary.Data;
using ChallengeWebApiJanuary.Models;
using System;
using Microsoft.FeatureManagement;
using Microsoft.Extensions.Caching.Memory;
using ChallengeWebApiJanuary;

namespace testeef.Controllers
{
    [ApiController]
    [Route("v1/contrato")]

    public class ContratoController : ControllerBase
    {
       private IMemoryCache cache;
       private DataContext _context;

       private IFeatureManager featureManager;

       private readonly string contratoKey = "contrato";
       public ContratoController(DataContext context, IMemoryCache cache, IFeatureManager featureManager)
       {
           _context=context;
           this.cache = cache;
           this.featureManager = featureManager;
       }
       
       [HttpGet]
       [Route("")]
       public async Task<ActionResult<List<Contrato>>> Get([FromServices] DataContext context)
       {
           var contrato = await context.Contratos.ToListAsync();
           return contrato;
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
                Contrato contrato = await context.Contratos.FindAsync(id);
                context.Contratos.Remove(contrato);
                await context.SaveChangesAsync();

                return contrato;
            }

            [HttpPut]
            [Route("Editar/{id}")]

            public async Task<Contrato> Editar ([FromServices] DataContext context, [FromBody]Contrato model, int id) 
            {
                Console.WriteLine(id);
                Contrato contrato = await context.Contratos.FindAsync(id);
                contrato.DataContratacao = model.DataContratacao;
                contrato.QuantidadeDeParcelas = model.QuantidadeDeParcelas;
                contrato.ValorFinanciado = model.ValorFinanciado;
                await context.SaveChangesAsync();

                return contrato;
            }

            [HttpGet]
            [Route("Listar/{id}")]
            
            public async Task<ActionResult<Contrato>> Get([FromServices] DataContext context, int id)
            {
                Contrato contrato;
                if (await featureManager.IsEnabledAsync(nameof(FeatureManagementEnum.MemoryCache)))
                {
                    if (cache.TryGetValue<Contrato>(contratoKey + id, out contrato))
                    return contrato;
                }
                contrato = await context.Contratos.FirstOrDefaultAsync(w => id == w.Id);
                var DataAtual = DateTime.Now;
                var DataExpiracao = new DateTimeOffset(DataAtual.Year,DataAtual.Month,DataAtual.Day + 1,0,0,0,TimeSpan.FromHours(-3));
                cache.Set<Contrato>(contratoKey + id, contrato, DataExpiracao);
                return contrato;
            }
        }
}