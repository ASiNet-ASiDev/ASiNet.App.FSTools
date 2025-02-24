using System.Diagnostics;
using System.Windows;
using ASiNet.FSTools.VirtualWorkspace.Interfaces.Controller;

namespace ASiNet.FSTools.VirtualWorkspace;
public class AreaController
{

    private IMovementElement? _movementElement;
    private IResizedElement? _resizedElement;
    private IScaledElement? _scaledElement;


    private Point _oldMovementPosition;
    private Point _oldResizedPosition;

    private bool _isMovement;
    private bool _isScaled;

    private bool _isConsiderScale;

    public bool StartMovement(Point position, IMovementElement element, bool considerScale = true)
    {
        if (_movementElement is not null && _isMovement)
        {
#if DEBUG
            Debug.WriteLine($"CALLED START MOVEMENT: FALSE");
#endif
            return false;
        }
#if DEBUG
        Debug.WriteLine($"CALLED START MOVEMENT: TRUE");
#endif
        _movementElement = element;
        _oldMovementPosition = position;
        _isConsiderScale = considerScale;
        _isMovement = true;
        return true;
    }

    public void InvokeMovement(Point position, double scale)
    {
        if (!_isMovement)
            return;
        var offset = _isConsiderScale ? (_oldMovementPosition - position) / scale : _oldMovementPosition - position;
        _oldMovementPosition = position;
#if DEBUG
        Debug.WriteLine($"CALLED MOVEMENT: {offset}");
#endif
        _movementElement?.MoveElement(offset, scale);
    }

    public void EndMovement()
    {
#if DEBUG
        Debug.WriteLine($"CALLED END MOVEMENT.");
#endif
        _isMovement = false;
        _movementElement = null;
        _oldMovementPosition = new Point(0, 0);
    }

    public bool StartScaleElement(IScaledElement element)
    {
        if(_scaledElement is not null && _isScaled)
            return false;
        _scaledElement = element;
        _isScaled = true;
        
        return true;
    }

    public void InvokeScale(Point position, double delta)
    {
        if (!_isScaled)
            return;
        if (delta > 1 && _scaledElement!.IsMaximumZoom)
            return;
        if (delta < 1 && _scaledElement!.IsMinimumZoom)
            return;
        _scaledElement?.ScaleElement(position, delta);
    }

    public void EndScaleElement()
    {
        _isScaled = false;
        _scaledElement = null;
    }

    public bool EndScaleElement(IScaledElement element)
    {
        if(_isScaled && _scaledElement == element)
        {
            _isScaled = false;
            _scaledElement = null;
            return true;
        }
        return false;
    }

    public bool StartResizeElement(IResizedElement element)
    {
        return false;
    }

    public bool ContainsMovementElement() =>
        _movementElement is not null;

    public bool ContainsCurrentMovementElement(IMovementElement element) =>
        _movementElement is not null && element == _movementElement;
}
