using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions.Common;

namespace FluentAssertions
{
    class NullConfigurationStore : IConfigurationStore
    {
        public string GetSetting(string name)
        {
            return null;
        }
    }
}
