using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleSelection : MonoBehaviour
{
    public VehicleList availableVehicles;
    public Vector2 spawnPoint;

    [SerializeField] private Text textName;

    private int selectedIndex = 0;
    private List<GameObject> loadedVehicles;

    private void Start()
    {
        int selectedId = GamePreferences.SelectedVehicle;
        loadedVehicles = new List<GameObject>();

        for (int i = 0; i < availableVehicles.vehicles.Count; i++)
        {
            VehicleData vehicle = availableVehicles.vehicles[i];

            if (vehicle.id == selectedId)
            {
                selectedIndex = i;
            }

            GameObject obj = Instantiate(vehicle.prefabBase, spawnPoint, Quaternion.identity, this.transform);
            obj.name = "Vehicle " + (i + 1);
            obj.transform.localScale = new Vector3(.8f, .8f, .8f);
            obj.SetActive(false);

            Rigidbody2D rb2d = obj.GetComponent<Rigidbody2D>();
            rb2d.drag = 1;
            rb2d.gravityScale = 3;
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

            loadedVehicles.Add(obj);
        }

        SelectVehicle(0, selectedIndex);
    }

    private void Update()
    {
        bool pressed = Input.GetButtonDown("Horizontal");

        if (pressed)
        {
            float axisHorizontal = Input.GetAxisRaw("Horizontal");
            if (axisHorizontal == 1)
            {
                SelectNext();
            }
            else if (axisHorizontal == -1)
            {
                SelectPrevious();
            }
        }
    }

    private void SpawnVehicle(int index)
    {
        loadedVehicles[index].transform.position = spawnPoint;
        loadedVehicles[index].SetActive(true);
    }

    private void HideVehicle(int index)
    {
        loadedVehicles[index].SetActive(false);
    }

    private void SelectNext()
    {
        if (selectedIndex >= loadedVehicles.Count-1)
            return;

        SelectVehicle(selectedIndex, selectedIndex + 1);
    }

    private void SelectPrevious()
    {
        if (selectedIndex <= 0)
            return;

        SelectVehicle(selectedIndex, selectedIndex - 1);
    }

    private void SelectVehicle(int currentIndex, int nextIndex)
    {
        HideVehicle(currentIndex);

        VehicleData vehicle = availableVehicles.vehicles[nextIndex];

        textName.text = vehicle.vehicleName;

        selectedIndex = nextIndex;
        GamePreferences.SelectedVehicle = vehicle.id;

        SpawnVehicle(nextIndex);
    }
}
