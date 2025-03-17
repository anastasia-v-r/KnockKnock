using SamplePlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lumina.Excel;
using Lumina.Excel.Sheets;

namespace KnockKnock;

// Create a wrapper for sheets access to simplify things a bit
public static class SheetsWrapper
{
    // Store needed sheets
    private static ExcelSheet<World>? worldSheet = DalamudServices.DataManager.GetExcelSheet<World>()!;


    // Allow user to be able to covert world id to a world name
    public static string? GetWorldFromId(uint worldId)
    {
        if (worldSheet.TryGetRow(worldId, out var world))
        {
            return world.Name.ExtractText();
        }

        return "A Foreign Land";
    }
}
