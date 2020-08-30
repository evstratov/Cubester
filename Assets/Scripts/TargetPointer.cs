using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetPointer : MonoBehaviour {

	public Transform Finish;
	public Transform Cube;

	public float StartAppearingRange = 1;
	public float EndAppearingRange = 5;

	private Image imageComponent;

	private void Start()
	{
		imageComponent = GetComponent<Image>();
	}

	void FixedUpdate() 
	{
		float distance = Finish.position.magnitude;
		float a = Mathf.Clamp01((distance - StartAppearingRange) / (EndAppearingRange - StartAppearingRange));
		imageComponent.color = new Color(0.96f, 0.77f, 0.41f, a);

		transform.localRotation = Quaternion.Euler(0,0, Vector3.SignedAngle(Finish.position, Cube.position, Vector3.up));
	}
}