using System.Reflection;

namespace Infrastructure
{
    public class ApplicationAssemblyReference
    {
        public static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
    }
}
