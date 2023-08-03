using Ecommerce.API.Models;
using Ecommerce.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        //bc
        private IUsuarioRepository _repository;

        public UsuariosController()
        {
            _repository = new UsuarioRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var usuario = _repository.Get(id);
            if(usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Insert([FromBody]Usuario usuario)
        {
            _repository.Insert(usuario);
            return Ok(usuario);
        }

        [HttpPut]
        public IActionResult Update([FromBody]Usuario usuario)
        {
            _repository.Update(usuario);
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok();
        }
    }
}

//CRUD
// - GET: obter a lista de usuários
// - GET: obter o usuário passando o ID
// - POST: Cadastrar um usuário 
// - PUT: Atualizar um usuário 
// - DELETE: Remover um usuário 

//Metodo HTTP
//Ex Get(): www.minhaApi.com.br/api/Usuario
//Ex Get(Usuario): www.minhaApi.com.br/api/Usuario/2

//Retornos
//200 ok
//300 redirecionamento para outro endereço
//400 erro dados por parte do usuario
//404 not found 
//500 servidor, exception
