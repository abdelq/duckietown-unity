using UnityEngine;

public class FollowCamera : MonoBehaviour {
	public Transform target;
	//public float damping = 1;
	private Vector3 offset;

	void Start () {
		offset = transform.position - target.position;
	}

	// https://gist.github.com/Hamcha/6096905
	// https://github.com/Brackeys/Smooth-Camera-Follow/blob/master/Smooth%20Camera/Assets/CameraFollow.cs
	// https://answers.unity.com/questions/1077171/how-to-make-camera-follow-player-position-and-rota.html
	// https://kylewbanks.com/blog/unity-make-camera-follow-player-smoothly-and-fluidly
	// https://www.salusgames.com/2016/12/28/smooth-2d-camera-follow-in-unity3d/
	// https://answers.unity.com/questions/831785/how-to-make-the-main-camera-switch-targets-during.html
	void LateUpdate() {
		transform.position = target.position + offset;
		//var currentAngle = transform.eulerAngles.y;
		//var desiredAngle = target.transform.eulerAngles.y;

		//var angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * damping);
		//var rotation = Quaternion.Euler(0, angle, 0);

		//transform.position = target.transform.position - rotation * offset;
		//transform.position = target.transform.position - offset;
		//transform.LookAt(target.transform);
		/*if(target) {
			if(smooth) {
				//Look at and dampen the rotation
				Quaternion rotation = Quaternion.LookRotation(target.position - _myTransform.position);
				_myTransform.rotation = Quaternion.Slerp(_myTransform.rotation, rotation, Time.deltaTime * damping);
			} else { //Just look at
				_myTransform.rotation = Quaternion.FromToRotation(-Vector3.forward, (new Vector3(target.position.x, target.position.y, target.position.z) - _myTransform.position).normalized);
		
				float distance = Vector3.Distance(target.position, _myTransform.position);
		
				if(distance < minDistance) {
					alpha = Mathf.Lerp(alpha, 0.0f, Time.deltaTime * 2.0f);
				} else {
					alpha = Mathf.Lerp(alpha, 1.0f, Time.deltaTime * 2.0f);
				}
			}
		}*/
	}
}
