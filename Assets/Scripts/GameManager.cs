using NUnit.Framework;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject[] coverPoints;
    public List<GameObject> buildings;

    private void Start()
    {
        coverPoints = GameObject.FindGameObjectsWithTag("CoverPoint");

        Application.targetFrameRate = 60; // or any number you want
        QualitySettings.vSyncCount = 1;   // 0 = off, 1 = every VBlank

        buildings = GameObject.FindGameObjectsWithTag("Building").ToList();

        StartCoroutine(DisableBuildingFloors());
    }

    IEnumerator DisableBuildingFloors()
    { yield return new WaitForSeconds(0.5f);
        foreach (var building in buildings)
        {
            var floors = building.GetComponentsInChildren<Transform>().Where(t => t.name.Contains("Level")).ToList();
            foreach (var floor in floors)
            {
                if (floor.transform.name == "Level1")
                {
                    floor.gameObject.SetActive(true);
                }
                else{ floor.gameObject.SetActive(false); }
            }
        }
    }
}
