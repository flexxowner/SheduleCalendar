using System.Threading.Tasks;

namespace CalendarAppointments.Activation
{
    public abstract class ActivationHandler
    {
        public abstract bool CanHandle(object args);

        public abstract Task HandleAsync(object args);
    }

    public abstract class ActivationHandler<T> : ActivationHandler
        where T : class
    {
        public override async Task HandleAsync(object args)
        {
            await HandleInternalAsync(args as T);
        }

        public override bool CanHandle(object args)
        {
            return args is T && CanHandleInternal(args as T);
        }

        protected abstract Task HandleInternalAsync(T args);

        protected virtual bool CanHandleInternal(T args)
        {
            return true;
        }
    }
}
