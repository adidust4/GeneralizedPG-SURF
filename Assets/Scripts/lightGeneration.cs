///-----------------------------------------------------------------------------------------------------------------
//File Name: lightGeneration.cs
//File Type: Visual Studio C# Unity File
//Project Title: Measured Alteration Of Generalized Procedural Generation For Robot Agility
//Author: A'di Dust
//Date Created: 7/8/2021
//Last Modified: 7/20/2021
//Description: File to handle light rendering
///-----------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

///<summary>Class <c>lightGeneration</c> handles specifics of rendering 
///inside and outside lighting using alteration information.</summary>
public class lightGeneration : MonoBehaviour
{
    public SharedData dataLights;
    public Light [] lights;
    public double [] typicalIntensity;
    public double [] maxIntensityVariation;
    public Color [] typicalLightColor;
    public double intensityScore;
    public double sunScore;
    public double colorScore;

    ///<summary>Sets inside light intensity somewhere between typical intensity
    ///+/- (score*max variation)</summary>
    private void setLightStrength()
    {
        for(int i = 0; i < lights.Length; i++)
        {
            double min = typicalIntensity[i];
            double variation = intensityScore * maxIntensityVariation[i];
            if (typicalIntensity[i] - variation > 0)
            {
                min = typicalIntensity[i] - variation;
            }
            double intensity = Random.Range((float)min, (float) (typicalIntensity[i] + variation));
            lights[i].intensity = Mathf.RoundToInt((float)intensity);
            //uncomment line below to print intensity values:
            //print("intensity[" + i + "]: " + lights[i].intensity);
        }
    }


    ///<summary>Sets inside light color somewhere between typical color
    ///+/- (score*max variation) within r,g,b,a context</summary>
    public void setLightColor()
    {
        for(int i = 0; i < lights.Length; i++)
        {
            double minColorR = typicalLightColor[i].r - colorScore;
            double minColorG = typicalLightColor[i].g - colorScore;
            double minColorB = typicalLightColor[i].b - colorScore;
            double minColorA = typicalLightColor[i].a - colorScore;
            double maxColorR = typicalLightColor[i].r + colorScore;
            double maxColorG = typicalLightColor[i].g + colorScore;
            double maxColorB = typicalLightColor[i].b + colorScore;
            double maxColorA = typicalLightColor[i].a + colorScore;
            if(minColorR < 0)
            {
                minColorR = 0;
            }
            if(minColorG < 0)
            {
                minColorG = 0;
            }
            if(minColorB < 0)
            {
                minColorB = 0;
            }
            if(minColorA < 0)
            {
                minColorA = 0;
            }

            if(maxColorR > 1)
            {
                maxColorR = 1;
            }
            if(maxColorG > 1)
            {
                maxColorG = 1;
            }
            if(maxColorB > 1)
            {
                maxColorB = 1;
            }
            if(maxColorA > 1)
            {
                maxColorA = 1;
            }

            float R = Random.Range((float)minColorR, (float)maxColorR);
            float G = Random.Range((float)minColorG, (float)maxColorG);
            float B = Random.Range((float)minColorB, (float)maxColorB);
            float A = Random.Range((float)minColorA, (float)maxColorA);

            lights[i].color = (new Color(R, G, B, A));
            //uncomment line below to print color values:
            // print("color[" + i + "]: " + lights[i].color.r + ", " + lights[i].color.g + ", " + lights[i].color.b + ", " + lights[i].color.a);
        }
    }


    ///<summary>Sets outside light position somewhere between mid-day
    ///+/- (score*max variation) rotation of sun</summary>
    private void setSunLight()
    {
        GameObject sun = new GameObject("Sun");
        Light sunComp = sun.AddComponent<Light>();
        sunComp.color = Color.white;
        sunComp.type = LightType.Directional;
        sunComp.intensity = 1;
        double maxVariation = sunScore * 180;
        float sunTime = Random.Range(90 - (float)maxVariation, (float)maxVariation + 90);
        sun.transform.Rotate(sunTime, 0, 0, Space.Self);
        //uncomment line below to print sun rotation values:
        // print("rotation: " + sun.Rotate());
    }


    // Start is called before the first frame update
    void Start()
    {
        // intensityScore = dataLights.intensityScore;
        // colorScore = dataLights.colorScore;
        // sunScore = dataLights.sunScore;
        setLightStrength();
        setSunLight();
        setLightColor();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            print("a");
            sunScore = 0.5;
            colorScore = 0.5;
            intensityScore = 0.5;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(Input.GetKeyDown("b")){
            print("b");
            sunScore = 1;
            colorScore = 1;
            intensityScore = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Start();
        }
    }
}
