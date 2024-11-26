using AutoMapper;
using Library.Controllers;
using Library.Repositories.Context;

namespace Library;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<BookEntity, BookDTO>();
    }
}