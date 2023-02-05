using AutoMapper;

namespace WebApi.Mapping;

public static class MapperExtensions
{
    public static T? MapFromPartials<T, V, K>(this IMapper mapper, V partial1, K partial2)
    {
        var result = mapper.Map<T>(partial1);
        if (result is null) return result;
        result = mapper.Map(partial2, result);
        return result;
    }
}