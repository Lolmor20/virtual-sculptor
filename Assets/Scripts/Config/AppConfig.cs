using System.Configuration;
using System.Collections.Immutable;

namespace Assets.Scripts.Config
{
    public static class AppConfig
    {
        public static readonly ImmutableArray<string> SkyboxNames = ImmutableArray.Create(ConfigurationManager.AppSettings["SkyboxNames"].Split(','));
        public static readonly ImmutableArray<string> SkyboxMaterials = ImmutableArray.Create(ConfigurationManager.AppSettings["SkyboxMaterials"].Split(','));
    }
}