using UnityEngine;
using System;
using Object = UnityEngine.Object;

public static class Logger {

	public static string ColorString(this string myStr, string color) {

		return $"<color={color}>{myStr}</color>";
	}

	private static void DoLog(Action<string,Object> LogFunction, string prefix, Object myObject, params object[] msg) {

		#if UNITY_EDITOR
		LogFunction($"{prefix}[{myObject.name.ColorString("lightblue")}]: {String.Join(',',msg)}\n", myObject);
		#endif
	}

	public static void Log(this Object myObject, params object[] msg) {

		DoLog(Debug.Log, "", myObject, msg);
	}

	public static void LogError(this Object myObject, params object[] msg) {

		DoLog(Debug.LogError, " <!> ".ColorString("red"), myObject, msg);
	}

	public static void LogWarning(this Object myObject, params object[] msg) {

		DoLog(Debug.LogWarning, " ⚠ ".ColorString("yellow"), myObject, msg);
	}

	public static void LogSuccess(this Object myObject, params object[] msg) {

		DoLog(Debug.Log, " ✔️ ".ColorString("green"), myObject, msg);
	}
}
