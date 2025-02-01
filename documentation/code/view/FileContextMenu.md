# Элемент управления ASiNet.FSTools.Controls.FilesContextMenu
## Предоставляет собой контекстное меню, содержащее в себе многие функции по взаимодействию с файлами.

На данный момент поодерживаются функции:
* Выделить все файлы.
* Снять выделение.
* Открыть все выделенные файлы.
* Переименовать все выделенные файлы.
* Переместить все выделенные файлы.
* Скопировать все выделеные файлы.
* Удалить все выделеные файлы.
* Вернутся к родительской/корневой директори.

_Можно вызвать щелчком ПКМ по файлам или же заднему фону._  
_Большенсво функций контекстного меню по управлению файлами становятся неактивными, если не выделен ни 1 файл!_

# Code

## Commands:
* OpenFileCommand
* RenameFileCommand
* MoveFileCommand
* CopyFileCommand
* DeleteFileCommand
* SelectAllCommand
* UnselectAllCommand
* BackToParentCommand

> Каждая из команд соответствует пункту контекстного меню с соответсвующим названием.

## Properties
* FileSpecificMenuOptionsIsEnabled (Boolean) _Данное свойство отвечает за активность, или же неактивность пунктов меню связанных с прямым взаимодействием с файлами._

## Events
* OpenFile (Action)
* RenameFile (Action)
* MoveFile (Action)
* CopyFile (Action)
* DeleteFile (Action)
* SelectAllFiles (Action)
* UnselectAllFiles (Action)
* BackToParentFile (Action)

> Каждое из событий соответствует пункту контекстного меню с соответсвующим названием.
