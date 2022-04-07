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
    public class ApuntadosController : ControllerBase
    {
        private RepositoryTorneos repo;
        
        public ApuntadosController(RepositoryTorneos repo)
        {
            this.repo = repo;
        }

        [HttpGet("[action]")]
        public ActionResult<List<Apuntado>> GetApuntados()
        {
            return this.repo.GetApuntados();
        }

        [HttpGet("[action]/{idtorneo}")]
        public ActionResult<List<Apuntado>> GetApuntadosByTorneo(int idtorneo)
        {
            return this.repo.GetApuntadosByTorneo(idtorneo);
        }

        [HttpGet("[action]/{idtorneo}/{posicion}")]
        public ActionResult<List<VistaApuntadosJugadores>> GetVApuntadosByTorneo(int idtorneo, int posicion)
        {
            return this.repo.GetVApuntadosByTorneo(idtorneo, posicion);
        }

        [HttpGet("[action]")]
        public ActionResult<List<VistaApuntadosTorneo>> GetApuntadosAdmin()
        {
            return this.repo.GetVApuntados();
        }

        [HttpGet("[action]/{idtorneo}")]
        public ActionResult<List<VistaApuntadosTorneo>> GetApuntadosAdminByTorneo(int idtorneo)
        {
            return this.repo.GetVApuntadosByTorneoNoPag(idtorneo);
        }


        [HttpGet("[action]")]
        public ActionResult<List<VistaApuntadosTorneo>> GetVApuntados()
        {
            return this.repo.GetVApuntados();
        }

        [HttpGet("[action]/{idapuntado}")]
        public ActionResult<Apuntado> FindApuntado(int idapuntado)
        {
            return this.repo.GetApuntadoById(idapuntado);
        }

        [HttpPost("[action]")]
        public ActionResult InsertApuntado(Apuntado apuntado)
        {
            int maxid = this.repo.GetApuntadoMaxId();
            this.repo.InsertApuntado(maxid, apuntado.IdTorneo, apuntado.IdJugador
                , apuntado.Puesto, apuntado.Record, apuntado.Seed);
            return Ok();
        }

        [HttpPut("[action]")]
        public ActionResult UpdateApuntado(Apuntado apuntado)
        {
            this.repo.UpdateApuntado(apuntado.IdApuntado, apuntado.IdTorneo,
                apuntado.IdJugador, apuntado.Puesto, apuntado.Record, apuntado.Seed);
            return Ok();
        }

        [HttpDelete("[action]/{idapuntado}")]
        public ActionResult DeleteApuntado(int idapuntado)
        {
            this.repo.DeleteApuntado(idapuntado);
            return Ok();
        }
    }
}
