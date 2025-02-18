using System.Windows;
using ASiNet.FSTools.Controls.Interfaces;

namespace ASiNet.App.FSTools.Models;
public class PagesManager
{
    public IPageContent this[int index]
    {
        get => _pageContents[index];
    }

    private List<IPageContent> _pageContents = [];

    public void AddContent(IPageContent pageContent)
    {
        _pageContents.Add(pageContent);
    }

    public void RemoveContent(IPageContent pageContent)
    {
        _pageContents.Remove(pageContent);
    }

    public (FrameworkElement view, object? viewModel) Create(IPageContent pageContent)
    {
        var page = Activator.CreateInstance(pageContent.PageType) as FrameworkElement;
        var vm = Activator.CreateInstance(pageContent.ViewModelType);
        if (page is null)
            throw new Exception();
        page.DataContext = vm;
        return (page, vm);
    }


}
