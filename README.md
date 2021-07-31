# GeneralizedPG-SURF


## How to Use


### Getting and Running the Program

To get the code, simply clone https://github.com/adidust4/GeneralizedPG-SURF.git.

![Inkedrun_code_LI](https://user-images.githubusercontent.com/56092297/127750848-66e02792-3ec0-4407-a31b-f33ac90e55f3.jpg)

In order to generate an altered environment, simply click the play button located on the top of the screen, as circled in light blue in the picture. If there is no camera set up or editing is desired, simply switch from *Game* view to *Scene* view using the navigation tabs near the top of the screen. 

### Setting up the Hierarchy


![hierarchy](https://user-images.githubusercontent.com/56092297/127750192-d27f1b04-bee7-448f-b3b1-50b9addde829.png)

By navigating to the hierarchy view, there are *Generate Parts*, *Generate Obstacles*, *Generate Lights*, *Parts*, *Obstacles*, *Lighting*, and *Environment Pieces*. Below covers the general function of each section in the hierarchy.

 - *Generate Parts* - An empty game object that holds the script for generating parts. By clicking on the game object in the hierarchy view, editable variables appear in the inspector view. 
 - *Generate Obstacles* - An empty game object that holds the script for generating obstacles. By clicking on the game object in the hierarchy view, editable variables appear in the inspector view. 
 - *Generate Lights* - An empty game object that holds the script for generating lighting. By clicking on the game object in the hierarchy view, editable variables appear in the inspector view. 
 - *Parts* - An empty game object that acts as a folder for all the parts used in the scene. These can be prefabs or simply game objects. 
 - *Obstacles* - An empty game object that acts as a folder for all the obstacles used in the scene. These can be prefabs or simply game objects.
 - *Lighting* - An empty game object that acts as a folder for all the inside lights used in the scene. These can be prefabs or simply game objects that contain both the light itself and the game object that contains the lights.
 - *Environment Pieces* - An empty game object that acts as a folder for all other pieces of the scene that do not effect part, obstacle, or light generation. The game objects contained in this folder represents the industrial setting that acts as the environment for the alterations. If desired, a camera might also be added to this folder.



### Using the Inspector for Generation


![parts1](https://user-images.githubusercontent.com/56092297/127750382-969e537f-329c-4c7b-a67d-68cdb6939e42.png)

By clicking on *Generate Parts*, *Generate Obstacles*, and *Generate Lights* in the hierarchy view, a script editor will appear in the inspector view. Each editable row represents a variable to be set within the script. For each game object added under "Part Object", a variable must be added to each of the other list-type variables. For example, if there are four parts, there must also be four **minX**'s. An overview of the different variables is provided below:

*Generate Parts* & *Generate Obstacles*
- **MinX**, **MinY**, and **MinZ** - the minimum x, y, and z values that the specified part or obstacle can exist within. 
- **MaxX**, **MaxY**, and **MaxZ** - the maximum x, y, and z values that the specified part or obstacle can exist within.
- **Typical Num Of Parts** - The initial value for the frequency of each part or obstacle.
- **Max Frequency Variation** - The maximum variation from the initial frequency for each part or obstacle. 
- **Frequency Score** - The alteration level, where 0 <= L <= 1 for part or obstacle frequency. 
- **Typical Spacing** - The initial value for the average spacing of each part or obstacle.
- **Max Spacing Variation** - The maximum variation from the initial spacing for each part or obstacle. 
- **Spacing Score** - The alteration level, where 0 <= L <= 1 for part or obstacle spacing.
- **Max Rotation** - The maximum rotation from the initial orientation for each part or obstacle. 
- **Orientation Score** - The alteration level, where 0 <= L <= 1 for part or obstacle orientation.  
- **Part Object** - A list containing each part or obstacle to be altered/generated.

*Generate Lights*
- **Lights** - A list of each inside light game object (containing both the object itself and it's light source). 
- **Typical Intensity** - A number between 0 and 8 which represents the initial intensity of each inside light. 
- **Max Intensity Variation** - A number between 0 and 8 which represents the maximum variation from the initial intensity.
- **Typical Light Color** - The initial color value of each inside light.  
- **Intensity Score** - The alteration level, where 0 <= L <= 1 for inside light intensity.
- **Sun Score** - The alteration level, where 0 <= L <= 1 for outside simulated light position (where the initial position is directly above).
- **Color Score** - The alteration level, where 0 <= L <= 1 for inside light color.


## Script Descriptions

Within the *Assets* -> *Scripts* Folder, there are two .cs scripts and their .meta counterparts that come with the program. Each method contains a commented line that allows for printed method testing. Simply uncomment to run the test. 

### Light Generation

This file contains 4 methods and 7 class variables. The 7 variables are public, and described within the **Using the Inspector for Generation** section of this README, under *Generate Lights*. A description of each of these methods are below. 

#### setLightStrength()
This method sets the inside light intensity somewhere between typical intensity +/- (score*max variation)

#### setLightColor()
This method sets inside light color somewhere between typical color +/- (score*max variation) for each r, g, b, and a value.

#### setSunLight()
This method creates a simulated sun, and changes its position somewhere between mid-day (directly above) +/- (score*max variation)

#### Start()
This method calls **setLightStrength**, **setLightColor**, and **setSunLight** at the start of the scene so that lighting can be altered and then rendered. 


### Part Generation

This file contains 7 methods, 15 public class variables, and 9 private class variables. The 15 public variables are described within the **Using the Inspector for Generation** section of this README, under *Generate Parts & Obstacles*. A description of each private variable and method are below. 

#### List - partWidth
A list of each part and obstacles widths. This is determined by taking maxX - minX.

#### List - partDepth
A list of each part and obstacles depths. This is determined by taking maxZ - minZ.

#### List - partHeight
A list of each part and obstacles heights. This is determined by taking maxY - minY.

#### List - frequency
A list of resulting frequencies for each part and obstacle object type.

#### List of List - positions
A list of positions for ever part and every object in the scene.

#### Int - numOfOrientations
The number of orientations for each part and obstacle object type.

#### List of List - orientations
A list of orientations for ever part and every object in the scene.

#### setFrequency()
This method sets the part or object frequency somewhere between typical frequency +/- (score*max variation)

#### setOrientations()
This method sets the part or obstacle orientations somewhere between typical orientation +/- (score*max variation)

#### createPositions(int i)
This method sets the starting positions of each part or obstacle from the bottom back left with typical spacing. *int i* is a variable which lists the index of the list of objects to vary.

#### varyPositions(int i, double maxVariance)
This method moves parts or obstacles to be changed within a random spacing somewhere between typical spacing +/- (score*max variation). *int i* is a variable which lists the index of the list of objects to vary and *double maxVariance* is the corresponding maximum alteration.

#### partsAreInPlace(Rigidbody [] parts, int partIndex)
This method checks that each part or obstacle is still within defined bounds, and if it isn't, it destroys said part or obstacle. *igidbody [] parts* is a variable contains a list of all parts and obstacles in the scene, and *int partIndex* is an index of which part or obstacle it needs to compare boundaries with.

#### setFinalPlacement()
This method simulates the effects of gravity manually to place and destroy before start of scene.

#### Awake()
This method calls all other methods at the start of the scene so that parts and obstacles can be altered, destroyed as needed, and then rendered.


 
 



