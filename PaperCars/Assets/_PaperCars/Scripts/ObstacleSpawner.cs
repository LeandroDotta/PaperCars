using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float interval;
    [SerializeField] private GameObject prefab;

    private IEnumerator Start()
    {
        while (true)
        {
            GameObject obj = Instantiate(prefab, this.transform.position, Quaternion.identity, this.transform);
            Vector3 rotation = obj.transform.rotation.eulerAngles;
            rotation.z = Random.Range(0, 360);
            obj.transform.rotation = Quaternion.Euler(rotation);
            obj.SetActive(true);

            yield return new WaitForSeconds(interval);
        }
    }
}
