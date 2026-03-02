using RoyalGames.Applications.Conversoes;
using RoyalGames.Applications.Regras;
using RoyalGames.Domains;
using RoyalGames.DTOs.JogoDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class JogoService
    {
        private readonly IJogoRepository _repository;

        public JogoService(IJogoRepository repository)
        {
            _repository = repository;
        }

        public List<LerJogoDTO> Listar()
        {
            List<Jogo> jogos = _repository.Listar();

            List<LerJogoDTO> jogosDto = jogos.Select(JogoParaDTO.ConverterParaDto).ToList();

            return jogosDto;
        }

        public LerJogoDTO ObterPorId(int id)
        {
            Jogo jogo = _repository.ObterPorId(id);

            if (jogo == null)
            {
                throw new DomainException("Jogo não encontrado");
            }

            return JogoParaDTO.ConverterParaDto(jogo);
        }

        private static void ValidarCadastro(CriarJogoDTO jogoDTO)
        {
            if (string.IsNullOrWhiteSpace(jogoDTO.Nome))
            {
                throw new DomainException("O nome do jogo é obrigatório.");
            }
            if (jogoDTO.Preco <= 0)
            {
                throw new DomainException("O preço do jogo deve ser maior que zero.");
            }
            if (string.IsNullOrEmpty(jogoDTO.Descricao))
            {
                throw new DomainException("A descrição do jogo deve conter pelo menos 10 caracteres.");
            }
            if (jogoDTO.Imagem == null || jogoDTO.Imagem.Length == 0)
            {
                throw new DomainException("A imagem do jogo é obrigatória.");
            }
            if (jogoDTO.GeneroIDs == null || jogoDTO.GeneroIDs.Count == 0)
            {
                throw new DomainException("O jogo deve estar associado a pelo menos um gênero.");
            }

        }

        public byte[] ObterImagem(int id)
        {
            byte[] imagem = _repository.ObterPorImagem(id);

            if (imagem == null || imagem.Length == 0)
            {
                throw new DomainException("Imagem do jogo não encontrada.");
            }

            return imagem;
        }

        public LerJogoDTO Adicionar(CriarJogoDTO jogoDTO, int usuarioId)
        {
            ValidarCadastro(jogoDTO);

            if (_repository.JogoExiste(jogoDTO.Nome))
            {
                throw new DomainException("Já existe um jogo com esse nome.");
            }

            Jogo jogo = new Jogo
            {
                Nome = jogoDTO.Nome,
                Preco = jogoDTO.Preco,
                Descricao = jogoDTO.Descricao,
                Imagem = ImagemParaBytes.ConverterParaBytes(jogoDTO.Imagem),
                StatusJogo = true,
                UsuarioID = usuarioId,
            };



            _repository.Adicionar(jogo, jogoDTO.GeneroIDs);

            return JogoParaDTO.ConverterParaDto(jogo);
        }

        public LerJogoDTO Atualizar(int id, CriarJogoDTO jogoDTO)
        {
            HorarioAlteracaoJogo.ValidarHorario();

            Jogo jogoBanco = _repository.ObterPorId(id);

            if (jogoBanco == null)
            {
                throw new DomainException("Jogo não encotrado");
            }

            if (_repository.JogoExiste(jogoDTO.Nome, jogoIdAtual: id))
            {
                throw new DomainException("Já existe um jogo com esse nome.");
            }

            if (jogoDTO.GeneroIDs == null || jogoDTO.GeneroIDs.Count == 0)
            {
                throw new DomainException("O jogo deve estar associado a pelo menos um gênero.");
            }

            if (jogoDTO.Preco <= 0)
            {
                throw new DomainException("O preço do jogo deve ser maior que zero.");
            }

            jogoBanco.Nome = jogoDTO.Nome;
            jogoBanco.Preco = jogoDTO.Preco;
            jogoBanco.Descricao = jogoDTO.Descricao;

            if (jogoDTO.Imagem != null && jogoDTO.Imagem.Length > 0)
            {
                jogoBanco.Imagem = ImagemParaBytes.ConverterParaBytes(jogoDTO.Imagem);
            }

            _repository.Atualizar(jogoBanco, jogoDTO.GeneroIDs);

            return JogoParaDTO.ConverterParaDto(jogoBanco);
        }

        public void Remover(int id)
        {
            HorarioAlteracaoJogo.ValidarHorario();

            Jogo jogoBanco = _repository.ObterPorId(id);

            if (jogoBanco == null)
            {
                throw new DomainException("Jogo não encontrado");
            }

            _repository.Remover(id);
        }
    }
}
