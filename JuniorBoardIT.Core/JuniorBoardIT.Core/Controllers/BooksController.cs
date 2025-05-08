using Microsoft.AspNetCore.Mvc;
using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.Models.ViewModels.BooksViewModels;
using JuniorBoardIT.Core.CQRS.Resources.Books.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Books.Queries;
using JuniorBoardIT.Core.Models.Enums;

namespace JuniorBoardIT.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public BooksController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpGet]
        [Route("GetBook")]
        public BookViewModel GetBook(Guid bgid)
            => dispatcher.DispatchQuery<GetBookQuery, BookViewModel>(new GetBookQuery() { BGID = bgid });

        [HttpGet]
        [Route("GetBooks")]
        public BooksListViewModel GetBooks(int skip, int take, GenreEnum genre, Guid agid, Guid pgid)
            => dispatcher.DispatchQuery<GetBooksQuery, BooksListViewModel>(new GetBooksQuery() { Skip = skip, Take = take, Genre = genre, AGID = agid, PGID = pgid });

        [HttpPost]
        [Route("AddBook")]
        public void AddBook(BookViewModel model)
            => dispatcher.DispatchCommand(new AddBookCommand() { Model = model });

        [HttpPut]
        [Route("UpdateBook")]
        public void UpdateBook(BookViewModel model)
            => dispatcher.DispatchCommand(new UpdateBookCommand() { Model = model });

        [HttpDelete]
        [Route("DeleteBook/{bGID}")]
        public void Delete(Guid bGID)
             => dispatcher.DispatchCommand(new DeleteBookCommand() { BGID = bGID });
    }
}