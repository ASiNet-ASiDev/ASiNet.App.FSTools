using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASiNet.FSTools.Controls.Enums;

public enum PagesViewSplitMode
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
