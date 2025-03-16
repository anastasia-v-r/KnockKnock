using System;
using System.Linq;
using System.Numerics;
using Dalamud.Interface.Windowing;
using FFXIVClientStructs.FFXIV.Common.Lua;
using ImGuiNET;
using SamplePlugin;

namespace KnockKnock.Tabs;

public class KnockListTab : ITab
{
    private Configuration Configuration;
    private string Name;

    public KnockListTab(Plugin plugin)
    {
        Configuration = plugin.Configuration;
        Name = "KnockList";
    }

    public void Dispose() { }

    public void Draw()
    {
        if (ImGui.BeginTabItem(Name))
        {
            // Display current list of players on whitelist
            var UserList = Configuration.StoredPlayers;
            if (UserList.Count > 0)
            {
                foreach (var player in UserList)
                {
                    if (ImGui.ArrowButton($"##RemoveNameButton {player.Key}", ImGuiDir.Left))
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
                    ImGui.SameLine();
                    ImGui.PushItemWidth(20);
                    ImGui.Text(player.Value.HomeWorld.ToString());
                    ImGui.PopItemWidth();
                    ImGui.SameLine();
                    ImGui.BeginDisabled();
                    var isFriend = player.Value.IsPlayerFriend;
                    if (ImGui.Checkbox("##isfriend", ref isFriend))
                    {

                    }
                    ImGui.EndDisabled();
                }
            }
            ImGui.EndTabItem();
        }
    }
}
