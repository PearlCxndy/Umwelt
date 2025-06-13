using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GrassDataAsset", menuName = "Grass/GrassDataAsset")]
public class GrassDataAsset : ScriptableObject
{
    public List<GrassData> grassData;
}
