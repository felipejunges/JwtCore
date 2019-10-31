using System;
using JwtCore.Models;
using Microsoft.Extensions.Caching.Memory;

namespace JwtCore.Data
{
    public class UsuarioRepository
    {
        private readonly IMemoryCache _memoryCache;

        public UsuarioRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public UsuarioEntity Get(int id)
        {
            var usuario = _memoryCache.GetOrCreate($"Usuario_{id}", entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromHours(24));
                return new UsuarioEntity() { UsuarioId = id, Login = "felipe" };
            });

            return usuario;
        }

        public void Save(UsuarioEntity usuario)
        {
            _memoryCache.Set($"Usuario_{usuario.UsuarioId}", usuario);
        }
    }
}