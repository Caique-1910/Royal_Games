using RoyalGames.Domains;
using RoyalGames.DTOs.UsuarioDto;
using RoyalGames.Interfaces;
using System.Security.Cryptography;
using RoyalGames.Exceptions;
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
                throw new DomainException("Email inválido.");
            }
        }

        private static byte[] HashSenha (string senha)
        {
            if (string.IsNullOrEmpty(senha))
            {
                throw new DomainException("Senha não pode ser vazia.");
            }

            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public LerUsuarioDTO ObterPorid(int id)
        {
            Usuario usuario = _repository.ObterPorId(id);
            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }
            return LerDto(usuario);
        }

        public  LerUsuarioDTO ObterPorEmail(string email)
        {
            ValidarEmail(email);
            Usuario usuario = _repository.ObterPorEmail(email);
            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }
            return LerDto(usuario);
        }

        public static void ValidarNome (string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new DomainException("Nome não pode ser vazio.");
            }
        }

        public LerUsuarioDTO Adicionar (CriarUsuarioDTO usuarioDto)
        {
            ValidarEmail(usuarioDto.Email);

            if (_repository.EmailExiste(usuarioDto.Email))
            {
                throw new DomainException("Email já cadastrado.");
            }
            Usuario usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = HashSenha(usuarioDto.Senha),
                StatusUsuario = true
            };
            _repository.Adicionar(usuario);

            return LerDto(usuario);
        }

        public LerUsuarioDTO Atualizar(int id, CriarUsuarioDTO usuarioDTO)
        { 
            Usuario usuarioBanco = _repository.ObterPorId(id);

            if (usuarioBanco == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            ValidarEmail(usuarioDTO.Email);
            ValidarNome(usuarioDTO.Nome);

            Usuario usuarioEmail = _repository.ObterPorEmail(usuarioDTO.Email);

            if (usuarioEmail != null && usuarioEmail.UsuarioID != id)
            {
                throw new DomainException("Email já cadastrado.");
            }

            usuarioBanco.Nome = usuarioDTO.Nome;
            usuarioBanco.Email = usuarioDTO.Email;
            usuarioBanco.Senha = HashSenha(usuarioDTO.Senha);
            usuarioBanco.StatusUsuario = usuarioDTO.StatusUsuario ?? usuarioBanco.StatusUsuario;

            _repository.Atualizar(usuarioBanco);

            return LerDto(usuarioBanco);
        }

        public void Deletar (int id)
        {
            Usuario usuario = _repository.ObterPorId(id);

            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            _repository.Deletar(id);
        }

    }
}
