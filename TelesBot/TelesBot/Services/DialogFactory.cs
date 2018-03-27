using Autofac;
using Microsoft.Bot.Builder.Internals.Fibers;
using System.Collections.Generic;
using System.Linq;
using TelesBot.Interfaces;

namespace TelesBot.Services
{
    public class DialogFactory : IDialogFactory
    {
        protected readonly IComponentContext Scope;

        //IComponentContext does not need to be registered as Autofac automatically provides it.
        public DialogFactory(IComponentContext scope)
        {
            SetField.NotNull(out this.Scope, nameof(scope), scope);
        }

        public T Create<T>()
        {
            return Scope.Resolve<T>();
        }

        public T Create<T>(IDictionary<string, object> parameters)
        {
            return Scope.Resolve<T>(parameters.Select(kv => new NamedParameter(kv.Key, kv.Value)));
        }
    }
}