using Dalamud.Game.ClientState.Objects;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace KnockKnock;

public class DalamudServices
{
    [PluginService] public static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
    [PluginService] public static ITextureProvider TextureProvider { get; private set; } = null!;
    [PluginService] public static ICommandManager CommandManager { get; private set; } = null!;
    [PluginService] public static IClientState ClientState { get; private set; } = null!;
    [PluginService] public static IContextMenu ContextMenu { get; set; } = null!;
    [PluginService] public static ITargetManager TargetManager { get; set; } = null!;
    [PluginService] public static IPluginLog Log { get; set; } = null!;
    [PluginService] public static IDataManager DataManager { get; set; } = null!;
    public static void Start(IDalamudPluginInterface plugin) => plugin.Create<DalamudServices>();
}
