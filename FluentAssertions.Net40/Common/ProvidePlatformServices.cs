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

        public IConfigurationStore ConfigurationStore
        {
#if ANDROID || __IOS__
            get { return new NullConfigurationStore(); }
#else
            get { return new AppSettingsConfigurationStore(); }
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