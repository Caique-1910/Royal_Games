using Microsoft.IdentityModel.Tokens;
using RoyalGames.Domains;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RoyalGames.Applications.Autenticacao
{
    public class GeradorTokenJwt
    {
        private readonly IConfiguration _config;

        public GeradorTokenJwt(IConfiguration config)
        {
            _config = config;
        }

        public string GerarToken (Usuario usuario)
        {
            var chave = _config["Jwt:Key"];
            
            var issuer = _config["Jwt:Issuer"];

            var audience = _config["Jwt:Audience"];

            var expiraMinutos = int.Parse(_config["Jwt:ExpiraMinutos"]);

            var keyBytes = Encoding.UTF8.GetBytes(chave);

            if (keyBytes.Length < 32) 
            {
                throw new DomainEception("Jwt: ky precisa ter pelo menos 32 caracteres (256 bits).");
            }

            var securityKey = new SymmetricSecurityKey(keyBytes);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claiims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioID.ToString()),

                new Claim(ClaimTypes.Name, usuario.Nome),

                new Claim(ClaimTypes.Email, usuario.Email),

                new Claim(ClaimTypes.Role, "true")
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claiims,
                expires: DateTime.Now.AddMinutes(expiraMinutos),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
