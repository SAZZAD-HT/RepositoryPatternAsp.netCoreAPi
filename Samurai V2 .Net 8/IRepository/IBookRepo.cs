using Samurai_V2_.Net_8.DTOs;

namespace Samurai_V2_.Net_8.IRepository
{
    public interface IBookRepo
    {
        Task<string> CreateBook(BookDto book);
        Task<BookDto> updateBooks(int Id,BookDto book);
    }
}
