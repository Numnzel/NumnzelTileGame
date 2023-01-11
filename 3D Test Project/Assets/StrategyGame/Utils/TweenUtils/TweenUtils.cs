using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public static class TweenUtils {

    public static void TweenMoveUnit(Unit unit, Vector3 endPos, System.Func<float,float> tweenScale) {

		Vector3 startPos = unit.transform.position;

		System.Action<ITween<Vector3>> updateUnitPos = (t) =>
		{
			unit.transform.position = t.CurrentValue;
		};

		unit.gameObject.Tween($"TweenMoveUnit:{unit.GetHashCode()}", startPos, endPos, 3f, tweenScale, updateUnitPos);
	}
}
