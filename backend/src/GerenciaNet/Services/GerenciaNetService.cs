using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Sampaio.Shared.Config;
using Sampaio.Shared.Utils;
using GerenciaNet.Models.Request;
using GerenciaNet.Models.Response;
using Gerencianet.NETCore.SDK;
using GerenciaNet.Services.Contracts;
using GerenciaNet.Services.Gerencianet.NETCore.SDK;

namespace GerenciaNet.Services
{
    public class GerenciaNetService : IGerenciaNetService
    {
        private readonly GerenciaNetConfig _config;
        private readonly dynamic _endpoints;
        public GerenciaNetService(GerenciaNetConfig config)
        {
            _config = config;
            var directoryName = 
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" +
                // Path.GetFullPath("./")+
                                _config.Credentials;
            _endpoints = new Endpoints(_config.ClientId, _config.ClientSecret, _config.Sandbox,directoryName);
        }

        public async Task<dynamic> GenerateBankingBillet(GenerateBankingBilletRequest request)
        {
            try
            {
                var json = JsonUtils.Serialize(request);
                var response = _endpoints.OneStep(null, json);
                if (response.ToString() == "Unauthorized")
                {
                    return new GenerateBankingBilletResponse()
                    {
                        Code = 0,
                    };
                }

                return JsonUtils.Deserialize<GenerateBankingBilletResponse>(response.ToString());
            }
            catch (GnException e)
            {
                Console.WriteLine("Erro ao gerar boleto", e);
                return e;
            }
        }

        public async Task<PixResponse> CreatePixRequest(CobRequest request)
        {
            try
            {
                var json = JsonUtils.Serialize(request.Body);
                var response = _endpoints.PixCreateImmediateCharge(null, json);
                var cob = JsonUtils.Deserialize<CobResponse>(response);
                var qrCode = _endpoints.PixGenerateQRCode(new {id= cob?.Loc?.Id});
                PixResponse resp = JsonUtils.Deserialize<PixResponse>(qrCode);
                resp.Success = cob?.Chave != null;
                resp.Response = response;
                resp.CobId = cob?.Txid?.ToString();
                return resp;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao criar solicitação (pix)", e);
                return new PixResponse()
                {
                    Message = e.Message,
                    Success = false
                };
            }
        }
        public async Task<CobInfoResponse> CheckCob(CobInfoRequest request)
        {
            try
            {
                var response = _endpoints.PixDetailCharge(new {txid =request.CobId});
                var resp = JsonUtils.Deserialize<CobInfoResponse>(response);
                resp.Success = resp?.Chave != null;
                resp.Success = resp?.Chave != null;
                return resp;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao criar solicitação (pix)", e);
                return new CobInfoResponse()
                {
                    Success = false
                };
            }
        }
    }
    
}

