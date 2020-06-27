using ICSharpCode.AvalonEdit.Highlighting;

namespace SmModStudio.Core
{

    public static class Constants
    {

        public const string DiscordClientId = "726303683150741505";
        
        public static readonly IHighlightingDefinition JsonSyntax = Utilities.ConvertXmlToSyntax(Utilities.RetrieveResourceData("SmModStudio.Resources.Syntax.Json.xml"));
        public static readonly IHighlightingDefinition LuaSyntax = Utilities.ConvertXmlToSyntax(Utilities.RetrieveResourceData("SmModStudio.Resources.Syntax.Lua.xml"));

    }

}