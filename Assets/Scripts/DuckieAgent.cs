using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckieAgent : Agent {
	Rigidbody rigidBody;
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
	}

	public override void AgentReset() {
		if (transform.position.y < -1.0) // XXX
        {
            // The agent fell
            transform.position = new Vector3(2.25f, 0.1f, 1.25f); // XXX
            rigidBody.angularVelocity = Vector3.zero; // XXX
            rigidBody.velocity = Vector3.zero; // XXX
        }
	}

	public override void CollectObservations()
	{
    	// Calculate relative position
    	//Vector3 relativePosition = Target.position - this.transform.position;
    
    	// Relative position
    	AddVectorObs(transform.position.x);
    	AddVectorObs(transform.position.z);
    
    	// Distance to edges of platform
    	//AddVectorObs((this.transform.position.x + 5)/5);
    	//AddVectorObs((this.transform.position.x - 5)/5);
    	//AddVectorObs((this.transform.position.z + 5)/5);
    	//AddVectorObs((this.transform.position.z - 5)/5);
    
    	// Agent velocity
    	//AddVectorObs(rigidBody.velocity.x/5);
    	//AddVectorObs(rigidBody.velocity.z/5);
	}

	public float speed = 10;
	//private float previousDistance = float.MaxValue;
	public override void AgentAction(float[] vectorAction, string textAction)
	{
    	// Rewards
    	/*float distanceToTarget = Vector3.Distance(this.transform.position, 
                                              Target.position);*/
    
    	// Reached target
    	/*if (distanceToTarget < 1.42f)
    	{
	        Done();
        	AddReward(1.0f);
		}*/
    
    	// Getting closer
    	/*if (distanceToTarget < previousDistance)
	    {
	        AddReward(0.1f);
		}*/

    	// Time penalty
    	//AddReward(-0.05f);

    	// Fell off platform
    	if (transform.position.y < -1.0)
    	{
			AddReward(-1.0f);
	        Done();
    	}

    	//previousDistance = distanceToTarget;

    	// Actions, size = 2
    	Vector3 controlSignal = Vector3.zero;
    	controlSignal.x = Mathf.Clamp(vectorAction[0], -1, 1);
    	controlSignal.z = Mathf.Clamp(vectorAction[1], -1, 1);
		Debug.Log(controlSignal);
    	rigidBody.AddForce(controlSignal * speed);
    }
}
