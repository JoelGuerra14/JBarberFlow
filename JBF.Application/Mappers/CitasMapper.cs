using JBF.Application.Dtos;
using JBF.Application.DTOs;
using ReservaCitasBackend.Modelos;

namespace JBF.Application.Mappers
{
    public static class CitaMapper
    {
        public static CitaDto ToCitaDto(MCitas entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new CitaDto
            {
                ID_Citas = entity.ID_Citas,
                ID_Cliente = entity.ID_Cliente,
                ID_Estilista = entity.ID_Estilista,
                ID_Servicio = entity.ID_Servicio,
                FechaInicio = entity.FechaInicio,
                FechaFin = entity.FechaFin,
                IsCanceled = entity.IsCanceled
            };
        }

        public static List<CitaDto> ToCitasListDto(IEnumerable<MCitas> entities)
        {
            if (entities == null)
            {
                return new List<CitaDto>();
            }

            return entities.Select(ToCitaDto).ToList();
        }

        public static MCitas ToCitasEntity(CreateCitaDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new MCitas
            {
                ID_Cliente = dto.ID_Cliente,
                ID_Estilista = dto.ID_Estilista,
                ID_Servicio = dto.ID_Servicio,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin
            };
        }
        public static MCitas ToCitasEntity(UpdateCitaDto dto)
        {
            if (dto == null)
            {
                return null!;
            }

            return new MCitas
            {
                ID_Citas = dto.ID_Citas,
                ID_Cliente = dto.ID_Cliente,
                ID_Estilista = dto.ID_Estilista,
                ID_Servicio = dto.ID_Servicio,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin
            };
        }
    }
}