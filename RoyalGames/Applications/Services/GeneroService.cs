using RoyalGames.Domains;
using RoyalGames.DTOs.GeneroDto;
using RoyalGames.Interfaces;
using RoyalGames.Exceptions;

namespace RoyalGames.Applications.Services
{
    public class GeneroService
    {
        private readonly IGeneroRepository _repository;

        public GeneroService(IGeneroRepository repository)
        {
            _repository = repository;
        }

        public List<LerGeneroDto> Listar()
        {
            List<Genero> generos = _repository.Listar();

            List<LerGeneroDto> generoDto = generos.Select(genero => new LerGeneroDto
            {
                GeneroID = genero.GeneroID,
                Nome = genero.Nome,
            }).ToList();

            return generoDto;
        }

        public LerGeneroDto ObterPorId(int id)
        {
            Genero genero = _repository.ObterPorId(id);

            if(genero == null)
            {
                throw new CannotUnloadAppDomainException("Genero nao encontrada");
            }

            LerGeneroDto generoDto = new LerGeneroDto()
            {
                GeneroID = genero.GeneroID,
                Nome = genero.Nome,
            };

            return generoDto;   
        }

        private static void ValidarNome(string nome)
        {
            if(string.IsNullOrEmpty(nome))
            {
                throw new CannotUnloadAppDomainException("Nome eh obrigatorio");
            }
        }

        public void Adicionar(CriarGeneroDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            if (_repository.NomeExiste(criarDto.Nome)) 
            {
                throw new DomainException("Genero ja existente.");
            }

            Genero genero = new Genero
            {
                Nome = criarDto.Nome,
            };

            _repository.Adicionar(genero);
        }

        public void Atualizar(int id, CriarGeneroDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            Genero generoBanco = _repository.ObterPorId(id);

            if(generoBanco == null)
            {
                throw new DomainException("Genero nao encontrada.");
            }

            if(_repository.NomeExiste(criarDto.Nome, generoIdAtual: id))
            {
                throw new DomainException("Ja existe outro Genero com esse nome.");
            }

            generoBanco.Nome = criarDto.Nome;
            _repository.Atualizar(generoBanco);
        }

        public void Remover(int id)
        {
            Genero generoBanco = _repository.ObterPorId(id);

            if(generoBanco == null)
            {
                throw new DomainException("Genero nao encontrado.");
            }

            _repository.Remover(id);
        }
    }
}
