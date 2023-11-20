using SuperLogica.Integracao.Response;
using SuperLogica.Models;

namespace SuperLogica.Interfaces
{
    public interface IContatoRepositorio
    {
        List<ContatoResponse> BuscarContatos();
        public Task<IEnumerable<ContatoResponse>> TaskBuscarContatosAsync();
        ContatoResponse BuscarContato(string numero);
        public Task<ContatoResponse> TaskBuscarContatoAsync(string numero);
        Task<bool> TaskAlterarContatoAsync(ContatoResponse contatoResponse);
        bool AlterarContato(ContatoResponse contatoResponse);
        string VerificaCelularDuplicado(string celular);
        string VerificaEmailDuplicado(string email);
        Task<bool> TaskDeletarContatoAsync(int ContatoID);
        bool DeletarContatoAsync(int ContatoID);
        public ContatoResponse BuscarContatoIDAsync(int id);
        Task<bool> TaskAdicionaContatoAsync(ContatoResponse contatoResponse);
        bool AdicionaContatoAsync(ContatoResponse contatoResponse);
        ViaCepResponse BuscarEndereco(string cep);
        Task<ViaCepResponse> TaskBuscarEnderecoAsync(string cep);

    }
}
