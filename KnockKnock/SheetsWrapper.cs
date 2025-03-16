using SamplePlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lumina.Excel;
using Lumina.Excel.Sheets;

namespace KnockKnock;

public static class SheetsWrapper
{
    private static ExcelSheet<World>? worldSheet = DalamudServices.DataManager.GetExcelSheet<World>()!;

    public static string? GetWorldFromId(uint worldId)
    {
        if (worldSheet.TryGetRow(worldId, out var world))
        {
            return world.Name.ExtractText();
        }

        return "A Foreign Land";
    }
}
