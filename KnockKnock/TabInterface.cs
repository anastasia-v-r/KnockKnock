using SamplePlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnockKnock;

public interface ITab
{
    public void Dispose();
    public void Draw();
}
