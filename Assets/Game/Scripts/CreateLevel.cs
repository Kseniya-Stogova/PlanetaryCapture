using System.Collections;
using System.Collections.Generic;
using PlanetaryCapture;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    public void Create(GameController level)
    {
        gameObject.SetActive(false);

        Instantiate(level);
    }
}
