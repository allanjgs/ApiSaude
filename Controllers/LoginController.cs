using System;
using ApiAuthSaude.Models;
using ApiAuthSaude.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuthSaude.Controllers
{
	[ApiController]
	[Route("v1")]
	public class LoginController : ControllerBase
	{
		[HttpPost]
		[Route("login")]
		public ActionResult<dynamic> Authenticate([FromBody] User model)
		{
			//recupera o ususario
			var user = UserRepository.Get(model.Username, model.Password);

			//Verifica se o ususario existe
			if (user == null) return NotFound(new { message = "Usuario ou senha inválidos" });


			//Gera o token
			var token = TokenService.GenerateToken(user);

			// Oculta a senha
			user.Password = "";

			//Retorna os dados
			return new
			{
				user = user,
				token = token
			};
		}

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
		public ActionResult<dynamic> NotFound(object value)
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
		{
			throw new NotImplementedException();
		}
	}
}
