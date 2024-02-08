using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Samurai_V2_.Net_8.DbContexts;
using Samurai_V2_.Net_8.DbContexts.Models;
using Samurai_V2_.Net_8.DTOs;
using Samurai_V2_.Net_8.IRepository;
using System.Net;

namespace Samurai_V2_.Net_8.Repository
{
    public class BookRepo: IBookRepo
    {
        private BookContexts _dbContexts;

        public BookRepo(BookContexts dbContexts)
        {
            _dbContexts = dbContexts;
        }

        public async Task<string> CreateBook(BookDto book)
        {
            string messge = "";
            try
            {

                var data = await _dbContexts.TblBooks.Where(e => e.BookId == book.BookId).FirstOrDefaultAsync();
                if (data != null)
                {

                    var e = await _dbContexts.TblBooks.Where(e => e.BookTitle.ToLower().Trim() == book.BookTitle.ToLower().Trim()).FirstOrDefaultAsync();
                    if (e != null)
                    {
                        throw new Exception("File is Same");
                    }
                    data.BookTitle = book.BookTitle;
                    data.Author = book.Author;
                    data.Genre = book.Genre;
                    data.Price = book.Price;
                    _dbContexts.TblBooks.Update(data);
                    messge = "Updated";
                }
                else
                {
                    //var e = await _dbContexts.TblBook.Where(e => e.BookTitle.ToLower().Trim() == book.BookTitle.ToLower().Trim()).FirstOrDefaultAsync();
                    var e = await _dbContexts.TblBooks.Where(e => e.BookTitle.ToLower().Trim() == book.BookTitle.ToLower().Trim()).FirstOrDefaultAsync();
                    if (e != null)
                    {
                        throw new Exception("File is Same");
                    }
                    TblBook b = new TblBook();
                    b.BookTitle = book.BookTitle;
                    b.Author = book.Author;
                    b.Genre = book.Genre;
                    b.Price = book.Price;
                    await _dbContexts.TblBooks.AddAsync(b);
                    messge = "Created";
                }
                await _dbContexts.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw e;
            }
            return messge;
        }

        public async Task<BookDto> updateBooks(int Id, BookDto book)
        {
            string messge = "";
            try
            {

                var data = await _dbContexts.TblBooks.Where(e => e.BookId == Id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return null;
                }
                if (data != null)
                {

                    var e = await _dbContexts.TblBooks.Where(e => e.BookTitle.ToLower().Trim() == book.BookTitle.ToLower().Trim()).FirstOrDefaultAsync();
                    //if (e != null)
                    //{
                    //    throw new Exception("File is Same");
                    //}
                    data.BookTitle = book.BookTitle;
                    data.Author = book.Author;
                    data.Genre = book.Genre;
                    data.Price = book.Price;
                    _dbContexts.TblBooks.Update(data);
                    messge = "Updated";
                }
                
                     await _dbContexts.SaveChangesAsync();
                
                    var updatedData = await( from d in _dbContexts.TblBooks
                                             select new BookDto
                                             {
                                                 BookId = d.BookId,
                                                 BookTitle = d.BookTitle==null?"":d.BookTitle,
                                                 Author = d.Author == null ? "" : d.Author,
                                                 Genre = d.Genre == null ? "" : d.Genre,
                                                 Price = d.Price

                                             }).FirstOrDefaultAsync();
                    return updatedData;
                
                
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }
    }
}