# Unity-UML-Generator
Tool to generate UML diagram for Unity/C# projects. Uses Reflection API to inspect the classes, fields, methods, and constructors at runtime.
Currently only supports class diagrams.

## Class Diagram -
![ScreenShot](https://github.com/kewalkishang/Unity-UML-Generator/blob/main/UMLDiagram7-12-2023_11:24:00_PM.png)

## How to use -
1. Import UMLDiagram.unitypackage.
2. Open the UMLDiagram scene.
3. In the scene,in the UMLDiagram script on the UML GameObject.
4. Keep ProjectUML checked if you want just the classes in the asset folder to be included in the UML.
5. Else, uncheck it and add the name of the StartClass. 
6. Hit run. Your class diagram should be generated.
7. Play around with the canvas(World Space) rect to ensure all your classes are in view.
8. The panels can be dragged around in the game window.
9. Then screenshot it once you are ready. Window/UMLDiagram/Take ScreenShot.
10. Your diagram(PNG) will be saved in the root folder of your project.

## Known issue -
1. **Association isn't always right.**(Doesn't find association from inside the methods since reflection cant access it.)
2. Generates class diagram for unity classes(Might not be needed).
3. Doesnt support creating diagram for the whole project.

## Future Features -
- [ ] Enable users to delete and add classes during runtime.
- [ ] Add more UML Diagrams like activity, use-case diagram.
- [ ] Highlight insights from Diagrams.
- [ ] Make canvas accomodate all classes on automatically.
- [ ] spread out class diagrams better.
- [ ] Give user flexibility to include/exclude namespaces for class generation.
- [x] Generate Diagram using the root folder, rather than a class.
