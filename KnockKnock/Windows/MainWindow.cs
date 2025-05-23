using System;
using System.Numerics;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using Lumina.Excel.Sheets;

using KnockKnock;
using KnockKnock.Tabs;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SamplePlugin.Windows;

public class MainWindow : Window, IDisposable
{
    private string GoatImagePath;
    private Plugin Plugin;
    private List<ITab> TabList;

    // We give this window a hidden ID using ##
    // So that the user will see "My Amazing Window" as window title,
    // but for ImGui the ID is "My Amazing Window##With a hidden ID"
    public MainWindow(Plugin plugin, string goatImagePath)
        : base("KnockKnock", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(600, 500),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        GoatImagePath = goatImagePath;
        Plugin = plugin;
        TabList = new List<ITab> 
        { 
            new ConfigTab(plugin),
            new KnockListTab(plugin)
        };
    }

    public void Dispose() 
    {
        foreach (var tab in TabList)
        {
            tab.Dispose();
        }
    }

    public override void Draw()
    {
        if (ImGui.BeginTabBar("##MainTabBar"))
        {
            foreach (var tab in TabList)
            {
                tab.Draw();
            }
            if(ImGui.BeginTabItem("Housing"))
            {
                ImGui.EndTabItem();
            }
            ImGui.EndTabBar();
        }
    }
}
