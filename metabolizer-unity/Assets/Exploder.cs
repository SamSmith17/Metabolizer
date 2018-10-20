using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour {

    public GameObject explodePoint;
    public float explodeAmount;
    Explodee[] explodees;
    //public bool explodeAll;

	// Use this for initialization
	void Start () {
        Selecter.OnDeselect += Reposition;
        //if (explodeAll){
        //    List<Explodee> createdExplodees = new List<Explodee>();
        //    foreach(MeshRenderer _renderer in GetComponentsInChildren<MeshRenderer>()){
        //        Explodee newExplodee = new Explodee();
        //        newExplodee.items = new MeshRenderer[]{_renderer};
        //        Vector3 positionSums = Vector3.zero;
        //        Vector3 inverseVector = new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);
        //        newExplodee.meshOffset = _renderer.bounds.center - Vector3.Scale(transform.position, inverseVector);
        //        newExplodee.startPosition = transform.position + newExplodee.meshOffset;
        //        createdExplodees.Add(newExplodee);
        //    }
        //    explodees = createdExplodees.ToArray();
        //}
        explodees = GetComponentsInChildren<Explodee>();
        foreach (Explodee explodee in explodees){
            explodee.explodeDirection = explodee.startPosition - explodePoint.transform.position;
            explodee.distance = (explodee.explodeDirection - explodee.startPosition).magnitude;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.mouseScrollDelta.y!=0){
            explodeAmount += Input.mouseScrollDelta.y * Time.deltaTime;

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
        explodeeTransform.position = explodee.startPosition - explodee.meshOffset + explodee.explodeDirection * explodeAmount * explodee.distance;
    }
}
