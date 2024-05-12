# Visual_human_machine
![header](misc/visual_prog.png)

A short course on visual programming and human-machine interaction of SIBGUTI

Dev by IP-214 Nomokonov Danil

![Static Badge](https://img.shields.io/badge/github-262722?style=for-the-badge&logo=github) ![Static Badge](https://img.shields.io/badge/C%23-2F9CD2?style=for-the-badge&logo=csharp) ![Static Badge](https://img.shields.io/badge/AVALONIA-B321D6?style=for-the-badge&logo=framework)

## [Homework #1. Class diagrams](Homework-num-1-Class-diagrams/)
Implement a class diagram in the C# programming language and write a program demonstrating the work of classes from the diagram.

## [Homework #2. Application with a graphical interface](Homework-2-AvaloniaColor/)
Implement an application with a graphical interface consisting of a single window. The window should contain ten buttons that occupy the left third of the window, and a colored rectangle that occupies the right two thirds of the window. Certain colors are assigned to the buttons and when you click on the button, the rectangle is colored in the color assigned to the button. The names of the colors in which the rectangle is painted should be written on the buttons.

## [Homework #3. The binding mechanism](Homework-3-CalcApp/)
Implement a calculator in the C# programming language using the Avalonia framework.

## [Homework #4. Displaying collections](Homework-4-Explorer/)
Implement an application to view the file system. The application window should display a list of files and directories in the working directory.

## [Homework #5. Asynchrony in C#](Homework-5-ImprovedFileExplorer/)
Implement an application for viewing images based on homework No. 4.</br>
1. Implementation of asynchronous loading of the file system tree</br>
2. Implementation of displaying file system changes when it is changed outside the application
(you can use the FileSystemWatcher class)

## [Homework #6. WeatherApp](Homework-6-WeatherApp/)
Applicationem ad effectum deducendi videre tempestas praenuntientur aliquot diebus ante. Ut notitia, Uti OpenWeather officium.

## [Homework #7. Reactive programming](Homework-7-Reactive/)
Implement a factory that accepts an ObservableCollection object and returns an IObservable<NotifyCollectionChangedEventArgs> — a list of event arguments Inotifycollectionchanged for the transferred collection. Write a console program demonstrating the operation of the factory. In the program, it is necessary to pass as a subscriber a method that logs data about changes to a file.

## [Homework #8. MVVM Architectural Template](Homework-8-MVVM/)
Implement a graphical application that, when launched, receives information about users at https://jsonplaceholder.typicode.com/users and displays them on the screen. To implement two views for output - flat output (using the DataGrid element) and a hierarchical tree (using TreeView), the roots of the elements must be the user id.

## [Homework #9. Custom Controls №1](Homework_9_Custom/)
Create a custom control that will have two states — collapsed and full-size. When collapsed, the control is a square icon. In the expanded form, there is a scale next to the icon (you can do it based on the Slider element). Switching between views should take place by clicking on the icon. The control must have properties that determine the shape of the control, the icon, and the maximum and minimum scale values.

## [Homework #10. Custom Controls №2](Homework_10_ColorPicker)
Create a Palette control. The element must include all the necessary properties: the selected color, size, etc.

## [Homework #11. Custom Controls №3](Homework_11_CustomTable)
Create a control that displays the properties of the passed object (the object can beof any type!) in the form of a hierarchy. If the property has a base type (int, double, string, etc.), the pair "Attribute name":"Attribute value" is displayed. If the property does not have a base type, it is displayed as a drop-down list with a title in which the name of the property is written, and the contents are displayed in the body.

## [Homework #12. ](Homework_12_Valves)
Implement a library of gate controls for logic circuits. The controls must support the following properties: input values (collection), output value, display standard (GOST or ANSI), label (if present, drawn under the valve), font for the label, whether the element is highlighted or not (if highlighted, it should be visually displayed)

## [Homework #13-14. ](Homework_LogicalApp)
- [x] Add elements for input and output values to the library from homework No. 12. The input elements generate 0 or 1 and pass it to the output (implement the property for the output like the rest of the valves), the state is switched by left mouse click. The output element shows the value that goes to its input (implemented through a property).
- [x] Add a connector control that allows you to connect the input and output. The connector must have properties for the input and output values. Visually depict the connector as a line.
- [x] implement the choice of the bit depth of the input, output elements and connectors. Each bit of the input element can be switched by mouse click
- [x] Finalize the application from homework No. 13 by adding to it saving the drawn diagram to the SQLite database and loading the diagram from the database file. The database file acts as a project file, i.e. each schema is stored in a separate database file