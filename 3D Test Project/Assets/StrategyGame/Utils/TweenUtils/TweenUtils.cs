using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public static class TweenUtils {

    public static void TweenMove(GameObject gobj, Vector3 endPos, System.Func<float,float> tweenScale, float duration = 0.2f) {

		Vector3 startPos = gobj.transform.position;

		System.Action<ITween<Vector3>> updateUnitPos = (t) => {

			gobj.transform.position = t.CurrentValue;
		};

		gobj.Tween($"TweenMove:{gobj.GetHashCode()}", startPos, endPos, duration, tweenScale, updateUnitPos);
	}
}
