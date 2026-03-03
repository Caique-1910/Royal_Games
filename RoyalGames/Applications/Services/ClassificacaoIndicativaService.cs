using RoyalGames.Domains;
using RoyalGames.DTOs.ClassificacaoIndicativaDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class ClassificacaoIndicativaService
    {
        private readonly IClassificacaoIndicativaRepository _repository;

        public ClassificacaoIndicativaService(IClassificacaoIndicativaRepository repository)
        {
            _repository = repository;
        }

        public List<LerClassificaoDTO> Listar()
        {
            List<ClassificacaoIndicativa> classificacoes = _repository.Listar();

            List<LerClassificaoDTO> classificacaoDTOs = classificacoes.Select(c => new LerClassificaoDTO
            {
                ClassificacaoInditicativoID = c.ClassificacaoInditicativoID,
                Classificacao = c.Classificacao
            }).ToList();

            return classificacaoDTOs;
        }

        public LerClassificaoDTO ObterPorId(int id)
        {
            ClassificacaoIndicativa classificacao = _repository.ObterPorId(id);

            if (classificacao == null)
            {
                throw new DomainException("Classificacao não encontrada");  
            }

            LerClassificaoDTO classificacaoDTO = new LerClassificaoDTO
            {
                ClassificacaoInditicativoID = classificacao.ClassificacaoInditicativoID,
                Classificacao = classificacao.Classificacao
            };

            return classificacaoDTO;
        }


        private static void ValidarNome(string nome)
        {
            if(string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("O nome da classificação indicativa é obrigatório.");
            }
        }

        public void Adicionar(CriarClassificacaoDTO classificacaoDTO)
        {
            if (_repository.ClassificacaoExiste(classificacaoDTO.Classificacao))
            {
                   throw new DomainException("Classificação indicativa já existe.");
            }

            ClassificacaoIndicativa classificacao = new ClassificacaoIndicativa
            {
                Classificacao = classificacaoDTO.Classificacao
            };

            _repository.Adicionar(classificacao);
        }

        public void Atualizar(int id, CriarClassificacaoDTO classificacaoDTO)
        {
            ValidarNome(classificacaoDTO.Classificacao);
             ClassificacaoIndicativa classificacaoBanco = _repository.ObterPorId(id);

            if(classificacaoBanco == null)
            {
                throw new DomainException("Classificação indicativa não encontrada.");
            }

            if (_repository.ClassificacaoExiste(classificacaoDTO.Classificacao, classificacaoIdAtual: id))
            {
                throw new DomainException("Classificação indicativa já existe.");
            }


            classificacaoBanco.Classificacao = classificacaoDTO.Classificacao;


            _repository.Atualizar(classificacaoBanco);
        }

        public void Remover(int id)
        {
            ClassificacaoIndicativa classificacaoBanco = _repository.ObterPorId(id);

            if (classificacaoBanco == null)
            {
                throw new DomainException("Classificação indicativa não encontrada.");
            }

            _repository.Remover(id);
        }
    }
}
