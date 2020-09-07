using UnityEngine;
using System.Collections;

public class TargetPointer : MonoBehaviour {

	public Transform Target;

	void Update() 
	{
		transform.LookAt(Target);
		transform.Rotate(90f, 0, 0, Space.Self);
	}
}