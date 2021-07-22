using System;
using System.Collections.Generic;

public class Formulas {

    //Takes a maximum and minimum value and returns integer
    //value that correlates to the score
    private static int setNormalizedProperty (double score, double max, double min){
        return (int) Math.Round((score)*(max-min)+min);
    }

    private static double setDecimalNormalizedProperty (double score, double max, double min){
        return ((score)*(max-min)+min);
    }

    public static int setPartFrequence(double max, double score){
        double min = 0;
        return setNormalizedProperty(score, max, min);
    }

    public static double setPartSpacing(int numOfParts, double max, double score){
        return setDecimalNormalizedProperty(score, max, 0);
    }

    public static int setNumOfSpacingVariance(int numOfParts, double score){
        return setNormalizedProperty(score, numOfParts, 0);
    }

    public static int setPartOrientation(int numOfParts, double score){
        return setNormalizedProperty(score, numOfParts, 0);
    }

    public static int setPartRoughness(int numOfParts, double score){
        return setNormalizedProperty(score, numOfParts, 0);
    }

    public static int setObstacleFrequence(double obstacleSpace, double usableSpace, double score){
        double min = 1;
        double max = Math.Round((usableSpace - (2*obstacleSpace))/obstacleSpace);
        return setNormalizedProperty(score, max, min);
    }

    public static double setObstacleSpacing(int numOfObstacles, double max, double min, double score){
        return setDecimalNormalizedProperty(score, max, min);
    }

    public static double setObstacleTypes(int numOfObstacles, double score){
        return setDecimalNormalizedProperty(score, numOfObstacles, 0);
    }

    public static int setLightFrequence(double lightSpace, double usableSpace, double score){
        double min = 1;
        double max = Math.Round((usableSpace - (2*lightSpace))/lightSpace);
        return setNormalizedProperty(score, max, min);
    }

    public static double setLightIntensity(double numOfLights, double score){
        double min = 0;
        double max = 8 * numOfLights;
        return setDecimalNormalizedProperty(score, max, min);
    }

    public static int setLightColors(double numOfLights, double score){
        return setNormalizedProperty(score, numOfLights, 0);
    }

}