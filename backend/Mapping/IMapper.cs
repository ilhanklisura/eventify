namespace Eventify.Backend.Mapping;

/// <summary>Mapiranje entiteta / DTO-a (Entity → Response model).</summary>
public interface IMapper<TFrom, TTo>
{
    TTo Map(TFrom value);
}
