using ApiProyectoMaster.Repositories;
using Microsoft.AspNetCore.Mvc;
using NuggetModelsPryectoJalt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProyectoMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TorneosController : ControllerBase
    {
        private RepositoryTorneos repo;

        public TorneosController(RepositoryTorneos repo)
        {
            this.repo = repo;
        }
        [HttpGet("[action]")]
        public ActionResult<int> GetNTorneos()
        {
            int ntorneos = this.repo.GetNumeroTorneos();
            return ntorneos;
        }

        [HttpGet("[action]/{idtorneo}")]
        public ActionResult<Torneo> FindTorneo(int idtorneo)
        {
            Torneo torneo = this.repo.GetTorneoById(idtorneo);
            return torneo;
        }
        [HttpGet("[action]")]
        public ActionResult<List<Torneo>> GetTorneos()
        {
            List<Torneo> torneos = this.repo.GetTorneos();
            return torneos;
        }


        [HttpGet("[action]/{posicion}")]
        public ActionResult<List<VistaTorneo>> GetTorneosPag(int posicion)
        {
            List<VistaTorneo> torneospag = this.repo.GetTorneosPaginado(posicion);
            return torneospag;
        }


        [HttpPost("[action]")]
        public ActionResult InsertTorneo(Torneo torneo)
        {
            this.repo.InsertTorneo(torneo.IdTorneo, torneo.Nombre, torneo.Region, torneo.Fecha, torneo.Napuntados,
                torneo.Descripcion, torneo.Normas, torneo.Tipo, 
                torneo.Link, torneo.Foto);

            return Ok();
        }
        [HttpPut("[action]")]
        public ActionResult UpdateTorneo(Torneo torneo)
        {
            this.repo.UpdateTorneo(torneo.IdTorneo, torneo.Nombre, torneo.Region, torneo.Fecha, torneo.Napuntados,
                torneo.Descripcion, torneo.Normas, torneo.Tipo,
                torneo.Link, torneo.Foto);

            return Ok();
        }


        [HttpDelete("[action]/{idtorneo}")]
        public ActionResult DeleteTorneo(int idtorneo)
        {
            this.repo.DeleteTorneo(idtorneo);
            return Ok();
        }

        [HttpGet("[action]")]
        public ActionResult<int> GetTorneoMaxId()
        {
            int MaxId = this.repo.GetTorneoMaxId();
            return MaxId;
        }

        [HttpPut("[action]/{idtorneo}")]
        public ActionResult SumarApuntado(int idtorneo)
        {
            this.repo.SumarApuntado(idtorneo);
            return Ok();
        }

        [HttpGet("[action]/{idtorneo}")]
        public ActionResult<int> GetNApuntadosTorneo(int idtorneo)
        {
            int napuntados = this.repo.GetNApuntadosTorneo(idtorneo);
            return napuntados;
        }

        [HttpGet("[action]/{idjugador}")]
        public ActionResult<List<Torneo>> GetTorneosByJugador(int idjugador)
        {
            List<Torneo> torneos = this.repo.GetTorneosByIdJugador(idjugador);
            return torneos;
        }




    }
}