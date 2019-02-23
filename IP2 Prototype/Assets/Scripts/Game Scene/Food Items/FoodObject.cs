using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[CreateAssetMenu(fileName = "New Food Type", menuName = "IP2/Food", order = 1)]
public class FoodObject : ScriptableObject
{
    public Sprite foodIcon;
    
    [Header("FoodTypes To Transform Into & Related Appliance")]
    public ApplianceTypes[] applianceSolutions = new ApplianceTypes[1];
    public FoodObject[] applianceTransformations = new FoodObject[1];

    [Header("Default Transformation If Player Fails Puzzle")]
    public FoodObject failedTransformation;
}
