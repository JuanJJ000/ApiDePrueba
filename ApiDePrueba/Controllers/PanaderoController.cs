using ApiDePrueba.Models;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiDePrueba.Controllers
{
    public class PanaderoController : ApiController
    {

        [HttpGet]

        public IHttpActionResult Get()
        {

            List<PanaderoViewModel> PanaderoLista = new List<PanaderoViewModel>();

            using (PruebaEntities db = new PruebaEntities())
            {

                PanaderoLista = (from l in db.Panadero
                                 select new PanaderoViewModel
                                 {
                                     Id = l.Id,
                                     Nombre = l.Nombre,
                                     Apellido= l.Apellido,
                                     Edad = l.Edad,
                                     Carnet = l.Carnet


                                 }).ToList();
            }

            return Ok(PanaderoLista)
;

        }

        [HttpPost]

        public IHttpActionResult Post(PanaderoViewModel panaderoViewModel)
        {

           

            using (PruebaEntities db = new PruebaEntities())
            {
                var oPanaderoView = new Panadero();

                oPanaderoView.Nombre = panaderoViewModel.Nombre;
                oPanaderoView.Apellido = panaderoViewModel.Apellido;
                oPanaderoView.Edad = panaderoViewModel.Edad;
                oPanaderoView.Carnet = panaderoViewModel.Carnet;
                db.Panadero.Add(oPanaderoView);
                db.SaveChanges();



            }

            return Ok("Se ha registrado el panadero, exitosamente");
        }

        [HttpPut]

        public IHttpActionResult Put (PanaderoViewModel panaderoViewModel)
        {

            using (PruebaEntities pruebaEntities = new PruebaEntities())
            {
                var oPanaderoView = pruebaEntities.Panadero.Where(l => l.Id == panaderoViewModel.Id)
                    .FirstOrDefault<Panadero>();

                if (oPanaderoView != null)
                {
                    oPanaderoView.Nombre = panaderoViewModel.Nombre;
                    oPanaderoView.Apellido = panaderoViewModel.Apellido;
                    oPanaderoView.Edad = panaderoViewModel.Edad;
                    oPanaderoView.Carnet = panaderoViewModel.Carnet;

                    pruebaEntities.SaveChanges();

                }
                else { return NotFound(); }


            }

            return Ok();

        }

        [HttpDelete]

        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest("No es un id de libro válido");
            using (PruebaEntities db = new PruebaEntities())
            {
                var panadero = db.Panadero.Where(l => l.Id == id)
                    .FirstOrDefault();
                db.Entry(panadero).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            return Ok();

        }
    }
}
