using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Dal.Interface;
using PlanoriaCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Dal.Repository
{
    public class ArchivoRepository : IArchivoRepository
    {
        private readonly AppDbContext _context;

        public ArchivoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CrearAsync(ArchivoSubido archivo)
        {
            await _context.ArchivosSubidos
                .AddAsync(archivo);
        }

        public async Task ActualizarAsync(ArchivoSubido archivo)
        {
            _context.ArchivosSubidos
                .Update(archivo);

            await Task.CompletedTask;
        }

        public async Task EliminarAsync(ArchivoSubido archivo)
        {
            _context.ArchivosSubidos
                .Remove(archivo);

            await Task.CompletedTask;
        }

        public async Task<ArchivoSubido?> ObtenerPorIdAsync(int idArchivo)
        {
            return await _context.ArchivosSubidos
                .Include(a => a.Usuario)
                .Include(a => a.AnalisisIA!)
                    .ThenInclude(ai => ai.Flashcards)
                .Include(a => a.AnalisisIA!)
                    .ThenInclude(ai => ai.Quizzes!)
                        .ThenInclude(q => q.PreguntasQuiz)
                .FirstOrDefaultAsync(a =>
                    a.IdArchivo == idArchivo);
        }

        public async Task<IEnumerable<ArchivoSubido>> ObtenerPorUsuarioAsync(int idUsuario)
        {
            return await _context.ArchivosSubidos
                .Where(a => a.IdUsuario == idUsuario)
                .OrderByDescending(a => a.FechaSubida)
                .ToListAsync();
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
