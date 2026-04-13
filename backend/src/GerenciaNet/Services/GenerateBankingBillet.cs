using System;
using System.Threading.Tasks;
using GerenciaNet.Models.Request;
using Gerencianet.SDK;
using GerenciaNet.Services.Contracts;
using InfinityLoq.Shared.Config;

namespace GerenciaNet.Services
{
    public class GenerateBankingBilletService : IGenerateBankingBilletService
    {
        private readonly GerenciaNetConfig _config;
        private readonly dynamic _endpoints;
        
        public GenerateBankingBilletService(GerenciaNetConfig config)
        {
            _config = config;
            _endpoints = new Endpoints(_config.ClientId, _config.ClientSecret, _config.Sandbox);
        }

        public async Task<dynamic> GenerateBankingBillet(GenerateBankingBilletRequest request)
        {
            try
            {
                var response = await _endpoints.OneStep(null, request);
                return response;
            }
            catch (GnException e)
            {
                Console.WriteLine("Erro ao gerar boleto", e);
                return e;
            }
        }
    }
}