using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASiNet.FSTools.VirtualWorkspace.Interfaces;
public interface IVirtualWorkspaceWindowViewModel
{
    public IVirtualWorkspaceAreaViewModel AreaViewModel { get; set; }
}
