﻿using CQRS_Lite_Union_API.Application.Abstractions;
using CQRS_Lite_Union_API.Common.Response;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Lite_Union_API.Application.Utils
{
    public abstract class CommandHandlerBase<TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : IRequest<TResult>
    {
        protected IAppContext Context { get; private set; }

        public CommandHandlerBase(IAppContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            TResult response;
            try
            {
                Context.BeginTransaction();

                response = await HandleAsync(request, cancellationToken);

                Context.CommitTransaction();
            }
            catch (Exception ex)
            {
                // log exception
                Context.RollBackTransaction();
                throw;
            }

            return response;
        }

        protected abstract Task<TResult> HandleAsync(TCommand request, CancellationToken cancellationToken);
    }
}
