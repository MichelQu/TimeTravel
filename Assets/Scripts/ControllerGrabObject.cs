using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

// 2 - Ce script gère les contrôles et les interactions que possède l'utilisateur avec les controllers du casque.

public class ControllerGrabObject : MonoBehaviour {

    //A reference to the object being tracked. In this case, a controller.
    public SteamVR_Action_Boolean Movement;
    public SteamVR_Input_Sources handType;

    private SteamVR_TrackedObject trackedObj;

    private Vector3 lastPosition=Vector3.zero;
    private Vector3 velocity=Vector3.zero;

    private Quaternion lastRotation = Quaternion.identity;
    private Vector3 angularVelocity = Vector3.zero;

    //A device property to provide easy access to the controller. It uses the tracked object’s index to return the controller’s input.
    // private SteamVR_Controller.Device Controller
    //{
    //    get { return SteamVR_Controller.Input((int)trackedObj.index); }
    //}

    void Awake()
    {
        //reference to the SteamVR_TrackedObject
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Start()
    {
        SphereCollider collider = this.gameObject.AddComponent<SphereCollider>();
        collider.radius = 0.15f;
        collider.isTrigger = true;
        Rigidbody rbody = this.gameObject.AddComponent<Rigidbody>();
        rbody.isKinematic = true;

    }

    // Stores the GameObject that the trigger is currently colliding with, so you have the ability to grab the object.
    private GameObject collidingObject;
    // Serves as a reference to the GameObject that the player is currently grabbing.
    private GameObject objectInHand;

    private void SetCollidingObject(Collider col)
    {
        // Doesn’t make the GameObject a potential grab target if the player is already holding something or the object has no rigidbody.
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        // Assigns the object as a potential grab target.
        collidingObject = col.gameObject;
    }

    // When the trigger collider enters another, this sets up the other collider as a potential grab target.
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    // Ensures that the target is set when the player holds a controller over an object for a while. Without this, the collision may fail or become buggy.
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    // When the collider exits an object, abandoning an ungrabbed target, this code removes its target by setting it to null.
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void GrabObject()
    {
        // Move the GameObject inside the player’s hand and remove it from the collidingObject variable.
        objectInHand = collidingObject;
        collidingObject = null;
        // Add a new joint that connects the controller to the object using the AddFixedJoint() method below.
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    // Make a new fixed joint, add it to the controller, and then set it up so it doesn’t break easily. Finally, you return it.
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        // Make sure there’s a fixed joint attached to the controller.
        if (GetComponent<FixedJoint>())
        {
            // Remove the connection to the object held by the joint and destroy the joint.
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // Add the speed and rotation of the controller when the player releases the object, so the result is a realistic arc.
            objectInHand.GetComponent<Rigidbody>().velocity = velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = angularVelocity;
        }
        // Remove the reference to the formerly attached object.
        objectInHand = null;
    }

    // Update is called once per frame
    void Update () {
        //Calculate velocity
        velocity = (this.gameObject.transform.position - lastPosition) / Time.deltaTime;
        lastPosition = this.gameObject.transform.position;

        //Calculate angular velocity
        (this.gameObject.transform.rotation*Quaternion.Inverse(lastRotation)).ToAngleAxis(out float angleInDegrees, out Vector3 rotationAxis);
        Vector3 angularDisplacement = rotationAxis * angleInDegrees * Mathf.Deg2Rad;
        angularVelocity = angularDisplacement / Time.deltaTime;
        lastRotation = this.gameObject.transform.rotation;
        
        //Debug.Log(collidingObject?.name ?? "non");
        // When the player squeezes the trigger and there’s a potential grab target, this grabs it.
        if (Movement.GetStateDown(handType))
        {
            if (collidingObject)
            {
                GrabObject();
            } else {
                Debug.Log("Got input, but no colliding object");
            }
        }

        // If the player releases the trigger and there’s an object attached to the controller, this releases it.
        if (Movement.GetStateUp(handType))
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }
}
