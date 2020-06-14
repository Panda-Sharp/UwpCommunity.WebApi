using Microsoft.AspNetCore.Mvc;

namespace UwpCommunity.WebApi.Attributes
{
    public class DiscordRequirementAttribute : TypeFilterAttribute
    {
        public DiscordRequirementAttribute() : base(typeof(DiscordRequirementFilter))
        {
        }
    }
}
