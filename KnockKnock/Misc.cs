using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnockKnock
{
    static class Misc
    {
        readonly public static float TEXT_BASE_WIDTH = ImGui.CalcTextSize("A").X;
        readonly public static float TEXT_BASE_HEIGHT = ImGui.GetTextLineHeightWithSpacing();
    }
}
