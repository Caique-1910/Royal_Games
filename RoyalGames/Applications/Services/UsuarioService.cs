using RoyalGames.Domains;
using RoyalGames.DTOs.UsuarioDto;
using RoyalGames.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace RoyalGames.Applications.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        private static LerUsuarioDTO LerDto (Usuario usuario)
        {
            LerUsuarioDTO lerUsuario = new LerUsuarioDTO
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                StatusUsuario = usuario.StatusUsuario ?? true
            };

            return lerUsuario;
        }

        public List<LerUsuarioDTO> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();

            List<LerUsuarioDTO> usuariosDto = usuarios.Select(u => LerDto(u)).ToList();

            return usuariosDto;
        }

        private static void ValidarEmail (string email) 
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                throw new DomainExeception("Email inválido.");
            }
        }

        private static byte[] HashSenha (string senha)
        {
            if (string.IsNullOrEmpty(senha))
            {
                throw new DomainExeception("Senha não pode ser vazia.");
            }

            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public LerUsuarioDTO ObterPorid(int id)
        {
            Usuario usuario = _repository.ObterPorId(id);
            if (usuario == null)
            {
                throw new DomainExeception("Usuário não encontrado.");
            }
            return LerDto(usuario);
        }

        public  LerUsuarioDTO ObterPorEmail(string email)
        {
            ValidarEmail(email);
            Usuario usuario = _repository.ObterPorEmail(email);
            if (usuario == null)
            {
                throw new DomainExeception("Usuário não encontrado.");
            }
            return LerDto(usuario);
        }

        public static void ValidarNome (string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new DomainExeception("Nome não pode ser vazio.");
            }
        }

        public void Adicionar (CriarUsuarioDTO usuarioDto)
        {
            ValidarNome(usuarioDto.Nome);
            ValidarEmail(usuarioDto.Email);
            if (_repository.EmailExiste(usuarioDto.Email))
            {
                throw new DomainExeception("Email já cadastrado.");
            }
            Usuario usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = Convert.ToBase64String(HashSenha(usuarioDto.Senha)),
                StatusUsuario = true
            };
            _repository.Adicionar(usuario);
        }

    }
}
