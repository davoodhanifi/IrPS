using System;

namespace IrpsApi.Api.Exceptions
{
    public class ConfigurationRequiredException : Exception
    {
        public ConfigurationRequiredException(string key) : base($"configuration by key: {key} is required")
        {
        }
    }
}