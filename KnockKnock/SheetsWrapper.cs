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
    private static ExcelSheet<World>? worldSheet = DalamudServices.DataManager.GetExcelSheet<World>();

    public static string? GetWorldFromId(int id)
    {
        if (worldSheet == null) return null;
        foreach (World world in worldSheet)
        {
            if (world.RowId == id)
            {
                return world.Name.ExtractText();
            }
        }
        return null;
    }
}
