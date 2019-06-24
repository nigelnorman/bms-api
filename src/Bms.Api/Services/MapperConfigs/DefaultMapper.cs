using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bms.Api.Services.MapperConfigs
{
    public static class DefaultMapper
    {
        public static Action<IMapperConfigurationExpression> Config = (IMapperConfigurationExpression config) =>
        {
            // not sure what else should go here
            config.ValidateInlineMaps = false;
        };
    }
}
