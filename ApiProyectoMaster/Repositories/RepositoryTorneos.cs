using ApiProyectoMaster.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuggetModelsPryectoJalt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProyectoMaster.Repositories
{
    public class RepositoryTorneos
    {
        #region STORED PROCEDURES / VISTAS
        //    alter VIEW V_TORNEO_INDIVIDUAL
        //AS

        //    select
        //    CAST(row_number() over(order by idTorneo) as int)

        //   as posicion, isnull(idTorneo, 0) as idTorneo
        //   , Nombre, Region, Fecha, Napuntados, Descripcion,
        //   Normas, Tipo, Link from Torneos
        //Go
        //    Alter View V_APUNTADOS_JUGADOR
        //AS

        //    select
        //    CAST(row_number() over(order by idInscripcion) as int) as posicion,
        //	idInscripcion, idTorneo, apuntados.idJugador,
        //	Puesto, Record, Seed, Nombre, Nick, Equipo, Region
        //    from apuntados
        //    inner join Jugadores

        //    on apuntados.idJugador = Jugadores.idJugador
        //GO

        //    ALTER PROCEDURE SP_APUNTADOS_JUGADOR
        //    (@idtorneo int, @registros int out, @posicion int)
        //as
        //	select @registros

        //    select* FROM V_APUNTADOS_JUGADOR
        //   where idtorneo = @idtorneo and

        //   
        //go
        // PROCEDURE SETS CON NOMBRE JUGADORES BY IDAPUNTADO
        //    CREATE PROCEDURE SP_SETS_APUNTADO
        //    (@IdApuntado int)
        //as

        //select sets.idSet, sets.Ganador,
        //Resultado, Ronda, sets.idTorneo,
        //(select Nick from Jugadores
        //inner join apuntados
        //on Jugadores.idJugador = apuntados.idJugador
        //where idInscripcion = idApuntado2) as Nick2
        //,
        //(select Nick from Jugadores
        //inner join apuntados
        //on Jugadores.idJugador = apuntados.idJugador
        //where idInscripcion = idApuntado1) as Nick1
        //,
        //(select Nick from Jugadores
        //inner join apuntados
        //on Jugadores.idJugador = apuntados.idJugador
        //where idInscripcion = Ganador) as NickGanador
        //from Jugadores
        //inner join apuntados
        //on Jugadores.idJugador = apuntados.idJugador
        //inner join sets
        //on sets.idApuntado1 = idInscripcion or
        //sets.idApuntado2 = idInscripcion
        //where apuntados.IdInscripcion = @IdApuntado

        //go
        //PROCEDURE GET SETS FORMATEADO BY IDJUGADOR
        //    CREATE PROCEDURE SP_SETS_JUGADOR
        //    (@idjugador int)
        //as
        //	select sets.idSet, sets.Ganador,
        //Resultado, Ronda, sets.idTorneo,
        //(select Nick from Jugadores
        //inner join apuntados
        //on Jugadores.idJugador = apuntados.idJugador
        //where idInscripcion = idApuntado2) as Nick2
        //,
        //(select Nick from Jugadores
        //inner join apuntados
        //on Jugadores.idJugador = apuntados.idJugador
        //where idInscripcion = idApuntado1) as Nick1
        //,
        //(select Nick from Jugadores
        //inner join apuntados
        //on Jugadores.idJugador = apuntados.idJugador
        //where idInscripcion = Ganador) as NickGanador
        //from Jugadores
        //inner join apuntados
        //on Jugadores.idJugador = apuntados.idJugador
        //inner join sets
        //on sets.idApuntado1 = idInscripcion or
        //sets.idApuntado2 = idInscripcion
        //where jugadores.idJugador = @idjugador
        //go

        #endregion
       
            private TorneosContext context;

            public RepositoryTorneos(TorneosContext context)
            {
                this.context = context;
            }

            #region METODOS TORNEOS

            public int GetNumeroTorneos()
            {
                return this.context.Torneos.Count();
            }
            public List<Torneo> GetTorneos()
            {
                var consulta = from datos in this.context.Torneos
                               select datos;
                return consulta.ToList();
            }
            public Torneo GetTorneoById(int idtorneo)
            {
                var consulta = from datos in this.context.Torneos
                               where datos.IdTorneo == idtorneo
                               select datos;
                return consulta.SingleOrDefault();
            }
            public List<VistaTorneo> GetTorneosPaginado(int posicion)
            {
                var consulta = from datos in this.context.VistaTorneos
                               where datos.Posicion >= posicion
                               && datos.Posicion < (posicion + 2)
                               select datos;
                return consulta.ToList();
            }
            public void InsertTorneo(int idtorneo, string nombre, string region, DateTime fecha, int napuntados, string descripcion, string normas, string tipo, string link, string foto)
            {
                Torneo TorneoNuevo = new Torneo
                {
                    IdTorneo = idtorneo,
                    Nombre = nombre,
                    Region = region,
                    Fecha = fecha,
                    Napuntados = napuntados,
                    Descripcion = descripcion,
                    Normas = normas,
                    Tipo = tipo,
                    Link = link,
                    Foto = foto
                };
                this.context.Torneos.Add(TorneoNuevo);
                this.context.SaveChanges();
            }
            public void UpdateTorneo(int idtorneo, string nombre, string region, DateTime fecha, int napuntados, string descripcion, string normas, string tipo, string link, string foto)
            {
                Torneo TorneoEditar = this.GetTorneoById(idtorneo);
                TorneoEditar.Nombre = nombre;
                TorneoEditar.Region = region;
                TorneoEditar.Fecha = fecha;
                TorneoEditar.Napuntados = napuntados;
                TorneoEditar.Descripcion = descripcion;
                TorneoEditar.Normas = normas;
                TorneoEditar.Tipo = tipo;
                TorneoEditar.Link = link;
                TorneoEditar.Foto = foto;
                this.context.SaveChanges();
            }
            public int GetTorneoMaxId()
            {
                int idmax = this.context.Torneos.Max(x => x.IdTorneo);
                return idmax;
            }
            public void SumarApuntado(int idtorneo)
            {
                Torneo TorneoEditar = this.GetTorneoById(idtorneo);
                TorneoEditar.Napuntados += 1;
                this.context.SaveChanges();

            }
            public void DeleteTorneo(int idtorneo)
            {
                Torneo torneoelim = this.GetTorneoById(idtorneo);
                this.context.Torneos.Remove(torneoelim);
                this.context.SaveChanges();
            }
            public List<Torneo> GetTorneosByIdJugador(int idjugador)
            {
                string sql = "SP_TORNEOS_JUGADOR @idjugador";

                SqlParameter paramidtorneo = new SqlParameter("@idjugador", idjugador);

                var consulta =
                    this.context.Torneos.FromSqlRaw
                    (sql, paramidtorneo);

                List<Torneo> Torneos = consulta.ToList();

                return Torneos;
            }
            public int GetNApuntadosTorneo(int idtorneo)
            {
                var consulta = from datos in this.context.Torneos
                               where datos.IdTorneo == idtorneo
                               select datos.Napuntados;

                return consulta.FirstOrDefault();

            }
            #endregion

            #region METODOS SETS
            public List<Set> GetSets()
            {
                var consulta = from datos in this.context.Sets
                               select datos;
                return consulta.ToList();
            }
            public Set GetSetById(int idset)
            {
                var consulta = from datos in this.context.Sets
                               where datos.IdSet == idset
                               select datos;
                return consulta.FirstOrDefault();
            }
            public void InsertSet(int idset, int ap1, int ap2, int apganador, string resultado, string ronda, int idtorneo)
            {
                Set SetNuevo = new Set
                {
                    IdSet = idset,
                    IdApuntado1 = ap1,
                    IdApuntado2 = ap2,
                    Ganador = apganador,
                    Resultado = resultado,
                    Ronda = ronda,
                    IdTorneo = idtorneo
                };
                this.context.Sets.Add(SetNuevo);
                this.context.SaveChanges();
            }
            public void UpdateSet(int idset, int ap1, int ap2, int apganador, string resultado, string ronda, int idtorneo)
            {
                Set SetEditar = this.GetSetById(idset);
                SetEditar.IdApuntado1 = ap1;
                SetEditar.IdApuntado2 = ap2;
                SetEditar.Ganador = apganador;
                SetEditar.Resultado = resultado;
                SetEditar.Ronda = ronda;
                SetEditar.IdTorneo = idtorneo;
                SetEditar.IdApuntado1 = ap1;
                this.context.SaveChanges();
            }
            public int GetSetMaxId()
            {
                int idmax = this.context.Sets.Max(x => x.IdSet);
                return idmax;
            }
            public List<VistaSetFormateado> GetSetsFormatByIdApuntado(int idapuntado)
            {
                string sql = "SP_SETS_APUNTADO @idapuntado";

                SqlParameter paramidap = new SqlParameter("@idapuntado", idapuntado);

                var consulta =
                    this.context.VistaSetFormateados.FromSqlRaw
                    (sql, paramidap);

                List<VistaSetFormateado> Sets = consulta.ToList();

                return Sets;
            }
            public List<VistaSetFormateado> GetSetsFormatByIdJugador(int idjugador)
            {
                string sql = "SP_SETS_JUGADOR @idjugador";

                SqlParameter paramidap = new SqlParameter("@idjugador", idjugador);

                var consulta =
                    this.context.VistaSetFormateados.FromSqlRaw
                    (sql, paramidap);

                List<VistaSetFormateado> Sets = consulta.ToList();

                return Sets;
            }
            #endregion

            #region METODOS APUNTADOS
            public List<Apuntado> GetApuntados()
            {
                var consulta = from datos in this.context.Apuntados
                               select datos;
                return consulta.ToList();
            }
            public List<Apuntado> GetApuntadosByTorneo(int idtorneo)
            {
                var consulta = from datos in this.context.Apuntados
                               where datos.IdTorneo == idtorneo
                               select datos;
                return consulta.ToList();
            }
            public List<VistaApuntadosJugadores> GetVApuntadosByTorneo(int idtorneo, int posicion)
            {
                string sql = "SP_APUNTADOS_JUGADOR @idtorneo, @POSICION";

                SqlParameter paramidtorneo = new SqlParameter("@idtorneo", idtorneo);
                SqlParameter parampos = new SqlParameter("@POSICION", posicion);


                var consulta =
                    this.context.VistaApJug.FromSqlRaw
                    (sql, paramidtorneo, parampos);

                List<VistaApuntadosJugadores> VApuntados = consulta.ToList();

                return VApuntados;
            }
             public List<VistaApuntadosTorneo> GetVApuntadosByTorneoNoPag(int idtorneo)
            {
                var consulta = from datos in this.context.VistaApTor
                               where datos.IdTorneo == idtorneo
                               select datos;
                return consulta.ToList();
            }
            public List<VistaApuntadosTorneo> GetVApuntados()
            {
                var consulta = from datos in this.context.VistaApTor
                               select datos;
                return consulta.ToList();
            }
            public Apuntado GetApuntadoById(int idapuntado)
            {
                var consulta = from datos in this.context.Apuntados
                               where datos.IdApuntado == idapuntado
                               select datos;
                return consulta.FirstOrDefault();
            }
            public void InsertApuntado(int idinscripcion, int idtorneo, int idjugador, int puesto, string record, int seed)
            {
                Apuntado ApuntadoNuevo = new Apuntado
                {
                    IdApuntado = idinscripcion,
                    IdTorneo = idtorneo,
                    IdJugador = idjugador,
                    Puesto = puesto,
                    Record = record,
                    Seed = seed,
                };
                this.context.Apuntados.Add(ApuntadoNuevo);
                this.context.SaveChanges();
            }
            public void DeleteApuntado(int idinscripcion)
            {
                Apuntado apuntadoelim = this.GetApuntadoById(idinscripcion);
                this.context.Apuntados.Remove(apuntadoelim);
                this.context.SaveChanges();
            }
            public void UpdateApuntado(int idinscripcion, int idtorneo, int idjugador, int puesto, string record, int seed)
            {
                Apuntado ApuntadoEditar = this.GetApuntadoById(idinscripcion);
                ApuntadoEditar.IdTorneo = idtorneo;
                ApuntadoEditar.IdJugador = idjugador;
                ApuntadoEditar.Puesto = puesto;
                ApuntadoEditar.Record = record;
                ApuntadoEditar.Seed = seed;
                this.context.SaveChanges();
            }
            public int GetApuntadoMaxId()
            {
                int idapMax = this.context.Apuntados.Max(x => x.IdApuntado);
                return idapMax + 1;
            }
            #endregion

            #region METODOS JUGADORES
            public List<Jugador> GetJugadores()
            {
                var consulta = from datos in this.context.Jugadores
                               select datos;
                return consulta.ToList();
            }
            public int GetNJugadores()
            {
                var consulta = from datos in this.context.Jugadores
                               select datos;
                int numjugadores = consulta.ToList().Count();
                return numjugadores;
            }
            public List<VistaJugadores> GetVistaJugadores(int posicion)
            {
                string sql = "SP_JUGADOR  @POSICION";


                SqlParameter parampos = new SqlParameter("@POSICION", posicion);


                var consulta =
                    this.context.VistaJugadores.FromSqlRaw
                    (sql, parampos);

                List<VistaJugadores> VJugadores = consulta.ToList();

                return VJugadores;
            }
            public void DeleteJugador(int idjugador)
            {
                Jugador jugadorelim = this.GetJugadorById(idjugador);
                this.context.Jugadores.Remove(jugadorelim);
                this.context.SaveChanges();
            }
            public Jugador GetJugadorById(int idjugador)
            {
                var consulta = from datos in this.context.Jugadores
                               where datos.IdJugador == idjugador
                               select datos;
                return consulta.SingleOrDefault();
            }
            public void InsertJugador(int idjugador, string nick, string region, string nombre, string email, string password, string rol, string equipo)
            {
                Jugador JugadorNuevo = new Jugador
                {
                    IdJugador = idjugador,
                    Nick = nick,
                    Region = region,
                    Nombre = nombre,
                    Email = email,
                    Password = password,
                    Rol = rol,
                    Equipo = equipo
                };
                this.context.Jugadores.Add(JugadorNuevo);
                this.context.SaveChanges();
            }
            public void UpdateJugador(int idjugador, string nick, string region, string nombre, string email, string rol, string equipo)
            {
                Jugador JugadorEditar = this.GetJugadorById(idjugador);
                JugadorEditar.Nick = nick;
                JugadorEditar.Region = region;
                JugadorEditar.Nombre = nombre;
                JugadorEditar.Email = email;
                JugadorEditar.Rol = rol;
                JugadorEditar.Equipo = equipo;
                this.context.SaveChanges();
            }
            public int GetJugadorMaxId()
            {
                int idmax = this.context.Jugadores.Max(x => x.IdJugador);
                return idmax;
            }

            public Jugador ExisteJugador(string email, string password)
            {
                var consulta = from datos in this.context.Jugadores
                               where datos.Email == email
                               && datos.Password == password
                               select datos;
                return consulta.SingleOrDefault();
            }
            #endregion

        }
    }

