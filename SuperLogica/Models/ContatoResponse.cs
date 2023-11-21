using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace SuperLogica.Models
{
    public class ContatoResponse
    {

        public int ContatoID { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo Celular é obrigatório.")]
        public string Celular { get; set; }
        [Required(ErrorMessage = "O campo Logradouro é obrigatório.")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "O campo Número é obrigatório.")]
        public string Numero { get; set; }
        public string? Complemento { get; set; }
        [Required(ErrorMessage = "O campo Bairro é obrigatório.")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "O campo Estado é obrigatório.")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "O campo CEP é obrigatório.")]
        public string CEP { get; set; }
        public string? ValidaCelular { get; set; }
        public string? ValidaEmail { get; set; }

        public static bool ValidaContato(ContatoResponse contatoResponse)
        {
            bool TodosCamposPreenchidos = !string.IsNullOrEmpty(contatoResponse.Nome) &&
                !string.IsNullOrEmpty(contatoResponse.Celular) &&
                !string.IsNullOrEmpty(contatoResponse.Email) &&
               !string.IsNullOrEmpty(contatoResponse.Logradouro) &&
               !string.IsNullOrEmpty(contatoResponse.Numero) &&
               !string.IsNullOrEmpty(contatoResponse.Bairro) &&
               !string.IsNullOrEmpty(contatoResponse.Cidade) &&
               !string.IsNullOrEmpty(contatoResponse.Estado) &&
               !string.IsNullOrEmpty(contatoResponse.CEP);
            if (TodosCamposPreenchidos)
            {
                string celular = contatoResponse.Celular.Replace("-","").Replace(" ","").Trim();
                bool validaCel = celular.Length < 11;
                string cep = contatoResponse.CEP.Replace("-", "").Trim();
                bool validaCep = cep.Length <8;
                if(!validaCel && !validaCep) return true;
                else return false;
            }
            else { return false; }
            
        }
    }
}
