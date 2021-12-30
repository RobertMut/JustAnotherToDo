using IdentityServer4.Services;
using MediatR;

namespace JustAnotherToDo.Application.System.Queries
{
    public class GetErrorQueryHandler : IRequestHandler<GetErrorQuery, ErrorVm>
    {
        private readonly IIdentityServerInteractionService _interaction;

        public GetErrorQueryHandler(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

        public async Task<ErrorVm> Handle(GetErrorQuery request, CancellationToken cancellationToken)
        {
            var vm = new ErrorVm();
            var message = await _interaction.GetErrorContextAsync(request.ErrorId);
            if (message != null)
            {
                vm.Error = message;
            }

            return vm;
        }
    }
}
