using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using Gerencianet.NETCore.SDK;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace GerenciaNet.Services
{
    // Decompiled with JetBrains decompiler
// Type: Gerencianet.NETCore.SDK.Endpoints
// Assembly: Gerencianet.NETCore.SDK, Version=2.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 2700D0C7-F44F-4A77-BC07-5058143DE170
// Assembly location: C:\Users\Warley\.nuget\packages\gerencianet.netcore.sdk\2.0.2\lib\netcoreapp2.1\Gerencianet.NETCore.SDK.dll
namespace Gerencianet.NETCore.SDK
{
  public class LocalEndpoints : DynamicObject
  {
    private const string version = "2.0.2";
    private static string clientId;
    private static string clientSecret;
    private static JObject endpoints;
    private static JObject urls;
    private static string token;
    private static bool sandbox;
    private static string certificate;
    private string baseURL;

    public LocalEndpoints(JObject options)
    {
      ClientId = (string) options["client_id"];
      ClientSecret = (string) options["client_secret"];
      var jobject = JObject.Parse(new Constants().getConstant());
      endpoints = (JObject) jobject["ENDPOINTS"];
      urls = (JObject) jobject["URLS"];
      Sandbox = (bool) options[nameof (sandbox)];
      Certificate = (string) options["pix_cert"];
    }

    public LocalEndpoints(string clientId, string clientSecret, bool sandbox, string certificate)
    {
      ClientId = clientId;
      ClientSecret = clientSecret;
      var jobject = JObject.Parse(new Constants().getConstant());
      endpoints = (JObject) jobject["ENDPOINTS"];
      urls = (JObject) jobject["URLS"];
      Sandbox = sandbox;
      Certificate = certificate;
    }

    public override bool TryInvokeMember(
      InvokeMemberBinder binder,
      object[] args,
      out object result)
    {
      JObject jobject;
      if ((JObject) endpoints["PIX"][binder.Name] != null)
      {
        baseURL = Sandbox ? (string) urls["PIX"]["sandbox"] : (string) urls["PIX"]["production"];
        jobject = (JObject) endpoints["PIX"][binder.Name];
        if (Certificate == null)
          throw new GnException(1, "certificate_not_found", "Para utilizar os endpoints do pix é necessário informar o caminho do certificado.");
      }
      else
      {
        baseURL = Sandbox ? (string) urls["DEFAULT"]["sandbox"] : (string) urls["DEFAULT"]["production"];
        jobject = (JObject) endpoints["DEFAULT"][binder.Name];
        Certificate = null;
      }
      var endpoint = jobject != null ? (string) jobject["route"] : throw new GnException(0, "invalid_endpoint", string.Format("Método '{0}' inexistente", binder.Name));
      var method = (string) jobject["method"];
      object body = null;
      object query = null;
      string headersComplement = null;
      if (args.Length != 0 && args[0] != null)
        query = args[0];
      if (args.Length > 1 && args[1] != null)
        body = args[1];
      if (args.Length > 2 && args[2] != null)
        headersComplement = (string) args[2];
      Authenticate();
      try
      {
        result = RequestEndpoint(endpoint, method, query, body, headersComplement);
        return true;
      }
      catch (GnException ex)
      {
        if (ex.Code != 401)
          throw ex;
        Authenticate();
        result = RequestEndpoint(endpoint, method, query, body, headersComplement);
        return true;
      }
    }

    private void Authenticate()
    {
      var s = string.Format("{0}:{1}", ClientId, ClientSecret);
      var base64String = Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(s));
      var restRequest1 = new RestRequest(Method.POST);
      restRequest1.AddHeader("Authorization", string.Format("Basic {0}", base64String));
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
      IRestResponse restResponse;
      if (Certificate != null)
      {
        var restClient = new RestClient(baseURL + "/oauth/token");
        try
        {
          var cert = new X509Certificate2(certificate, string.Empty, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
          restClient.ClientCertificates = new X509Certificate2Collection()
          {
            cert
          };
        }
        catch (Exception e)
        {
          Console.WriteLine(e);
        }
        restRequest1.AddHeader("Content-Type", "application/json");
        restRequest1.AddHeader("Cache-Control", "no-cache");
        restRequest1.AddHeader("Accept", "application/json");
        restRequest1.AddParameter( "{\r\n    \"grant_type\": \"client_credentials\"\r\n}", ParameterType.RequestBody);
        restResponse = restClient.Execute(restRequest1);
      }
      else
      {
        var restClient = new RestClient(baseURL + "/authorize");
        restRequest1.AddJsonBody("{\r\n    \"grant_type\": \"client_credentials\"\r\n}");
        var restRequest2 = restRequest1;
        restResponse = restClient.Execute(restRequest2);
      }
      Token = JObject.Parse(restResponse.Content)["access_token"].ToString();
    }

    private object RequestEndpoint(
      string endpoint,
      string method,
      object query,
      object body,
      string headersComplement)
    {
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
      var endpointRequest = GetEndpointRequest(endpoint, method, query);
      var request = new RestRequest();
      if (method == "PUT")
        request.Method = Method.PUT;
      else if (method == "GET")
        request.Method = Method.GET;
      else if (method == "POST")
        request.Method = Method.POST;
      else if (method == "DELETE")
        request.Method = Method.DELETE;
      else if (method == "PATCH")
        request.Method = Method.PATCH;
      request.AddHeader("Authorization", string.Format("Bearer {0}", Token));
      request.AddHeader("api-sdk", string.Format("dotnet-core-{0}", "2.0.2"));
      if (headersComplement != null)
      {
        var jobject = JObject.Parse(headersComplement);
        if (jobject["x-skip-mtls-checking"] != null)
          request.AddHeader("x-skip-mtls-checking", (string) jobject["x-skip-mtls-checking"]);
      }
      try
      {
        return SendRequest(request, body, endpointRequest);
      }
      catch (WebException ex)
      {
        if (ex.Response != null && ex.Response is HttpWebResponse)
        {
          var statusCode = (int) ((HttpWebResponse) ex.Response).StatusCode;
        }
        throw GnException.Build("", 500);
      }
    }

    public string GetEndpointRequest(string endpoint, string method, object query)
    {
      if (query != null)
      {
        var bindingAttr = BindingFlags.Instance | BindingFlags.Public;
        var dictionary = new Dictionary<string, object>();
        foreach (var property in query.GetType().GetProperties(bindingAttr))
        {
          if (property.CanRead)
            dictionary.Add(property.Name, property.GetValue(query, null));
        }
        var matchCollection = Regex.Matches(endpoint, ":([a-zA-Z0-9]+)");
        for (var i = 0; i < matchCollection.Count; ++i)
        {
          var key = matchCollection[i].Groups[1].Value;
          try
          {
            var replacement = dictionary[key].ToString();
            endpoint = Regex.Replace(endpoint, string.Format(":{0}", key), replacement);
            dictionary.Remove(key);
          }
          catch (Exception ex)
          {
          }
        }
        var str = "";
        foreach (var keyValuePair in dictionary)
        {
          str = !str.Equals("") ? str + "&" : "?";
          str += string.Format("{0}={1}", keyValuePair.Key, keyValuePair.Value);
        }
        endpoint += str;
      }
      return endpoint;
    }

    public object SendRequest(RestRequest request, object body, string newEndpoint)
    {
      if (body != null)
        request.AddJsonBody(body);
      var restClient = new RestClient(baseURL + newEndpoint);
      if (certificate != null)
      {
        var x509Certificate2 = new X509Certificate2(certificate, "");
        restClient.ClientCertificates = new X509CertificateCollection
        {
          x509Certificate2
        };
      }
      return restClient.Execute(request).Content;
    }

    private static string ClientId
    {
      get => clientId;
      set => clientId = value;
    }

    public static string ClientSecret
    {
      get => clientSecret;
      set => clientSecret = value;
    }

    public static bool Sandbox
    {
      get => sandbox;
      set => sandbox = value;
    }

    public static string Certificate
    {
      get => certificate;
      set => certificate = value;
    }

    public static string Token
    {
      get => token;
      set => token = value;
    }
  }
}

}