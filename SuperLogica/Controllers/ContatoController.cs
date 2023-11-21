using Microsoft.AspNetCore.Mvc;
using SuperLogica.Integracao.Response;
using SuperLogica.Interfaces;
using SuperLogica.Models;
using System.Runtime.ConstrainedExecution;

namespace SuperLogica.Controllers
{
    public class ContatoController : Controller
    {
        public readonly IContatoRepositorio _contatoRepositorio;
        public ContatoController(IContatoRepositorio contatoRepositorio) => _contatoRepositorio = contatoRepositorio;

        public IActionResult TodosContatos()
        {
            List<ContatoResponse> contaList = _contatoRepositorio.BuscarContatos().ToList();
            return View(contaList);
        }
        public IActionResult Detalhes(string celular)
        {
            return View(_contatoRepositorio.BuscarContato(celular));
        }
        
        public ActionResult ExcluirContato(int ContatoID)
        {
            return View(_contatoRepositorio.BuscarContatoIDAsync(ContatoID));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void ExcluirContato(int ContatoID, ContatoResponse contato)
        {
            try
            {
                _contatoRepositorio.DeletarContatoAsync(ContatoID);
                var RawURL = String.Format("/Contato/TodosContatos");
                Response.Redirect(RawURL);
                
            }
            catch
            {
                var RawURL = String.Format("/Contato/TodosContatos");
                Response.Redirect(RawURL);
            }
        }
        public ActionResult CriarContato()
        {
            return View();
        }

        // POST: AlunosController/Create
        [HttpPost]
        public IActionResult CriarContato(ContatoResponse contato)
        {
            try
            {
                if (!ContatoResponse.ValidaContato(contato))
                {
                    ViewBag.AcaoBemSucedida = false;
                    ViewData["message"] = "Informações Inválidas";
                    return View();
                }
                if (_contatoRepositorio.VerificaEmailDuplicado(contato.Email) != "Validado")
                {
                    ViewBag.AcaoBemSucedida = false;
                    ViewData["message"] = "Esse Email já foi cadastrado";
                    return View();
                }
                if (_contatoRepositorio.VerificaCelularDuplicado(contato.Celular) != "Validado")
                {
                    ViewBag.AcaoBemSucedida = false;
                    ViewData["message"] = "Esse Celular já foi cadastrado";
                    return View();
                }
                if (_contatoRepositorio.AdicionaContatoAsync(contato))
                {
                    ViewBag.AcaoBemSucedida = true;
                    ViewData["message"] = "Cadastro Efetuado com sucesso!!!";
                    //var RawURL = String.Format("/Contato/TodosContatos");
                    //Response.Redirect(RawURL);
                    return View();
                }
                return View();

            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ViaCepResponse CarregaEndereco(string cep)
        {
            var resposta = _contatoRepositorio.BuscarEndereco(cep);
            if (resposta == null)
            {
                TempData["mensagemErro"] = "CEP Não Encontrado!!!";
                return null;
                
            }
            return resposta;
            
        }
        public IActionResult EditarContato(string celular)
        {
            
            return View(_contatoRepositorio.BuscarContato(celular));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarContato(ContatoResponse contatoResponse)
        {
                        
            if (!ContatoResponse.ValidaContato(contatoResponse))
            {
                ViewData["message"] = "Dados faltando, verifique os campos e tente novamente!";
                return View();
            }
            if (contatoResponse.ValidaCelular != contatoResponse.Celular)
            {
                string resposta = _contatoRepositorio.VerificaCelularDuplicado(contatoResponse.Celular);
                ViewData["message"] = resposta;
                if(resposta!="Validado") return View();

            }
            if (contatoResponse.ValidaEmail != contatoResponse.Email)
            {
                string resposta = _contatoRepositorio.VerificaEmailDuplicado(contatoResponse.Email);
                ViewData["message"] = resposta;
                if (resposta != "Validado") return View();
            }

            if (_contatoRepositorio.AlterarContato(contatoResponse))
            {
                //var RawURL = String.Format("/Contato/EditarContato?celular={0}", contatoResponse.Celular);
                //Response.Redirect(RawURL);
                ViewData["message"] = "Cadastro Atualizado com Sucesso!";
                return View();
            }

            ViewData["message"] = "Algo deu errado!";
            return View();
        }
    }
}
