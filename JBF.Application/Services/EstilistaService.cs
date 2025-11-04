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
    public class EstilistaService : IEstilista
    {
        private readonly IEstilistasRepository _repository;
        private readonly ILogger<EstilistaService> _logger;

        public EstilistaService(IEstilistasRepository repository, ILogger<EstilistaService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        //Crear
        async Task<OperationResult> IEstilista.Createasync(EstilistaDTO estilistaDTO)
        {
            _logger.LogInformation("Intentando crear perfil de estilista");

            try
            {
                if (estilistaDTO == null)
                {
                    return OperationResult.Failure($"Al intentar crear el perfil los datos se detectaron como null ");
                }
                else 
                {
                    var MEstilistas = new MEstilista
                    {
                        Nombre = estilistaDTO.NombreDTO,
                        Email = estilistaDTO.EmailDTO,
                        ID_Servicio = estilistaDTO.ID_ServicioDTO
                    };

                   var resultado = await _repository.Createasync(MEstilistas);

                    if (!resultado.IsSuccess)
                    {
                        _logger.LogError($"Error al crear perfil: {resultado.Message}");
                        return resultado;
                    }

                    var dtoCreado = MapeoDTO(resultado.Data as MEstilista);

                    _logger.LogInformation($"Perfil de estilista con nombre {dtoCreado.NombreDTO} creado");
                    return OperationResult.Success("Perfil creado con exito", dtoCreado);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al crear perfil de estilista: {ex.Message}");
                return OperationResult.Failure($"Ocurrio un error inesperado al crear perfil de estilista:{ex.Message}");
            }
        }

        //Actualizar
        async Task<OperationResult> IEstilista.Updateasync(int id, EstilistaDTO estilistaDTO)
        {
            try
            {
                _logger.LogInformation("Intentando actualizar perfil de estilista");

                if (estilistaDTO == null)
                {
                    return OperationResult.Failure("Datos para actualizar se detectaron como null");
                }

                var verificar = await _repository.GetbyIdasync(id);

                if (!verificar.IsSuccess)
                {
                    return OperationResult.Failure($"Estilista con id {id} no encontrado");
                }

                //Usa la instancia rastreada por el contexto
                var estilista = verificar.Data as MEstilista;

                if (estilista == null)
                    return OperationResult.Failure("No se pudo obtener la entidad para actualizar");

                //Modifica directamente sus propiedades
                estilista.Nombre = estilistaDTO.NombreDTO;
                estilista.Email = estilistaDTO.EmailDTO;
                estilista.ID_Servicio = estilistaDTO.ID_ServicioDTO;

                var actualizar = await _repository.Updateasync(estilista);

                if (!actualizar.IsSuccess) 
                { 
                    OperationResult.Failure($"Error al actualizar los datos: {actualizar.Message}");
                    return actualizar;
                }

                _logger.LogInformation($"Registro con id {id} actualizado");
                return OperationResult.Success("Registro actualizado", MapeoDTO(estilista));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al intentar actualizar registro con id {id}: {ex.Message}");
                return OperationResult.Failure($"Error inesperado al intentar actualizar registro: {ex.Message}");
            }
        }

        //Traer por id
        async Task<OperationResult> IEstilista.GetbyIdasync(int id)
        {
            try
            {
                _logger.LogInformation("Intentando recuperar perfil de estilista");

                var resultado = await _repository.GetbyIdasync(id);

                if (!resultado.IsSuccess)
                {
                    OperationResult.Failure($"No se encontro un perfil con el id {id}"); 
                    return resultado;
                }

                var dtoCreado = MapeoDTO(resultado.Data as MEstilista);

                if (dtoCreado == null)
                {
                    return OperationResult.Failure("No se pudo mapear el perfil del estilista");
                }

                return OperationResult.Success("Perfil recuperado con exito.", dtoCreado);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al recuperar perfil: {ex.Message}");
                return OperationResult.Failure($"Error inesperado al recuperar perfil: {ex.Message}");
            }
        }

        //Traer todo
        async Task<OperationResult> IEstilista.GetAllasync()
        {
            try
            {
                _logger.LogInformation("Intentando recuperar perfil de todos los estilista");

                var resultado = await _repository.GetAllasync();

                if (!resultado.IsSuccess)
                {
                    _logger.LogError($"Error al traer perfiles: {resultado.Message}");
                    return resultado;
                }

                var listaPerfiles = resultado.Data as IEnumerable<MEstilista>;

                var listadto = listaPerfiles.Select(MapeoDTO).ToList();

                return OperationResult.Success("Datos recuperados con exito", listadto);
            }
            catch (Exception ex) 
            { 
                _logger.LogError($"Error inesperado al recuperar todos los datos: {ex.Message}");
                return OperationResult.Failure($"Error inesperado al recuperar todos los datos: {ex.Message}");
            }
        }

        //Mapeo de datos
        private EstilistaDTO MapeoDTO(MEstilista mEstilista)
        {
            if (mEstilista == null)
            {
                return null;
            }

            return new EstilistaDTO
            {
                NombreDTO = mEstilista.Nombre,
                EmailDTO = mEstilista.Email,
                ID_ServicioDTO = mEstilista.ID_Servicio
            };
        }
    }
}
