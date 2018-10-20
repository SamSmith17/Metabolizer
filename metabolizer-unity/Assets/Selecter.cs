using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecter : MonoBehaviour
{

    Explodee selectedExplodee;
    public float holdDistance;

    public delegate void DeselectAction(Explodee explodee, Transform explodeeTransform);
    public static event DeselectAction OnDeselect;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Explodee clickedExplodee = hit.transform.GetComponentInParent<Explodee>();
                if (clickedExplodee != null)
                {
                    if (!clickedExplodee.selected){
                        if (selectedExplodee != null)
                            Deselect();
                        Select(clickedExplodee);
                    }
                    else
                        Deselect();
                }
                else{
                    Deselect();
                }
            }
            else{
                Deselect();
            }
        }

        if (selectedExplodee != null)
        {
            //Debug.Log(selectedExplodee.name + " moving. offset: " + selectedExplodee.meshOffset + ", should be " + (Camera.main.transform.position + holdDistance * ray.direction - selectedExplodee.meshOffset) + ", actual " + selectedExplodee.transform.position);
            selectedExplodee.transform.position =Camera.main.transform.position + holdDistance* ray.direction - selectedExplodee.meshOffset;
        }
    }

    void Deselect(){
        if (selectedExplodee != null)
        {
            OnDeselect(selectedExplodee, selectedExplodee.transform);
            selectedExplodee.Deselect();
            selectedExplodee = null;
        }


    }

    void Select(Explodee clickedExplodee){
        clickedExplodee.Select();
        selectedExplodee = clickedExplodee;
    }
}