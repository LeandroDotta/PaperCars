using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Paper Cars/Vehicle Data", fileName = "New Vehicle Data")]
public class VehicleData : ScriptableObject
{
    public int id;
    public string vehicleName;

    [Header("Prefabs")]
    public GameObject prefabBase;
    public GameObject prefabFull;
}
