using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public GameObject player;
	public float timeOffset;
	public Vector3 posOffset;

	private Vector3 velocity;

	public static CameraFollow instance;

	private void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}

		if (instance != null)
		{
			Debug.LogWarning("Il y a plus d'un instance de CameraFollow dans la scène");
			return;
		}

		instance = this;
	}

	public Transform camTransform;

	public float shakeDuration = 0f;
	public float shakeAmount = 0.3f;
	public float decreaseFactor = 1.0f;

	Vector3 originalPos;

	void Update()
	{
		transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
		originalPos = transform.position;
		if (shakeDuration > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
		}
	}
}
