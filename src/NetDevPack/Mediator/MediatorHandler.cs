using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;

namespace NetDevPack.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Task<ValidationResult> SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Task PublishEvent<T>(T @event) where T : Event
        {
            return _mediator.Publish(@event);
        }
    }
}
