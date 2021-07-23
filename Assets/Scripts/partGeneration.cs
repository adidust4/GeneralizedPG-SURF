///-----------------------------------------------------------------------------------------------------------------
//File Name: partGeneration.cs
//File Type: Visual Studio C# Unity File
//Project Title: Measured Alteration Of Generalized Procedural Generation For Robot Agility
//Author: A'di Dust
//Date Created: 6/29/2021
//Last Modified: 7/23/2021
//Description: File to handle object rendering
///-----------------------------------------------------------------------------------------------------------------


using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

///<summary>Class <c>partGeneration</c> handles specifics of rendering 
///parts and obstacles using alteration information.</summary>
public class partGeneration : MonoBehaviour
{
    //width and height of available space for placement
    private List<double> width = new List<double>();
    private List<double> height = new List<double>();
    //part/obstacle dimensions
    private List<double>  partWidth = new List<double>();
    private List<double> partHeight = new List<double>();
    private List<double> partDepth = new List<double>();
    //boundaries of space for placement
    public List<double> minX;
    public List<double> maxX;
    public List<double> minY;
    public List<double> maxY;
    public List<double> minZ;
    public List<double> maxZ;
    //frequency variables
    public List<int> typicalNumOfParts;
    public List<int> maxFrequencyVariation;
    public double frequencyScore;
    private List<int> frequency = new List<int>();
    //spacing variables
    public List<double> typicalSpacing;
    public List<double> maxSpaceVariation;
    public double spacingScore;
    private List<List<Vector3>> positions = new List<List<Vector3>>();
    //orientation variables
    public List<float> maxRotation;
    public double orientationScore;
    private int numOfOrientations;
    private List<List<float>> orientations = new List<List<float>>();
    //prefabs to use
    public List<GameObject> partObject;



    ///<summary>Sets number of objects somewhere between typical frequency
    ///+/- (score*max frequency)</summary>
    public void setFrequency()
    {
        int min = 0; 
        for(int i = 0; i < partObject.Count(); i++)
        {
            int variation = Mathf.RoundToInt((float)((double)maxFrequencyVariation[i] * frequencyScore));
            if(typicalNumOfParts[i] + variation <= 0)
            {
                frequency.Add(0);
            }
            else
            {
                if (typicalNumOfParts[i] - variation > 0)
                {
                    min = typicalNumOfParts[i] - variation;
                }
                frequency.Add(Mathf.RoundToInt(Random.Range(min, typicalNumOfParts[i] + variation)));
                //uncomment line below to print frequency values:
                //print("frequency[" + i + "]: " + frequency[i]);
                }
        }
    }


    ///<summary>Sets rotation of each object somewhere between typical orientation
    ///+/- (score*max rotation)</summary>
    public void setOrientations()
    {
        for(int i = 0; i < partObject.Count(); i++)
        {
            orientations.Insert(i, new List<float>());
            numOfOrientations = Formulas.setPartOrientation(frequency[i], orientationScore);
            for(int j = 0; j < frequency[i]; j++)
            {
                orientations[i].Add(Random.Range(0-maxRotation[i], maxRotation[i]));
            }
            //uncomment line below to print orientation values:
            //print("Orientations[" + i + "]: " + string.Join(", ", orientations[i]));
        }
    }


    ///<summary>Sets starting positions of each object from bottom back left 
    ///with typical spacing and then adjust by typical spacing +/- (score*max spacing)</summary>
    public void createPositions(int i){
        positions.Insert(i, new List<Vector3>());
        double maxVariance = Formulas.setPartSpacing(frequency[i], maxSpaceVariation[i], spacingScore);
        //start with parts starting from bottom back left with typical spacing untl runs out of room
        //or objects
        int count = 0;
        double y = minY[i];
        while(y < maxY[i] && count < frequency[i])
        {
            for(double z = maxZ[i] - (partHeight[i]/2); z >= minZ[i] + (partHeight[i]/2); z = z - typicalSpacing[i] - partHeight[i])
            {
                for(double x = minX[i] + (partWidth[i]/2); x <= maxX[i] - (partWidth[i]/2); x = x + typicalSpacing[i] + partWidth[i])
                {
                    positions[i].Add(new Vector3((float)x, (float)y, (float)z));
                    count++;
                    if(count >= frequency[i])
                    {
                        break;
                    }
                }
            }
            y = y + partDepth[i];
        }
        varyPositions(i, maxVariance);
    }


