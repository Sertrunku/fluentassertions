using System;

using FluentAssertions.Execution;
using FluentAssertions.Formatting;

namespace FluentAssertions.Common
{
    public class ProvidePlatformServices : IProvidePlatformServices
    {
        public Action<string> Throw
        {
            get { return TestFrameworkProvider.Throw; }
        }

        public Configuration Configuration
        {
#if !__IOS__ && !ANDROID
            get { return new Configuration(new AppSettingsConfigurationStore()); }
#else
            get { return null; }
#endif
        }

        public IReflector Reflector
        {
            get { return new DefaultReflector(); }
        }

        public IValueFormatter[] Formatters
        {
            get
            {
                return new IValueFormatter[]
                {
                    new AggregateExceptionValueFormatter(),
                    new XDocumentValueFormatter(),
                    new XElementValueFormatter(),
                    new XAttributeValueFormatter()
                };
            }
        }
    }
}