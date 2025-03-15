using Dalamud.Game.ClientState.Objects;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace KnockKnock;

public class PluginHandlers
{
    [PluginService] public static IContextMenu ContextMenu { get; set; } = null!;
    [PluginService] public static ITargetManager TargetManager { get; set; } = null!;
    [PluginService] public static IPluginLog PluginLog { get; set; } = null!;
    public static void Start(IDalamudPluginInterface plugin) => plugin.Create<PluginHandlers>();
}
