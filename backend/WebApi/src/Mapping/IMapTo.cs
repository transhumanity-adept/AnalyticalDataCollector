using AutoMapper;

namespace WebApi.Mapping;

public interface IMapTo<T>
{
    void Map(Profile profile)
    {
        profile.CreateMap(GetType(), typeof(T));
    }
}