using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace Jamdofai
{
    [HarmonyPatch(typeof(Text), "text", MethodType.Setter)]
    public static class SetTextPatch1
    {
        public static void Prefix(ref string value, Text __instance)
        {
            Translated t = __instance.gameObject.GetOrAddComponent<Translated>();
            if (t.prev_text == value || value.IsNullOrEmpty())
                return;
            t.prev_text = value;
            value = JamTranslator.Translate(value);
        }
    }

    [HarmonyPatch(typeof(TextMesh), "text", MethodType.Setter)]
    public static class SetTextPatch2
    {
        public static void Prefix(ref string value, TextMesh __instance)
        {
            Translated t = __instance.gameObject.GetOrAddComponent<Translated>();
            if (t.prev_text == value || value.IsNullOrEmpty())
                return;
            t.prev_text = value;
            value = JamTranslator.Translate(value);
        }
    }
}
