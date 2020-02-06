using JetBrains.Annotations;

namespace KMFService.Api
{
    public interface ILogger
    {
        void Log([NotNull] string message);
    }
}