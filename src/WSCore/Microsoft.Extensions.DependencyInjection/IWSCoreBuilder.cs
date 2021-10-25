namespace Microsoft.Extensions.DependencyInjection
{
    public interface IWSCoreBuilder
    {
        IServiceCollection Services { get; }
    }
}