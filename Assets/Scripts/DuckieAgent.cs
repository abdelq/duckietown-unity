using UnityEngine;

public class DuckieAgent : Agent {
    new Rigidbody rigidbody;
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
	}

	public Transform Target; // XXX Should not need target
	public override void AgentReset() {
		if (this.transform.position.y < -2.0) { // XXX Falling
			this.transform.position = Vector3.zero;
			this.rigidbody.velocity = Vector3.zero;
			this.rigidbody.angularVelocity = Vector3.zero; // XXX
		} else {
			Target.position = new Vector3(Random.value * 8 - 4, 1f, Random.value * 8 - 4); // XXX Need to put in proper random position
		}
	}

	public override void CollectObservations() {
		// Relative position
		var relativePosition = Target.position - this.transform.position;
		AddVectorObs(relativePosition.x/5);
		AddVectorObs(relativePosition.z/5);

		// Distance to edges of map
    	AddVectorObs((this.transform.position.x + 5)/5); // XXX
    	AddVectorObs((this.transform.position.x - 5)/5); // XXX
    	AddVectorObs((this.transform.position.z + 5)/5); // XXX
    	AddVectorObs((this.transform.position.z - 5)/5); // XXX

		// Velocity
    	AddVectorObs(rigidbody.velocity.x/5);
    	AddVectorObs(rigidbody.velocity.z/5);
	}

	public float speed = 10;
	private float previousDistance = float.MaxValue;

	public override void AgentAction(float[] vectorAction, string textAction) {
		float distanceToTarget = Vector3.Distance(this.transform.position, Target.position);

		// Reached target
		if (distanceToTarget < 1f) { // XXX
			//Done();
			AddReward(1.0f);
		}

		// Getting closer
		if (distanceToTarget < previousDistance) {
			AddReward(0.1f);
		}

		// Time penalty
		AddReward(-0.05f);

		// Fell off
		if (this.transform.position.y < -2.0) { // XXX
			Done();
			AddReward(-1.0f);
		}

		previousDistance = distanceToTarget;
    	// XXX Actions, size = 2
    	var controlSignal = Vector3.zero;
    	controlSignal.x = Mathf.Clamp(vectorAction[0], -1, 1);
    	controlSignal.z = Mathf.Clamp(vectorAction[1], -1, 1);
    	rigidbody.AddForce(controlSignal * speed);
	}
}
