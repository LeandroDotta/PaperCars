using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Paper Cars/Vehicle List", fileName = "New Vehicle List")]
public class VehicleList : ScriptableObject
{
    public List<VehicleData> vehicles;
}
