using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CouchScore.Entities;
using CouchScore.Helpers;
using CouchScore.Models;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Globalization;

namespace CouchScore.Services
{
	public interface IUserService
	{
		AuthenticateResponse AuthenticateLogin(AuthenticateRequestLogin request);
		AuthenticateResponse AuthenticateRegister(AuthenticateRequestRegister request);
		IEnumerable<User> GetAll();
	}

	public class UserService : IUserService
	{
		// users hardcoded for simplicity, store in a db with hashed passwords in production applications
		private List<User> _users = new List<User>
		{
			new User { Id = 1, FirstName = "Test", LastName = "User", Username = "testtest", Password = "testtest" }
		};

		private readonly AppSettings _appSettings;

		public UserService(IOptions<AppSettings> appSettings)
		{
			_appSettings = appSettings.Value;
		}

		public AuthenticateResponse AuthenticateLogin(AuthenticateRequestLogin request)
		{
			var user = _users.SingleOrDefault(x => x.Username == request.Username && x.Password == request.Password);

			// return null if user not found
			if (user == null) return null;

			// authentication successful so generate jwt token
			var token = GenerateJwtToken(user);

			return new AuthenticateResponse(user, token);
		}

		public AuthenticateResponse AuthenticateRegister(AuthenticateRequestRegister request)
		{
			var user = _users.SingleOrDefault(x => x.Username == request.Username && x.Password == request.Password);

			// return null if user not found
			if (user == null) return null;

			// authentication successful so generate jwt token
			var token = GenerateJwtToken(user);

			return new AuthenticateResponse(user, token);
		}

		public IEnumerable<User> GetAll()
		{
			return _users;
		}

		/*
		 * Helper functions
		 * 
		 */

		private string GenerateJwtToken(User user)
		{
			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.Id.ToString(new CultureInfo("en-us")))
				}),
				//Expires = DateTime.UtcNow.AddMinutes(1),
				Expires = DateTime.UtcNow.AddDays(365), //todo - change to proper value
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

	}
}
