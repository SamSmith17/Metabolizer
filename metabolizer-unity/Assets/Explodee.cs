using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSplodee{
    public MeshRenderer mesh;
    public Vector3 meshOffset;
    public Vector3 startPosition;
    public Vector3 explodeDirection;
    public float distance;

}

public class Explodee : MonoBehaviour
{
    public List<SubSplodee> items;
    public Vector3 meshOffset;
    public Vector3 startPosition;
    public Vector3 explodeDirection;
    public float distance;
    public bool selected;

    float explodeAmount = 0f;


    // Use this for initialization
    void Awake()
    {
        items = new List<SubSplodee>();
        SubSplodee newSplodee;
        MeshRenderer[] meshes= GetComponentsInChildren<MeshRenderer>();
        Vector3 positionSums = Vector3.zero;
        foreach (MeshRenderer mesh in meshes)
        {
            newSplodee = new SubSplodee();
            newSplodee.mesh = mesh;
            newSplodee.meshOffset = mesh.bounds.center;

            positionSums += newSplodee.meshOffset;
            items.Add(newSplodee);
        }

        Vector3 inverseVector = new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);
        meshOffset = positionSums / items.Count - Vector3.Scale(transform.position, inverseVector);
        startPosition = transform.position + meshOffset;

        foreach (SubSplodee splodee in items)
        {
            splodee.meshOffset-=Vector3.Scale(transform.position, inverseVector);
            splodee.startPosition = transform.position + splodee.meshOffset;
            splodee.explodeDirection = splodee.startPosition - startPosition;
            splodee.distance = (splodee.explodeDirection - splodee.startPosition).magnitude;
        }

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (selected && Input.mouseScrollDelta.y != 0)
        {
            explodeAmount += Input.mouseScrollDelta.y * Time.deltaTime;
            Explode(explodeAmount);

        }

    }

    public void OnMouseEnter()
    {
        if (!selected){
            foreach (SubSplodee splodee in items)
            {
                MeshRenderer _renderer = splodee.mesh;
                _renderer.material.color = Color.red;
            }
        }

    }

    public void OnMouseExit()
    {
        if (!selected){
            foreach (SubSplodee splodee in items)
            {
                MeshRenderer _renderer = splodee.mesh;
                _renderer.material.color = Color.grey;
            }

        }

    }


    public void ToggleSelect()
    {
        selected = !selected;
    }

    public void Deselect(){
        selected = false;
        Explode(0f);
        foreach (Renderer _renderer in transform.GetComponentsInChildren<Renderer>())
        {
            _renderer.material.color = Color.grey;
        }
    }

    public void Select()
    {
        selected = true;

        foreach (Renderer _renderer in transform.GetComponentsInChildren<Renderer>())
        {
            _renderer.material.color = Color.blue;
        }
    }

    void Explode(float amount){
        foreach (SubSplodee splodee in items)
        {
            splodee.mesh.transform.position = transform.position + splodee.explodeDirection * amount * splodee.distance;
        }

    }

}
