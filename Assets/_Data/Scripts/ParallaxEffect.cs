using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : NhoxBehaviour
{
    public Camera cam;
    public Transform followTarget;

    //Starting position for parallax game object
    [SerializeField] protected Vector2 startingPosition;

    //Start Z value of the parallax game obj
    [SerializeField] protected float startingZ;

    //Distance that the camera has moved from the starting position of the parallax obj
    protected Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    //If obj is in front of the target, use near clip plane. If behind the target use far clip plane
    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    //The further the obj from the player , the faster the ParallaxEffect obj will move. Drag it's Z value closer to the target to make it move slower
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    // Start is called before the first frame update
    protected override void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //When the target move, move the parallax obj the same distance time a multiplier
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;
        //The X/Y position changes based on target travel speed times the parallax factor, but Z stay consistant
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCamera();
        this.LoadTarget();
    }

    protected virtual void LoadCamera()
    {
        if (cam != null) return;
        cam = FindObjectOfType<Camera>();
        Debug.Log(transform.name + " Load camera", gameObject);
    }

    protected virtual void LoadTarget()
    {
        if (followTarget != null) return;
        followTarget = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(transform.name + " Load follow target", gameObject);
    }
}
