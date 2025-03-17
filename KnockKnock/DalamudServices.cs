using Dalamud.Game.ClientState.Objects;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace KnockKnock;

// Setup and provide clean condensed access throughout the plogon to dalamud's various services
public class DalamudServices
{
    [PluginService] public static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
    [PluginService] public static ITextureProvider TextureProvider { get; private set; } = null!;
    [PluginService] public static ICommandManager CommandManager { get; private set; } = null!;
    [PluginService] public static IClientState ClientState { get; private set; } = null!;
    [PluginService] public static IContextMenu ContextMenu { get; set; } = null!; // Allows for context menu integraton (like when right clicking a player)
    [PluginService] public static ITargetManager TargetManager { get; set; } = null!;
    [PluginService] public static IPluginLog Log { get; set; } = null!; // Logging service for debugging
    [PluginService] public static IDataManager DataManager { get; set; } = null!; // Provides access to the sheets system for converting id's to names such as world name or the names of gear
    public static void Start(IDalamudPluginInterface plugin) => plugin.Create<DalamudServices>(); // Provide way to spool up the various services
}
