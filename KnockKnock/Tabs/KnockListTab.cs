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
                if (ImGui.BeginTable("Table", 4, ImGuiTableFlags.None))
            {
                // Setup headers for table
                DalamudServices.Log.Debug($"{Misc.TEXT_BASE_WIDTH}");
                ImGui.TableSetupColumn("Enable", ImGuiTableColumnFlags.WidthFixed, Misc.TEXT_BASE_WIDTH * 7);
                ImGui.TableSetupColumn("Player Name", ImGuiTableColumnFlags.WidthFixed, Misc.TEXT_BASE_WIDTH * 25);
                ImGui.TableSetupColumn("Homeworld", ImGuiTableColumnFlags.WidthFixed, Misc.TEXT_BASE_WIDTH * 15);
                ImGui.TableSetupColumn("Friend?", ImGuiTableColumnFlags.WidthFixed, Misc.TEXT_BASE_WIDTH * 5);
                //ImGui.PopItemWidth();
                ImGui.TableHeadersRow();

                // Display current list of players on whitelist
                var UserList = Configuration.StoredPlayers;
                if (UserList.Count > 0)
                {
                    // Run through existing player list and display their info as well as provide the ability to remove them
                    foreach (var player in UserList)
                    {
                        ImGui.TableNextRow();

                        // Adds button for removal from whitelist
                        ImGui.TableSetColumnIndex(0);
                        ImGui.SetNextItemWidth(Misc.TEXT_BASE_WIDTH * 2.5f);
                        if (ImGui.ArrowButton($"##RemoveNameButton {player.Key}", ImGuiDir.Left)) 
                        {
                            UserList.Remove(player.Key);
                            Configuration.StoredPlayers = UserList;
                            Configuration.Save();
                            break;
                        }
                        
                        // Allows user to toggle whitelisted user temporarily
                        ImGui.SameLine();
                        ImGui.SetNextItemWidth(Misc.TEXT_BASE_WIDTH * 2.5f);
                        var isEnabled = true;
                        if (ImGui.Checkbox("##Enable/Disable Player", ref isEnabled)) ;

                        // Displays the whitelisted players name
                        ImGui.TableSetColumnIndex(1);
                        ImGui.Text(player.Value.Name);

                        // Displays the whitelisted players homeworld
                        ImGui.TableSetColumnIndex(2);
                        ImGui.Text(player.Value.HomeWorld.ToString());

                        // Shows if whitelisted player is on the users friendlist
                        ImGui.TableSetColumnIndex(3);
                        ImGui.BeginDisabled();
                        var isFriend = player.Value.IsPlayerFriend;
                        if (ImGui.Checkbox("##isfriend", ref isFriend))
                        {

                        }
                        ImGui.EndDisabled();
                    }
                }

                ImGui.EndTable();
            }
            ImGui.EndTabItem();
        }
    }
}
