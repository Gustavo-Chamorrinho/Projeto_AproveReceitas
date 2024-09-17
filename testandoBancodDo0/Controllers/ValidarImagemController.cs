using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using System.Reflection;
using System.Resources;

namespace testandoBancodDo0.Controllers
{
    public class ValidarImagemController : Controller
    {
        private readonly string[] ExtencaoPermitida = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly string[] TiposMimePermitido = { "image/jpeg", "image/png", "image/gif" };
        private const long TamanhoMaximo = 5 * 1024 * 1024;
        private readonly ResourceManager _resourceManager;

        public ValidarImagemController()
        {
            _resourceManager = new ResourceManager("testandoBancodDo0.Resources.ResourceMensagensErroImg", Assembly.GetExecutingAssembly());
        }

        public List<string?> ValidarImagem(IFormFile imagem)
        {
            var mensagensErro = new List<string?>();

            if (imagem == null || imagem.Length == 0)
            {
                mensagensErro.Add(_resourceManager.GetString("SELECIONE_IMAGEM"));

            }

            if (imagem?.Length > TamanhoMaximo)
            {
                mensagensErro.Add(_resourceManager.GetString("TAMANHO_EX"));
                
            }

            var extencao = Path.GetExtension(imagem?.FileName)?.ToLowerInvariant();
            if (!ExtencaoPermitida.Contains(extencao) || !TiposMimePermitido.Contains(imagem?.ContentType))
            {
                mensagensErro.Add(_resourceManager.GetString("IMAGEM_APENAS"));
            }

            try
            {
                using (var stream = imagem?.OpenReadStream())
                {
                    if (stream != null)
                    {
                        var image = Image.Load(stream);
                    }
                    
                }

            }
            catch (UnknownImageFormatException)
            {
                mensagensErro.Add(_resourceManager.GetString("NAO_VALIDA"));
            }

            return mensagensErro;
        }
    }
}
