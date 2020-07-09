using System;
using System.IO;
using ICSharpCode.AvalonEdit.Highlighting;

namespace SmModStudio.Core
{

    public static class Constants
    {

        public const string DiscordClientId = "726303683150741505";
        public const string AppCenterAppSecret = "6c038214-26f7-4fd7-8f6f-95d125c23dcb";

        public static readonly string GameUserPath =
            Utilities.GetGameUserPath();
        public static readonly IHighlightingDefinition JsonSyntax =
            Utilities.ConvertXmlToSyntax(Utilities.RetrieveResourceData("SmModStudio.Resources.Syntax.Json.xml"));
        public static readonly IHighlightingDefinition LuaSyntax =
            Utilities.ConvertXmlToSyntax(Utilities.RetrieveResourceData("SmModStudio.Resources.Syntax.Lua.xml"));

        public static readonly string[] AccentNames =
        {
            "Red", "Green", "Blue", "Purple", "Orange",
            "Lime", "Emerald", "Teal", "Cyan", "Cobalt",
            "Indigo", "Violet", "Pink", "Magenta", "Crimson",
            "Amber", "Yellow", "Brown", "Olive", "Steel",
            "Mauve", "Taupe", "Sienna"
        };

    }

}