using Domain.Entities;
using JBF.Application.DTOs;
using JBF.Application.Interfaces;
using JBF.Domain.Base;
using Microsoft.Extensions.Logging;
using ReservaCitasBackend.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Application.Services
{
    public class UsuarioService : IUsuario
    {
        private readonly IUsuariosRepository _repository;
        private readonly ILogger<UsuarioService> _logger;

        public UsuarioService(IUsuariosRepository repository, ILogger<UsuarioService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // Crear usuario
        async Task<OperationResult> IUsuario.Createasync(UsuarioDTO usuarioDTO)
        {
            _logger.LogInformation("Intentando crear perfil de usuario");

            try
            {
                if (usuarioDTO == null)
                    return OperationResult.Failure("Datos de usuario nulos");

                var usuario = new MUsers
                {
                    NombreUsuario = usuarioDTO.NombreUsuario,
                    Correo = usuarioDTO.Correo,
                    PasswordHash = usuarioDTO.Password,
                    TipoUsuario = "Usuario"
                };

                var resultado = await _repository.Createasync(usuario);

                if (!resultado.IsSuccess)
                {
                    _logger.LogError($"Error al crear usuario: {resultado.Message}");
                    return resultado;
                }

                _logger.LogInformation($"Perfil de usuario con nombre {usuario.NombreUsuario} creado");
                return OperationResult.Success("Perfil creado con éxito", MapToDTO(usuario));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al crear usuario: {ex.Message}");
                return OperationResult.Failure($"Ocurrió un error inesperado al crear usuario: {ex.Message}");
            }
        }

        // Actualizar usuario
        async Task<OperationResult> IUsuario.Updateasync(int id, UsuarioDTO usuarioDTO)
        {
            _logger.LogInformation($"Intentando actualizar usuario con id {id}");

            try
            {
                if (usuarioDTO == null)
                    return OperationResult.Failure("Datos para actualizar se detectaron como null");

                // Obtener entidad existente
                var verificar = await _repository.GetbyIdasync(id);
                if (!verificar.IsSuccess || verificar.Data == null)
                    return OperationResult.Failure($"Usuario con id {id} no encontrado");

                var usuario = verificar.Data as MUsers;
                if (usuario == null)
                    return OperationResult.Failure("No se pudo obtener la entidad para actualizar");

                // Actualizar propiedades
                usuario.NombreUsuario = usuarioDTO.NombreUsuario;
                usuario.Correo = usuarioDTO.Correo;
                usuario.PasswordHash = usuarioDTO.Password;

                var actualizar = await _repository.Updateasync(usuario);
                if (!actualizar.IsSuccess)
                {
                    _logger.LogError($"Error al actualizar usuario: {actualizar.Message}");
                    return actualizar;
                }

                _logger.LogInformation($"Usuario con id {id} actualizado");
                return OperationResult.Success("Usuario actualizado", MapToDTO(usuario));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al actualizar usuario con id {id}: {ex.Message}");
                return OperationResult.Failure($"Error inesperado al actualizar usuario: {ex.Message}");
            }
        }

        // Traer usuario por id
        async Task<OperationResult> IUsuario.GetbyIdasync(int id)
        {
            _logger.LogInformation($"Intentando recuperar usuario con id {id}");

            try
            {
                var resultado = await _repository.GetbyIdasync(id);
                if (!resultado.IsSuccess || resultado.Data == null)
                    return OperationResult.Failure($"Usuario con id {id} no encontrado");

                var usuario = resultado.Data as MUsers;
                return OperationResult.Success("Usuario recuperado con éxito", MapToDTO(usuario));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al recuperar usuario: {ex.Message}");
                return OperationResult.Failure($"Error inesperado al recuperar usuario: {ex.Message}");
            }
        }

        // Traer todos los usuarios
        async Task<OperationResult> IUsuario.GetAllasync()
        {
            _logger.LogInformation("Intentando recuperar todos los usuarios");

            try
            {
                var resultado = await _repository.GetAllasync();
                if (!resultado.IsSuccess)
                    return resultado;

                var usuarios = resultado.Data as IEnumerable<MUsers>;
                var listaDTO = usuarios.Select(MapToDTO).ToList();

                return OperationResult.Success("Usuarios recuperados con éxito", listaDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al recuperar todos los usuarios: {ex.Message}");
                return OperationResult.Failure($"Error inesperado al recuperar todos los usuarios: {ex.Message}");
            }
        }

        // Mapear entidad a DTO
        private UsuarioDTO MapToDTO(MUsers usuario)
        {
            if (usuario == null) return null;

            return new UsuarioDTO
            {
                NombreUsuario = usuario.NombreUsuario,
                Correo = usuario.Correo,
                Password = usuario.PasswordHash
            };
        }
    }
}
