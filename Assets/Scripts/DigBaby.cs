using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigBaby : MonoBehaviour
{

    public SnowTerrain snowTerrain;
    public Vector3 legPos;
    public Vector2 legSize;


    private void Update()
    {
        legPos = transform.position;
        legSize = new Vector2(4f, 4f);
    }

    private void FixedUpdate()
    {
        snowTerrain.Dig(legPos, legSize);
    }

    public void OnDig()
    {
        snowTerrain.Dig(legPos, legSize);
        snowTerrain.Hello();

        Debug.Log(legPos);
    }

}
