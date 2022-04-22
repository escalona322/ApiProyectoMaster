using ApiProyectoMaster.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuggetModelsPryectoJalt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiProyectoMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JugadoresController : ControllerBase
    {

        private RepositoryTorneos repo;

        public JugadoresController(RepositoryTorneos repo)
        {
            this.repo = repo;
        }
        [HttpGet("[action]")]
        public ActionResult<List<Jugador>> GetJugadores()
        {
            List<Jugador> jugadores = this.repo.GetJugadores();
            return jugadores;
        }
        [HttpGet("[action]")]
        public ActionResult<int> GetNJugadores()
        {
            int njugadores = this.repo.GetNJugadores();
            return njugadores;
        }
        [HttpGet("[action]/{posicion}")]
        public ActionResult<List<VistaJugadores>> GetVistaJugadores(int posicion)
        {
            List<VistaJugadores> VJugadores = this.repo.GetVistaJugadores(posicion);
            return VJugadores;
        }

        [HttpDelete("[action]/{idjugador}")]
        public ActionResult DeleteJugador(int idjugador)
        {
           this.repo.DeleteJugador(idjugador);
            return Ok();
        }
        [HttpGet("[action]/{idjugador}")]
        public ActionResult<Jugador> FindJugador(int idjugador)
        {
            Jugador jugador = this.repo.GetJugadorById(idjugador);
            return jugador;
        }

        [HttpPost("[action]")]
        public ActionResult InsertJugador(Jugador jugador)
        {
            this.repo.InsertJugador(jugador.IdJugador, jugador.Nick, jugador.Region,
                jugador.Nombre, jugador.Email,
                jugador.Password, jugador.Rol, jugador.Equipo);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> EnviarMailJugador(Jugador jugador)
        {
            await this.repo.EnviarMailRegistro(jugador);
            return Ok();
        }
        [HttpPut("[action]")]
        public ActionResult UpdateJugador(Jugador jugador)
        {
            this.repo.UpdateJugador(jugador.IdJugador, jugador.Nick, 
                jugador.Region, jugador.Nombre, jugador.Email, 
                jugador.Rol, jugador.Equipo);
            return Ok();
        }
        [HttpGet("[action]")]
        public ActionResult<int> GetMaxId()
        {
            int maxidjug = this.repo.GetJugadorMaxId();
            return maxidjug;
        }

        [HttpGet("[action]/{email}/{password}")]

        public ActionResult<Jugador> ExisteJugador(string email, string password)
        {
            Jugador jug = this.repo.ExisteJugador(email, password);
            return jug;
        }

        [HttpGet("[action]")]
        [Authorize]
        public ActionResult<Jugador> PerfilJugador()
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();

            string jsonJugador = claims.SingleOrDefault(z => z.Type == "UserData").Value;

            Jugador usu = JsonConvert.DeserializeObject<Jugador>(jsonJugador);
            return usu;
        }
    }
}
