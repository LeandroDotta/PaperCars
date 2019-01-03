using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Transform spawnPoint;

    [SerializeField] private CinemachineVirtualCamera followCam;
    [SerializeField] private VehicleList availableVehicles;

    [Header("UI")]
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLoose;
    [SerializeField] private GameObject panelPause;


    private VehicleData player;

    public static StageManager Current { get; private set; }
    public bool IsPaused { get; private set; }

    private void Reset() 
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
        followCam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
    }
    
    private void Awake() 
    {
        Current = this;
    }

    void Start()
    {
        player = availableVehicles.vehicles.Find(obj => obj.id == GamePreferences.SelectedVehicle);

        GameObject playerObj = Instantiate(player.prefabFull, spawnPoint.position, Quaternion.identity);
        followCam.Follow = playerObj.transform;
        playerObj.SetActive(true);
    }

    private void OnDestroy() 
    {
        Current = null;    
    }

    public void Win()
    {
        panelWin.SetActive(true);
    }

    public void Loose()
    {
        panelLoose.SetActive(true);
    }

    public void Pause()
    {
        IsPaused = true;
        Time.timeScale = 0;
        panelPause.SetActive(true);
    }

    public void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1;
        panelPause.SetActive(false);
    }
}
