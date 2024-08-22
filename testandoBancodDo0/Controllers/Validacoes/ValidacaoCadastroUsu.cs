using System.ComponentModel.DataAnnotations;
using System.Resources;
using System.Text.RegularExpressions;
using testandoBancodDo0.Context;
using testandoBancodDo0.Models;

namespace testandoBancodDo0.Controllers.Validacoes
{
    public class ValidacaoCadastroUsu
    {
        private readonly ResourceManager _resourceManager;
        private readonly AproveDbContext _dbContext;

        public ValidacaoCadastroUsu(ResourceManager resourceManager, AproveDbContext dbContext)
        {
            _resourceManager = resourceManager;
            _dbContext = dbContext;
        }

        public List<string?>Validar(UsuarioModel model, string confirmPassword)
        {
            var MensagensErro = new List<string?>();

            if (!SenhasIguais(model.Senha, confirmPassword))
            {
                MensagensErro.Add(_resourceManager.GetString("SENHA_DIFERENTE"));
            }

            if (!QuantidadeCaracter(model.Senha))
            {
                MensagensErro.Add(_resourceManager.GetString("SENHA_QUANTIDADE"));
            }

            if (EmailExistente(model.Email))
            {
                MensagensErro.Add(_resourceManager.GetString("EMAIL_VERIFICA"));
            }

            if (!CaracterEspecial(model.Senha))
            {
                MensagensErro.Add(_resourceManager.GetString("SENHA_CARACTER_ESPECIAL"));
            }

            if (!ValidarFormatoEmail(model.Email))
            {
                MensagensErro.Add(_resourceManager.GetString("VALIDA_FORMATO_EMAIL"));
            }

            return MensagensErro;
        }

        private bool SenhasIguais(string? senha, string? confirmPassword)
        {
            return senha == confirmPassword;
        }
        
        private bool QuantidadeCaracter(string? senha)
        {
            return senha.Length >= 6;
        }

        private bool EmailExistente(string? email)
        {
            return _dbContext.usuarios.Any(u => u.Email == email);
        }

        private bool CaracterEspecial(string senha)
        {
            string caracters = @"[^a-zA-Z0-9]";
            return Regex.IsMatch(senha, caracters);
        }

        private bool ValidarFormatoEmail(string? email)
        {
            var emailAttribute = new EmailAddressAttribute();
            return emailAttribute.IsValid(email);   
        }


    }
}
