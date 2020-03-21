using DesafioCodenation.Objeto;
using RestSharp;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Ajax.Utilities;

namespace DesafioCodenation
{
    class Program
    {
        static void Main(string[] args)
        {
            Frase resposta = ObtemMensagemCriptografada();
            string mensagemDescriptografada = Descriptografar.Criptografia.descriptogrfar(resposta.cifrado, resposta.numero_casas);
            string mensagemSHA1 = Descriptografar.Criptografia.GetSHA1(mensagemDescriptografada);

            resposta.decifrado = mensagemDescriptografada;
            resposta.resumo_criptografico = mensagemSHA1;

            Enviar();
        }

        /// <summary>
        /// Obtem via API REST os dados do desafio
        /// </summary>
        /// <returns></returns>
        private static Frase ObtemMensagemCriptografada()
        {
            var client = new RestClient("https://api.codenation.dev/v1/challenge/dev-ps/generate-data?token=a8768f5698004ce05455c0da25f38bf3f5147db1");
            RestRequest request = new RestRequest(Method.GET);

            var response = client.Get(request);
            var resposta = JsonConvert.DeserializeObject<Frase>(response.Content);
            return resposta;
        }

        /// <summary>
        /// Envia para API REST o arquivo contendo a resposta do desafio
        /// </summary>
        private static void Enviar()
        {
            string filepath = $@"{Directory.GetCurrentDirectory()}\Arquivo\answer.json";

            RestClient restClient = new RestClient($@"https://api.codenation.dev/v1/challenge/dev-ps/submit-solution?token=a8768f5698004ce05455c0da25f38bf3f5147db1");
            RestRequest restRequest = new RestRequest();
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.Method = Method.POST;
            restRequest.AddFile("answer", filepath, "multipart/form-data");
            var response = restClient.Execute(restRequest);


            // score = 65
        }
    }
}
