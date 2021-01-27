using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerRegistry_UI
{
    internal sealed class AppSet
    {
        public static string RegistryUrl { get; internal set; }
        public static string RegistryCredentials { get; internal set; }
    }
}
