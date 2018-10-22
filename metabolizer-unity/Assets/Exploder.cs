using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour {

    public GameObject explodePoint;
    public float explodeAmount;
    Explodee[] explodees;
    bool shouldExplode = true;
    //public bool explodeAll;

	// Use this for initialization
	void Start () {
        Selecter.OnDeselect += Reposition;
        Selecter.OnSelect += DisableExplode;
        explodees = GetComponentsInChildren<Explodee>();
        foreach (Explodee explodee in explodees){
            explodee.explodeDirection = explodee.startPosition - explodePoint.transform.position;
            explodee.distance = (explodee.explodeDirection - explodee.startPosition).magnitude;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.mouseScrollDelta.y!=0 && shouldExplode){
            explodeAmount += Input.mouseScrollDelta.y * Time.deltaTime;
            explodeAmount = Mathf.Max(explodeAmount, 0f);
            foreach (Explodee explodee in explodees)
            {
                if (!explodee.selected)
                    Reposition(explodee,explodee.transform);
                //else if (!explodee.selected && explodeAll)
                    //Reposition(explodee,explodee.items[0].transform);
            }

        }

	}

    void Reposition(Explodee explodee,Transform explodeeTransform){
        shouldExplode = true;
        explodeeTransform.position = explodee.startPosition - explodee.meshOffset + explodee.explodeDirection * explodeAmount * explodee.distance;
    }

    void DisableExplode(){
        shouldExplode = false;
    }
}
