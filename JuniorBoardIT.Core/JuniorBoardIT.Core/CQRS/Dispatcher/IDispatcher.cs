using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;

namespace JuniorBoardIT.Core.CQRS.Dispatcher
{
    public interface IDispatcher
    {
        TResponse DispatchQuery<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;
        void DispatchCommand<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
