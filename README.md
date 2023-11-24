# Image Comparison with WPF and LiveCharts
This project involves the comparison of two or more images of varying sizes stored in memory. Statistical characteristics are computed for each image, and due to the different sizes, distinct features emerge. However, to effectively compare their shapes, it is necessary to standardize them.

## Technology Used
+ Framework: Windows Presentation Foundation (WPF)
+ Chart Library: LiveCharts
+ .Net8

## Task Overview
1. Data Preparation:
  + Load two or more images of different sizes into memory.
  + Compute statistical characteristics for each image.
2. Chart Construction:
  + Utilize LiveCharts library to build natural graphs for each image based on their statistical characteristics.
3. Scaling Charts:
  + Implement a functionality to scale the graphs using a Scale button.
  + Redraw charts based on identifying the maximum interval on one graph.
  + Transform points of other graphs proportionally according to the identified interval.
4. Comparison Visualization:
  + Provide a visual representation of the updated charts for effective shape comparison.

## Instructions for Use
+ Clone the repository and open the solution in Visual Studio.
+ Ensure that the LiveCharts library is correctly referenced.
+ Run the application
+ Download two different .jpg images into empty fields and push "Save"
+ Push "Process image" to build scaled chart
+ To start with other images push "Reset"
## Additional Notes
+ This project is implemented using WPF for the user interface and LiveCharts to create interactive and dynamic charts.
+ Feel free to customize and extend the functionality based on your specific requirements.
