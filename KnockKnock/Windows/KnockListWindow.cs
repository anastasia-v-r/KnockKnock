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
    private byte[] NewPlayerString = new byte[20];

    // We give this window a constant ID using ###
    // This allows for labels being dynamic, like "{FPS Counter}fps###XYZ counter window",
    // and the window ID will always be "###XYZ counter window" for ImGui
    public KnockListWindow(Plugin plugin) : base("KnockKnock Player Whitelist")
    {
        Flags = ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar |
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
        var UserList = Configuration.WhiteList;
        if (UserList.Count > 0)
        {
            if (ImGui.ListBox("Player Names", ref UserListCurrentIndex, UserList.ToArray(), UserList.Count))
            {

            }
        }

        // Allow user to add a name manually
        if (ImGui.Button("##"))
        {
            UserList.Add(System.Text.Encoding.UTF8.GetString(NewPlayerString, 0, NewPlayerString.Length));
            Configuration.WhiteList = UserList;
            Configuration.Save();
        }
        ImGui.SameLine();
        if (ImGui.InputText("bingus", NewPlayerString, (uint)NewPlayerString.Length))
        {

        }
    }
}
