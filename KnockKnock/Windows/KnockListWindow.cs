using System;
using System.Linq;
using System.Numerics;
using Dalamud.Interface.Windowing;
using FFXIVClientStructs.FFXIV.Common.Lua;
using ImGuiNET;

namespace SamplePlugin.Windows;

public class KnockListWindow : Window, IDisposable
{
    private Configuration Configuration;

    private Int32 UserListCurrentIndex = 0;

    // We give this window a constant ID using ###
    // This allows for labels being dynamic, like "{FPS Counter}fps###XYZ counter window",
    // and the window ID will always be "###XYZ counter window" for ImGui
    public KnockListWindow(Plugin plugin) : base("KnockKnock Player Whitelist")
    {
        Flags = ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoScrollbar |
                ImGuiWindowFlags.NoScrollWithMouse;

        Size = new Vector2(500, 600);
        SizeCondition = ImGuiCond.Always;

        Configuration = plugin.Configuration;
    }

    public void Dispose() { }

    public override void PreDraw()
    {
        // Flags must be added or removed before Draw() is being called, or they won't apply
        if (Configuration.IsConfigWindowMovable)
        {
            Flags &= ~ImGuiWindowFlags.NoMove;
        }
        else
        {
            Flags |= ImGuiWindowFlags.NoMove;
        }
    }

   

    public override void Draw()
    {

        // Display current list of players on whitelist
        var UserList = Configuration.StoredPlayers;
        if (UserList.Count > 0)
        {
            foreach (var player in UserList)
            {
                if(ImGui.ArrowButton($"##RemoveNameButton {player.Key}", ImGuiDir.Left))
                {
                    UserList.Remove(player.Key);
                    Configuration.StoredPlayers = UserList;
                    Configuration.Save();
                    break;
                }
                ImGui.SameLine();
                ImGui.PushItemWidth(20);
                ImGui.Text(player.Value.Name);
                ImGui.PopItemWidth();
            }
        }
    }
}
