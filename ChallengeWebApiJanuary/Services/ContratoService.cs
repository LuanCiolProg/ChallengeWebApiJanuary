using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeWebApiJanuary.Data;
using ChallengeWebApiJanuary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.FeatureManagement;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ChallengeWebApiJanuary.Services
{
    
    public class ContratoService : IContratoService
    {
        private DataContext context;
         private readonly string contratoKey = "contrato";
    
        public ContratoService(DataContext context)
        {
            this.context = context;
        }
        public async Task<List<Contrato>> GetContratos()
       {
           var contrato = await context.Contratos.ToListAsync();
           return contrato;
       } 
        public async Task<Contrato> DeleteContrato ( int id) 
        {
            Contrato contrato = await context.Contratos.FindAsync(id);
            context.Contratos.Remove(contrato);
            await context.SaveChangesAsync();

            return contrato;
        }
        
        public async Task<Contrato> EditarContrato (int id, Contrato model)
        {
        Contrato contrato = await context.Contratos.FindAsync(id);
            contrato.DataContratacao = model.DataContratacao;
            contrato.QuantidadeDeParcelas = model.QuantidadeDeParcelas;
            contrato.ValorFinanciado = model.ValorFinanciado;
            await context.SaveChangesAsync();
            return contrato;
        }

            public async Task<Contrato> GetListar (int id, IFeatureManager featureManager, IMemoryCache cache)
        {
            Contrato contrato;
            if (await featureManager.IsEnabledAsync(nameof(FeatureManagementEnum.MemoryCache)))
            {
                if (cache.TryGetValue<Contrato>(contratoKey + id, out contrato))
                if(contrato != null)
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