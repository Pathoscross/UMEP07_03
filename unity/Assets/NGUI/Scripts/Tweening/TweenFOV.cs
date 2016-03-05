//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright 穢 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Tween the camera's field of view.
/// </summary>

[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/Tween/Tween Field of View")]
public class TweenFOV : UITweener
{
	public float from = 45f;
	public float to = 45f;

	Camera mCam;

	/// <summary>
	/// Camera that's being tweened.
	/// </summary>

	public Camera cachedCamera { get { if (mCam == null) mCam = GetComponent<Camera>(); return mCam; } }

	/// <summary>
	/// Current field of view value.
	/// </summary>

	public float fov { get { return cachedCamera.fieldOfView; } set { cachedCamera.fieldOfView = value; } }

	/// <summary>
	/// Perform the tween.
	/// </summary>

	protected override void OnUpdate (float factor, bool isFinished)
	{
		cachedCamera.fieldOfView = from * (1f - factor) + to * factor;
	}

	/// <summary>
	/// Start the tweening operation.
	/// </summary>

	static public TweenFOV Begin (GameObject go, float duration, float to)
	{
		TweenFOV comp = UITweener.Begin<TweenFOV>(go, duration);
		comp.from = comp.fov;
		comp.to = to;

		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
		return comp;
	}
}
