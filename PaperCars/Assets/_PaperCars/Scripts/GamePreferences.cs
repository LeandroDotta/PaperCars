using UnityEngine;

public static class GamePreferences
{
    public static int SelectedVehicle
    {
        get
        {
            return PlayerPrefs.GetInt("selected_vehicle", 1);
        }
        set
        {
            PlayerPrefs.SetInt("selected_vehicle", value);
        }
    }
}