    ///<summary>Move objects to be changed within a random range of variance specified by user.</summary>
    private void varyPositions(int i, double maxVariance){
        for(int j = 0; j < positions[i].Count(); j++)
        {
            double randomXVariation = Random.Range(0, (float)maxVariance);
            double randomZVariation = Random.Range(0, (float)maxVariance);
            if(randomXVariation + positions[i][j].x < minX[i] + width[i] - typicalSpacing[i] - (partWidth[i]/2))
            {
                positions[i][j] = new Vector3(positions[i][j].x + (float)randomXVariation, (float)minY[i] , positions[i][j].z);
            }
            if(randomZVariation + positions[i][j].z < maxZ[i] - typicalSpacing[i] - (partHeight[i]/2))
            {
                positions[i][j] = new Vector3(positions[i][j].x, (float)minY[i], positions[i][j].z + (float)randomZVariation);
            }
        }
        //uncomment line below to print position values:
        //print("Positions[" + i + "]: " + string.Join(", ", positions[i]));
    }


    ///<summary>checks that with gravity, part is still within defined bounds, if not then destroy it</summary>
    ///<param name="parts">Rigid body objects to check bounds of</param>
    ///<param name="partIndex">Index to know which corresponding bounds to check</param>
    public void partsAreInPlace(Rigidbody [] parts, int partIndex){
        foreach(Rigidbody part in parts){
            if(part.gameObject.name.Contains(partObject[partIndex].name)){
                GameObject part1 = part.gameObject;
                if(part1.transform.position.x < minX[partIndex] || part1.transform.position.x > maxX[partIndex] || part1.transform.position.y < minY[partIndex] || part1.transform.position.y > maxY[partIndex] || part1.transform.position.z < minZ[partIndex] || part1.transform.position.z > maxZ[partIndex])
                {
                    //uncomment below to see where objects were destroyed:
                    //print(partIndex + " destroyed at:  " + part.position);
                    if(!partObject[partIndex].Equals(part.gameObject)){
                        Destroy(part.gameObject);
                    }
                }
            }
        }
    }


    ///<summary>Simulates the effects of gravity manually to place and destroy before start of scene.</summary>
    public void setFinalPlacement(){
        int maxIterations = 1000;
        Rigidbody [] simulatedBodies = FindObjectsOfType<Rigidbody>();
        Physics.autoSimulation = false;
        for(int i = 0; i < partObject.Count(); i++)
        {
            for(int j = 0; j < maxIterations; j++)
            {
                Physics.Simulate(Time.fixedDeltaTime);
                if(simulatedBodies.All(rb => rb.IsSleeping()))
                {
                    partsAreInPlace(simulatedBodies, i);
                    break;
                }
            }
            partsAreInPlace(simulatedBodies, i);
        }
        Physics.autoSimulation = true;
    }


    void Awake()
    {
        setFrequency();
        setOrientations();
        
        for(int i = 0; i < partObject.Count(); i++)
        {
            //get part dimentions from prefab
            Bounds bounds = new Bounds (partObject[i].transform.position, Vector3.zero);
            Collider[] renderers = partObject[i].GetComponentsInChildren<Collider> ();
            foreach (Collider renderer in renderers)
            {
                bounds.Encapsulate(renderer.bounds);
            }
            partWidth.Add(bounds.size.x);
            partHeight.Add(bounds.size.z);
            partDepth.Add(bounds.size.y);
            //find dimensions of part space for placement
            width.Add(maxX[i] - minX[i]);
            height.Add(maxZ[i] - minZ[i]);

            //setup alterations
            createPositions(i);

            //instantiate all parts and obstacles using given alterations
            for(int j = 0; j < frequency[i]; j++)
            {
                if(positions[i].Count() > j)
                {
                    Instantiate(partObject[i], positions[i][j], Quaternion.Euler(new Vector3(orientations[i][j], 0, 0)));
                }
            }
        }
        setFinalPlacement();
        //destroy original prefabs
        foreach(GameObject part in partObject)
        {
            Destroy(part.GetComponent<MeshRenderer>());
        }
    }
    
}
