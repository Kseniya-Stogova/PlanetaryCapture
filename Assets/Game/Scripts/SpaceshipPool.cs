using System.Collections;
using System.Collections.Generic;
using PlanetaryCapture;
using UnityEngine;

public class SpaceshipPool : MonoBehaviour
{
    public static SpaceshipPool SharedInstance;
    public List<Spaceship> pooledObjects;
    public Spaceship objectToPool;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<Spaceship>();
        Spaceship tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.gameObject.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public Spaceship GetSpaceship()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
