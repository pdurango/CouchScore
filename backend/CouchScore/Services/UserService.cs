using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CouchScore.Helpers;
using CouchScore.Models;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Globalization;
using CouchScore.DAL;
using Microsoft.EntityFrameworkCore;

/* https://jasonwatmore.com/post/2019/10/11/aspnet-core-3-jwt-authentication-tutorial-with-example-api
 * For future reference; I have not used this yet.
 */

namespace CouchScore.Services
{
	
	public interface IUserService
	{
		Task<AuthenticateResponse> AuthenticateLoginAsync(AuthenticateRequest request);
		Task<AuthenticateResponse> AuthenticateRegisterAsync(AuthenticateRequestRegister request);
		IEnumerable<User> GetAll();
		public int GetUserIdFromToken(ClaimsPrincipal claimsPrincipal);
	}

	public class UserService : IUserService
	{
		/*
		// users hardcoded for simplicity, store in a db with hashed passwords in production applications
		private List<User> _users = new List<User>
		{
			new User { Id = 1, FirstName = "Test", LastName = "User", Username = "testtest", Password = "testtest" }
		};
		*/
		public const string STR_USERID = "UserId";

		private readonly AppSettings _appSettings;
		private DataContext _context;

		public UserService(IOptions<AppSettings> appSettings, DataContext context)
		{
			_context = context;
			_appSettings = appSettings.Value;
		}

		public int GetUserIdFromToken(ClaimsPrincipal claimsPrincipal)
		{
			var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;
			var userId = int.Parse(claimsIdentity.FindFirst("UserId")?.Value, new CultureInfo("en-us"));

			return userId;
		}

		public async Task<AuthenticateResponse> AuthenticateLoginAsync(AuthenticateRequest request)
		{
			var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == request.Username && x.Password == request.Password);

			// return null if user not found
			if (user == null) return null;

			// authentication successful so generate jwt token
			var token = GenerateJwtToken(user);

			return new AuthenticateResponse(user, token);
		}

		public async Task<AuthenticateResponse> AuthenticateRegisterAsync(AuthenticateRequestRegister request)
		{
			var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == request.Username || x.Email == request.Email);

			// return null if user not found
			if (user != null) return null;

			user = new User
			{
				FirstName = request.FirstName,
				LastName = request.LastName,
				Email = request.Email,
				Username = request.Email,
				Password = request.Password
			}; //todo - add proper hash

			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
			// authentication successful so generate jwt token
			var token = GenerateJwtToken(user);

			return new AuthenticateResponse(user, token);
		}

		public IEnumerable<User> GetAll()
		{
			return _context.Users.ToList();
		}

		/*
		 * Helper functions
		 * 
		 */

		private string GenerateJwtToken(User user)
		{
			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			var x = user.Id.ToString(new CultureInfo("en-us"));
			SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(STR_USERID, x)
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
