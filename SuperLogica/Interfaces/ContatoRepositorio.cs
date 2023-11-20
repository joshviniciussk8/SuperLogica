using SuperLogica.Context;
using SuperLogica.Models;
using System.Diagnostics;
using Dapper;
using System;
using Microsoft.Data.SqlClient;
using Azure.Core;
using SuperLogica.Integracao.Response;
using Newtonsoft.Json;

namespace SuperLogica.Interfaces
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly string uprApi = "https://viacep.com.br/ws/";

        private readonly BancoContext _context;
        public ContatoRepositorio(BancoContext context) => _context = context;
        public bool AlterarContato(ContatoResponse contatoResponse)
        {
            bool resultado = TaskAlterarContatoAsync(contatoResponse).Result;
            return resultado;
        }
        public async Task<bool> TaskAlterarContatoAsync(ContatoResponse contatoResponse)
        {
            string sql = @"update contato set Nome=@nome,Celular=@celular, Email=@email, Logradouro=@logradouro, Numero=@numero, Complemento=@complemento, Bairro=@bairro, Cidade=@cidade, Estado=@estado, CEP=@cEP where ContatoID = @contatoID";
            var parametros = new DynamicParameters();
            parametros.Add("contatoID", contatoResponse.ContatoID);
            parametros.Add("nome", contatoResponse.Nome);
            parametros.Add("email", contatoResponse.Email);
            parametros.Add("celular", contatoResponse.Celular);
            parametros.Add("validaCelular", contatoResponse.ValidaCelular);
            parametros.Add("logradouro", contatoResponse.Logradouro);
            parametros.Add("numero", contatoResponse.Numero);
            parametros.Add("complemento", contatoResponse.Complemento);
            parametros.Add("bairro", contatoResponse.Bairro);
            parametros.Add("cidade", contatoResponse.Cidade);
            parametros.Add("estado", contatoResponse.Estado);
            parametros.Add("cEP", contatoResponse.CEP);
            

            using var con = _context.CreateConnection();
            return await con.ExecuteAsync(sql, parametros) > 0;
        }

        public ContatoResponse BuscarContato(string numero)
        {
            var resultado = TaskBuscarContatoAsync(numero).Result;
            resultado.ValidaCelular = resultado.Celular;
            resultado.ValidaEmail = resultado.Email;

            return resultado;
        }

        public List<ContatoResponse> BuscarContatos()
        {
            var resultado = (List<ContatoResponse>)TaskBuscarContatosAsync().Result;   
            return resultado.ToList();
        }


        public async Task<ContatoResponse> TaskBuscarContatoAsync(string numero)
        {
            string sql = @$"select * from Contato where Celular = @Numero; ";
            using var con = _context.CreateConnection();
            return await con.QueryFirstOrDefaultAsync<ContatoResponse>(sql, new { Numero = numero });
           
        }

            public async Task<IEnumerable<ContatoResponse>> TaskBuscarContatosAsync()
        {
            try
            {
                string sql = @"select * from Contato";
                using var con = _context.CreateConnection();
                return await con.QueryAsync<ContatoResponse>(sql);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public ContatoResponse BuscarContatoIDAsync(int id)
        {
            return TaskBuscarContatoIDAsync(id).Result;
        }
        public async Task<ContatoResponse> TaskBuscarContatoIDAsync(int id)
        {
            string sql = @$"select * from Contato where ContatoID = @Id; ";
            using var con = _context.CreateConnection();
            return await con.QueryFirstOrDefaultAsync<ContatoResponse>(sql, new { Id = id });

        }
        public string VerificaCelularDuplicado(string celular)
        {
            return TaskCelularDuplicadoAsync(celular).Result;
        }
        public async Task<string> TaskCelularDuplicadoAsync(string celular)
        {
            string sql = @$"select * from Contato where Celular = @Numero; ";
            using var con = _context.CreateConnection();
            var resposta =  await con.QueryFirstOrDefaultAsync<ContatoResponse>(sql, new { Numero = celular });
            if (resposta != null)
            {
                return "Celular já cadastrado!";
            }
            return "Validado";
        }

        public string VerificaEmailDuplicado(string email)
        {
            return TaskEmailDuplicadoAsync(email).Result;
        }
        public async Task<string> TaskEmailDuplicadoAsync(string email)
        {
            string sql = @$"select * from Contato where Email = @Email; "; 
            using var con = _context.CreateConnection();
            var resposta = await con.QueryFirstOrDefaultAsync<ContatoResponse>(sql, new { Email = email });
            if (resposta != null)
            {
                return "Email já cadastrado!";
            }  
            return "Validado";
        }

        public async Task<bool> TaskDeletarContatoAsync(int ContatoID)
        {
            string sql = @"delete from Contato where ContatoID = @Id ";
            using var con = _context.CreateConnection();
            return await con.ExecuteAsync(sql, new { Id = ContatoID }) > 0;
        }

        public bool DeletarContatoAsync(int ContatoID)
        {
            return TaskDeletarContatoAsync(ContatoID).Result;
        }

        public async Task<bool> TaskAdicionaContatoAsync(ContatoResponse contatoResponse)
        {
            string sql = @"insert into contato(Nome,Email,Celular,Logradouro,Numero,Complemento,Bairro,Cidade,Estado,CEP)values(@Nome,@Email,@Celular,@Logradouro,@Numero,@Complemento,@Bairro,@Cidade,@Estado,@CEP)";
            using var con = _context.CreateConnection();
            return await con.ExecuteAsync(sql, contatoResponse) > 0;
        }

        public bool AdicionaContatoAsync(ContatoResponse contatoResponse)
        {
            bool result = TaskAdicionaContatoAsync(contatoResponse).Result;
            return result;
        }

        public ViaCepResponse BuscarEndereco(string cep)
        {
            try
            {
                ViaCepResponse resposta = TaskBuscarEnderecoAsync(cep).Result;
                return resposta;
            }
            catch (Exception ex)
            {
                return null;
            }
            
            
        }

        public async Task<ViaCepResponse> TaskBuscarEnderecoAsync(string cep)
        {
            var retorno = new ViaCepResponse();

            try
            {
                string teste = uprApi + cep + "/json/";
                using var cliente = new HttpClient();
                var resposta = cliente.GetStringAsync(uprApi + cep + "/json/");
                resposta.Wait();
                retorno = JsonConvert.DeserializeObject<ViaCepResponse>(resposta.Result);
                
            }
            catch
            {
                throw;
            }
            return retorno;
        }
    }
}
