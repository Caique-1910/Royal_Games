using RoyalGames.Domains;
using RoyalGames.DTOs.PlataformaDto;
using RoyalGames.Interfaces;
using VHBurguer.Exceptions;

namespace RoyalGames.Applications.Services
{
    public class PlataformaService
    {
        private readonly IPlataformaRepository _repository;
        public PlataformaService(IPlataformaRepository repository)
        {
            _repository = repository;
        }

        public List<LerPlataformaDto> Listar()
        {
            List<Plataforma> plataformas = _repository.Listar();

            // converte cada categoria para LerCategoriaDto
            List<LerPlataformaDto> plataformaDto = plataformas.Select(plataforma => new LerPlataformaDto
            {
                PlataformaID = plataforma.PlataformaId,
                Nome = plataforma.Nome,
            }).ToList();

            //Retorna a lista ja convertida em DTO 
            return plataformaDto;
        }

        public LerPlataformaDto ObterPorId(int id)
        {
            Plataforma plataforma = _repository.ObterPorId(id);

            if (plataforma == null)
            {
                throw new DomainException("Plataforma nao encontrada");
            }

            LerPlataformaDto plataformaDto = new LerPlataformaDto
            {
                PlataformaID = plataforma.PlataformaId,
                Nome = plataforma.Nome
            };

            return plataformaDto;
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new DomainException("Nome é obrigatorio");
            }
        }

        public void Adicionar(CriarPlataformaDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            if (_repository.NomeExiste(criarDto.Nome))
            {
                throw new DomainException("Plataforma ja existente.");
            }

            Plataforma plataforma = new Plataforma
            {
                Nome = criarDto.Nome,
            };

            _repository.Adicionar(plataforma);
        }

        public void Atualizar(int id, CriarPlataformaDto criarDto)
        {
            ValidarNome(criarDto.Nome); // valida se o campo nome foi preenchido 

            Plataforma plataformaBanco = _repository.ObterPorId(id);

            if (plataformaBanco == null)
            {
                throw new DomainException("Plataforma nao encontrada.");
            }

            //categoriaIdAtual: id -> categoriaIdAtual recebe id 
            if (_repository.NomeExiste(criarDto.Nome, PlataformaIdAtual: id))
            {
                throw new DomainException("Ja existe outra Plataforma com esse nome.");
            }

            plataformaBanco.Nome = criarDto.Nome;
            _repository.Atualizar(plataformaBanco);
        }

        public void Remover(int id)
        {
            Plataforma plataformaBanco = _repository.ObterPorId(id);

            if (plataformaBanco == null)
            {
                throw new DomainException("Plataforma nao encontrada.");
            }

            _repository.Remover(id);
        }

    }
}
