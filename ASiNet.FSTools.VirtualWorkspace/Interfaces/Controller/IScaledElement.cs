using System.Windows;

namespace ASiNet.FSTools.VirtualWorkspace.Interfaces.Controller;
public interface IScaledElement
{

    public double MaxZoom { get; set; }

    public double MinZoom { get; set; }

    public double Scale { get; set; }


    public bool IsMinimumZoom => Scale <= MinZoom;
    
    public bool IsMaximumZoom => Scale >= MaxZoom;

    public void ScaleElement(Point position, double delta);
}
