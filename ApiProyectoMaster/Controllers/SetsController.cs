using ApiProyectoMaster.Repositories;
using Microsoft.AspNetCore.Http;
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
    public class SetsController : ControllerBase
    {
        private RepositoryTorneos repo;

        public SetsController(RepositoryTorneos repo)
        {
            this.repo = repo;
        }

        [HttpGet("[action]")]
        public ActionResult<List<Set>> GetSets()
        {
            List<Set> sets = this.repo.GetSets();
            return sets;
        }

        [HttpGet("[action]/{idset}")]
        public ActionResult<Set> FindSet(int idset)
        {
            Set set = this.repo.GetSetById(idset);
            return set;
        }
        [HttpGet("[action]/{idapuntado}")]
        public ActionResult<List<VistaSetFormateado>> GetSetsFormateadoApuntado(int idapuntado)
        {
            List<VistaSetFormateado> sets = this.repo.GetSetsFormatByIdApuntado(idapuntado);
            return sets;
        }
        [HttpGet("[action]/{idjugador}")]
        public ActionResult<List<VistaSetFormateado>> GetSetsFormateadoJugador(int idjugador)
        {
            List<VistaSetFormateado> sets = this.repo.GetSetsFormatByIdJugador(idjugador);
            return sets;
        }

        [HttpGet("[action]")]
        public ActionResult<int> GetSetMaxId()
        {
            int maxidset = this.repo.GetSetMaxId();
            return maxidset;
        }

        [HttpPost("[action]")]
        public ActionResult InsertSet(Set set)
        {
            this.repo.InsertSet(set.IdSet, set.IdApuntado1, set.IdApuntado2, 
                set.Ganador, set.Resultado, set.Ronda, set.IdTorneo);

            return Ok();
        }
        [HttpPut("[action]")]
        public ActionResult UpdateSet(Set set)
        {
            this.repo.UpdateSet(set.IdSet, set.IdApuntado1, set.IdApuntado2,
                set.Ganador, set.Resultado, set.Ronda, set.IdTorneo);

            return Ok();
        }


        [HttpDelete("[action]/{idset}")]
        public ActionResult DeleteSet(int idset)
        {
            this.repo.DeleteSet(idset);
            return Ok();
        }
    }
}
