# GeneralizedPG-SURF

_______________________________________________________________________
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

_______________________________________________________________________
