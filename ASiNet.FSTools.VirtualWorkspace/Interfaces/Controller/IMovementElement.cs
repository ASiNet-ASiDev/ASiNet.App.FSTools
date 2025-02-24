using System.Windows;
using System.Windows.Threading;

namespace ASiNet.FSTools.VirtualWorkspace.Interfaces.Controller;
public interface IMovementElement
{
    public void MoveElement(Vector offset, double scale);
}
