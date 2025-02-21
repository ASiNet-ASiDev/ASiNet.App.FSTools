namespace ASiNet.FSTools.SplitWorkspace.Enums;
public enum SplitMode
{
    NoSplit,
    /// <summary>
    /// Вертикально, на 2 области.
    /// </summary>
    VerticalTwoAreas,
    /// <summary>
    /// Горизонтально, на 2 области.
    /// </summary>
    HorizontalTwoAreas,
    /// <summary>
    /// Разделение на 2 области сверху, 1 область с низу.
    /// </summary>
    HorizontalTwoAndVerticalButtonOneAreas,
    /// <summary>
    /// Разделение на 2 области с низу, 1 область с верху.
    /// </summary>
    HorizontalTwoAndVerticalTopOneAreas
}
