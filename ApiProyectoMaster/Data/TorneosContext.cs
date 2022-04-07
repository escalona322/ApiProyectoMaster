using Microsoft.EntityFrameworkCore;
using NuggetModelsPryectoJalt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProyectoMaster.Data
{
    public class TorneosContext : DbContext
    {
        public TorneosContext
            (DbContextOptions<TorneosContext> context) : base(context)
        {

        }

        public DbSet<Apuntado> Apuntados { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Torneo> Torneos { get; set; }
        public DbSet<Jugador> Jugadores { get; set; }
        public DbSet<VistaTorneo> VistaTorneos { get; set; }
        public DbSet<VistaApuntadosJugadores> VistaApJug { get; set; }
        public DbSet<VistaSetFormateado> VistaSetFormateados { get; set; }
        public DbSet<VistaJugadores> VistaJugadores { get; set; }
        public DbSet<VistaApuntadosTorneo> VistaApTor { get; set; }
    }
}
