﻿using BlazorWasm.Compartilhado.Entidades;
using BlazorWasmServer.Server;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasmServer.Server.Controllers
{
    [ApiController]

    [Route("api/Carro")]
    public class CarroController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public CarroController(ApplicationDbContext context)
        {
            this.context = context;


        }
        [HttpPost]
        public async Task<ActionResult<int>> Post(Carro carro)
        {

            context.Carro.Add(carro);
            await context.SaveChangesAsync();
            return carro.Id;

        }



        [HttpGet]
        public async Task<ActionResult<List<Carro>>> Get()
        {
            
            return await context.Carro.ToListAsync();
        }


        [HttpGet("{Marca}")]
        public async Task<ActionResult<Carro>> Get(string Marca)
        {
            var Localizar = await context.Carro.FirstOrDefaultAsync(x => x.Marca == Marca);
            if (Localizar == null)
            {
                return NotFound();
            }
            return Localizar;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Carro>> Get(int id)
        {
            var resp = await context.Carro.FirstOrDefaultAsync(x => x.Id == id);
            if (resp == null)
            {
                return NotFound();
            }
            return resp;
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var carro = await context.Carro.FirstOrDefaultAsync(x => x.Id == id);
            if (carro == null)
            {
                return NotFound();
            }
            context.Remove(carro);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut]
        public async Task<ActionResult> Put(Carro carro)
        {
            context.Attach(carro).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();

        }
    }
}


