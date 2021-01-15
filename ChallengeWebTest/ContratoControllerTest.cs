using System;
using Xunit;
using System.Collections.Generic;
using ChallengeWebApiJanuary.Data;
using ChallengeWebApiJanuary.Services;
using ChallengeWebApiJanuary.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.FeatureManagement;

namespace ChallengeWebTest
{
    public class ContratoControllerTest
    {
      //Mock<IMemoryCache> mockCache = new Mock<IMemoryCache>();
      
      [Fact]
      public async void GetVariosContratos()
      {
          var opcoes =  new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "Database").Options;
          using (var context = new DataContext(opcoes))
          {
            context.Contratos.Add(new Contrato{DataContratacao = "23/01/2021", QuantidadeDeParcelas = 48, ValorFinanciado = 10000});
            
            ContratoService contrato = new ContratoService(context);
            foreach(var i in await contrato.GetContratos())
            {
              Assert.NotEqual(0,i.ValorFinanciado);
              Assert.Equal(48,i.QuantidadeDeParcelas);
              Assert.Equal(10000,i.ValorFinanciado);

            }
          }

      }
        
    }
}
