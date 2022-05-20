using System.Collections.Generic;
using UnityEngine;

public static class Extension {
    public static bool IsNaN(this float value) => float.IsNaN(value);
    public static int TakeSignOnly(this float value) => value > 0 ? 1 : -1;
    public static List<string> ToNameOnlyList(this List<JumperController> controllers) {
        List<string> nameList = new List<string>();
        foreach(JumperController controller in controllers)
            nameList.Add(controller.gameObject.name);
        return nameList;
    }
}