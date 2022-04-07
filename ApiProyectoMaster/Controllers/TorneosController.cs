using ApiProyectoMaster.Repositories;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<int> GetNTorneos()
        {
            int ntorneos = this.repo.GetNumeroTorneos();
            return ntorneos;
        }

        public ActionResult
    }
}
