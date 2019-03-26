using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[CreateAssetMenu(fileName = "New Alien Type", menuName = "IP2/Alien", order = 2)]
public class AlienData : ScriptableObject
{
    public Sprite alienIcon;

    [Header("Alien Food Preferences")]
    public List<string> likes;
    public List<string> dislikes;
}
