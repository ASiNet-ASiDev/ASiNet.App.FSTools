using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASiNet.FSTools.Controls.Interfaces;
public interface IPageContent
{
    public string Title { get; }

    public string Description { get; }

    public Type ViewModelType { get; }

    public Type PageType { get; }
}