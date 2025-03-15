using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using SamplePlugin.Windows;
using KnockKnock;
using Dalamud.Game.Gui.ContextMenu;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.Text;
using FFXIVClientStructs.FFXIV.Client.Game.Character;
using System.Collections.Generic;
using FFXIVClientStructs.FFXIV.Common.Lua;
using System.Linq;

namespace SamplePlugin;

public sealed unsafe class Plugin : IDalamudPlugin
{
    [PluginService] internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
    [PluginService] internal static ITextureProvider TextureProvider { get; private set; } = null!;
    [PluginService] internal static ICommandManager CommandManager { get; private set; } = null!;
    [PluginService] internal static IClientState ClientState { get; private set; } = null!;
    [PluginService] internal static IDataManager DataManager { get; private set; } = null!;
    [PluginService] internal static IPluginLog Log { get; private set; } = null!;

    private const string CommandName = "/knockknock";
#if DEBUG
    private const string DevCommandName = "/kkd";
#endif

    public Configuration Configuration { get; init; }

    public readonly WindowSystem WindowSystem = new("SamplePlugin");

    private KnockListWindow KnockListWindow { get; init; }
    private ConfigWindow ConfigWindow { get; init; }
    private MainWindow MainWindow { get; init; }

    public Plugin(IDalamudPluginInterface dalamud)
    {
        // Dalamad Services section
        PluginHandlers.Start(dalamud);
        PluginHandlers.ContextMenu.OnMenuOpened += OnOpenMenu;

        Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();

        // you might normally want to embed resources and load them from the manifest stream
        var goatImagePath = Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "goat.png");

        KnockListWindow = new KnockListWindow(this);
        ConfigWindow = new ConfigWindow(this);
        MainWindow = new MainWindow(this, goatImagePath);

        WindowSystem.AddWindow(KnockListWindow);
        WindowSystem.AddWindow(ConfigWindow);
        WindowSystem.AddWindow(MainWindow);

        CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
        {
            HelpMessage = "A useful message to display in /xlhelp"
        });

#if DEBUG
        CommandManager.AddHandler(DevCommandName, new CommandInfo(OnCommand)
        {
            HelpMessage = "A useful message to display in /xlhelp"
        });
#endif
        PluginInterface.UiBuilder.Draw += DrawUI;

        // This adds a button to the plugin installer entry of this plugin which allows
        // to toggle the display status of the configuration ui
        PluginInterface.UiBuilder.OpenConfigUi += ToggleConfigUI;

        // Adds another button that is doing the same but for the main ui of the plugin
        PluginInterface.UiBuilder.OpenMainUi += ToggleMainUI;

        // Add a simple message to the log with level set to information
        // Use /xllog to open the log window in-game
        // Example Output: 00:57:54.959 | INF | [SamplePlugin] ===A cool log message from Sample Plugin===
        Log.Information($"===A cool log message from {PluginInterface.Manifest.Name}===");
    }


    public void Dispose()
    {
        WindowSystem.RemoveAllWindows();

        KnockListWindow.Dispose();
        ConfigWindow.Dispose();
        MainWindow.Dispose();

        CommandManager.RemoveHandler(CommandName);
    }

    private void OnCommand(string command, string args)
    {
        // in response to the slash command, just toggle the display status of our main ui
        ToggleMainUI();
    }

    private void OnOpenMenu(IMenuOpenedArgs args)
    {
        // Store local copy of White list for ez access and speed
        var AllowedContentIds = Configuration.StoredPlayers.Keys.ToList();

        // ???
        PluginHandlers.PluginLog.Info($"AddonName: {args.AddonName}");
        if (args.AddonName != null) return;

        // Filter for only players when right clicking on entities
        IGameObject? target = PluginHandlers.TargetManager.Target;
        if (target is not IPlayerCharacter pCharacter) return;

        // Locate entity data directly in memory, this is going underneath dalamud now, the name
        // is because this can be more than just players but we've already filtered down to player anywho
        BattleChara* bChara = (BattleChara*)pCharacter.Address;
        if (bChara == null) return;

        // Grab player characters unique ID
        ulong characterContentId = bChara->ContentId;

        // Check against existing id's, display different context based on this condition
        if (AllowedContentIds.Contains(characterContentId))
        {
            // add an item to the player context menu
            args.AddMenuItem(new MenuItem()
            {
                Name = "Remove Player from Whitelist",
                Prefix = SeIconChar.BoxedLetterK,
                PrefixColor = 0,
                OnClicked = (_) =>
                {
                    Configuration.StoredPlayers.Remove(characterContentId);
                    Configuration.Save();
                }
            });
        }
        else
        {
            // add an item to the player context menu
            args.AddMenuItem(new MenuItem()
            {
                Name = "Add Player To Whitelist",
                Prefix = SeIconChar.BoxedLetterK,
                PrefixColor = 0,
                OnClicked = (_) =>
                {
                    Configuration.StoredPlayers.Add(characterContentId, 
                        new PlayerDataStorageContainer(bChara->NameString, bChara->HomeWorld, bChara->IsFriend));
                    Configuration.Save();
                }
            });
        }
    }

    private void DrawUI() => WindowSystem.Draw();

    public void ToggleConfigUI() => ConfigWindow.Toggle();
    public void ToggleMainUI() => MainWindow.Toggle();
    public void ToggleKnockListUI() => KnockListWindow.Toggle();
}
