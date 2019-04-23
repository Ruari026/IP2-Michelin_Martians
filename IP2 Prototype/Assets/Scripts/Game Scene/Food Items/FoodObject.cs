using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[CreateAssetMenu(fileName = "New Food Type", menuName = "IP2/Food", order = 1)]
public class FoodObject : ScriptableObject
{
    public Sprite foodIcon;

    [Header("Food Details")]
    public CookingSetting foodSetting;
    public FoodToughness foodToughness;


    [Header("FoodTypes To Transform Into & Related Appliance")]
    public ApplianceTypes[] applianceSolutions = new ApplianceTypes[1];
    public FoodObject[] applianceTransformations = new FoodObject[1];

    [Header("FoodTypes To Combine With")]
    public FoodObject[] foodCombinations = new FoodObject[1];
    public FoodObject[] combinationTransformations = new FoodObject[1];

    [Header("Default Transformation If Player Fails Puzzle")]
    public FoodObject failedTransformation;
}

public enum CookingSetting
{
    LOW,
    MED,
    HIGH
};

public enum FoodToughness
{
    SOFT,
    WEAK,
    TOUGH,
    STRONG,
    HARD
};
