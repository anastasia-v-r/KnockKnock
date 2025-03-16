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
            // Setup headers for play list table
            if (ImGui.BeginTable("Table", 4, ImGuiTableFlags.Borders))
            {
                // Setup headers for table
                DalamudServices.Log.Debug($"{Misc.TEXT_BASE_WIDTH}");
                ImGui.TableSetupColumn("Enable", ImGuiTableColumnFlags.WidthFixed, Misc.TEXT_BASE_WIDTH * 5);
                ImGui.TableSetupColumn("Player Name", ImGuiTableColumnFlags.WidthFixed, Misc.TEXT_BASE_WIDTH * 25);
                ImGui.TableSetupColumn("Homeworld", ImGuiTableColumnFlags.WidthFixed, Misc.TEXT_BASE_WIDTH * 15);
                ImGui.TableSetupColumn("Friend?", ImGuiTableColumnFlags.WidthFixed, Misc.TEXT_BASE_WIDTH * 5);
                //ImGui.PopItemWidth();
                ImGui.TableHeadersRow();


                ImGui.EndTable();
            }
            

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

                    ImGui.SameLine(Misc.TEXT_BASE_WIDTH * 5f);
                    var isEnabled = true;
                    if (ImGui.Checkbox("##Enable/Disable Player", ref isEnabled));

                    ImGui.SameLine(Misc.TEXT_BASE_WIDTH * (25f + 5f));
                    ImGui.Text(player.Value.Name);

                    ImGui.SameLine(Misc.TEXT_BASE_WIDTH * ((25f + 5f) + 15f));
                    ImGui.Text(player.Value.HomeWorld.ToString());

                    ImGui.SameLine(Misc.TEXT_BASE_WIDTH * (((25f + 5f) + 15f) + 5f));
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
