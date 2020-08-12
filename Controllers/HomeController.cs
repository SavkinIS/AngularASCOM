using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Angular.Auth;
using Angular.DataFolder;
using Angular.Interfaces;
using Angular.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;


namespace Angular.Controllers
{
    [ApiController]
    [Route("api/sneakers")]
    public class HomeController : Controller
    {
        IUnitOfWorks data;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;


        public HomeController(UserManager<User> userManager,
                                SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.data = new UnitOfWorcks();
        }

        // GET: Home
        /// <summary>
        /// Передает список предметов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Sneakers> Get()
        {
          
            return data.Sneakers.GetDataList();
        }

        /// <summary>
        /// Передает один предмет по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Home/Details/5
        [HttpGet("{id}")]
        public Sneakers Get(int id)
        {
            return data.Sneakers.GetDataItem(id);
        }

        // GET: Home/Create
        /// <summary>
        /// Добавление предмета в БД
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        [HttpPost]        
        public ActionResult Post(Sneakers sn)
        {
            if (ModelState.IsValid)
            {
                data.Sneakers.Create(sn);
                return Ok(sn);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Изменеие предмета
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        // POST: Home/Chenge
        [HttpPut]        
        public ActionResult Create(Sneakers sn)
        {
            if (ModelState.IsValid)
            {
                data.Sneakers.Update(sn);
                return Ok(sn);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// удаление предмета
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]        
        public IActionResult Delete(int id)
        {

            var sn = data.Sneakers.GetDataItem(id);
            if (sn != null)
            {
                data.Sneakers.Delete(id);
                data.Sneakers.Save();
            }

            return Ok();

        }
        
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("login")]
        public async Task<ActionResult> Login([FromBody]UserLogin model)
        {
            SignInResult loginResult = new SignInResult();

            if (ModelState.IsValid)
            {
                loginResult = await this.signInManager.PasswordSignInAsync(model.LoginProp,
                    model.Password,
                    false,
                    lockoutOnFailure: false);

                if (loginResult.Succeeded)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.LoginProp),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new { Token = tokenString });
                }
                return Unauthorized();
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Выход
        /// </summary>
        /// <returns></returns>
        [Route("out")]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return Ok();
        }
    }
}