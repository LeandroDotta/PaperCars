using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Paper Cars/Vehicle Data", fileName = "New Vehicle Data")]
public class VehicleData : ScriptableObject
{
    public int id;
    public string vehicleName;

    public GameObject prefab;
}
