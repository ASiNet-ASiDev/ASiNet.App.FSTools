# Елемент управления ASiNet.FSTools.Controls.FileSystemEntriesList
## Предоставляет собой просмоторщик файлов.

На данный момент поодерживаются функции:
* Открытие файла по двойному щелчку (DoubleClick)
* Выделение как одного, так и нескольких файлов.

# Code
## Commands:
* OpenCommand Срабатывает при двойном щелчке(DoubleClick) по любому из файлов. Передаёт параметром файл по которому был совершон двойной щелчёк.
* SelectionChanged Срабатывает при изменении количества выделеных файлов. Передаёт параметром int, может быть отрицательным числом.

## Properties:
* ItemsSource (IEnumerable) Коллекция файлов.
* SelectedItems (IList) Выделенные файлы.
* IsSelectedItems (Boolean) true если хоть один элемент выделен, в противном случае false
