using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using KnockKnock;
using SamplePlugin;

namespace KnockKnock.Tabs;

public class ConfigTab : ITab
{
    readonly private Configuration Configuration;
    readonly private string Name;

    public ConfigTab(Plugin plugin)
    {
        Configuration = plugin.Configuration;
        Name = "ConfigTab";
    }

    public void Dispose() { }

    public void Draw()
    {
        if (ImGui.BeginTabItem(Name))
        {
            var UseWhiteList = Configuration.UseWhitelist;
            if (ImGui.Checkbox("Use whitelist", ref UseWhiteList))
            {
                Configuration.UseWhitelist = UseWhiteList;
                Configuration.Save();
            }

            var movable = Configuration.IsConfigWindowMovable;
            if (ImGui.Checkbox("Movable Config Window", ref movable))
            {
                Configuration.IsConfigWindowMovable = movable;
                Configuration.Save();
            }
        }
    }
}
