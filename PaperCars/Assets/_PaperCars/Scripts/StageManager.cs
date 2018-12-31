using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Transform spawnPoint;

    [SerializeField] private CinemachineVirtualCamera followCam;
    [SerializeField] private VehicleList availableVehicles;

    private VehicleData player;

    private void Reset() 
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
        followCam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
    }

    void Start()
    {
        player = availableVehicles.vehicles.Find(obj => obj.id == GamePreferences.SelectedVehicle);

        GameObject playerObj = Instantiate(player.prefab, spawnPoint.position, Quaternion.identity);
        followCam.Follow = playerObj.transform;
        playerObj.SetActive(true);
    }
}
