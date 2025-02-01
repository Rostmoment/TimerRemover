using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace NoTimer
{
    [HarmonyPatch]
    class Patches
    {
        [HarmonyPatch(typeof(EnvironmentController), "EventOver")]
        [HarmonyPostfix]
        private static void ResetLoop(RandomEvent randomEvent, EnvironmentController __instance, ref List<RandomEvent> ___events)
        {
            if (__instance.events.Last().Equals(randomEvent) && Configs.loopEvents.Value)
            {
                __instance.ResetEvents();
                __instance.StartEventTimers();
            }
        }

        [HarmonyPatch(typeof(EnvironmentController), "RemainingTime", MethodType.Getter)]
        [HarmonyPostfix]
        private static void InfiniteTime(ref int __result) => __result = int.MaxValue;

        [HarmonyPatch(typeof(EnvironmentController), "StartEventTimers")]
        [HarmonyPrefix]
        private static void RemoveTimeoutEvent(EnvironmentController __instance)
        {
            int count = __instance.events.Count;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    if (__instance.events[i].eventType == RandomEventType.TimeOut)
                    {
                        __instance.events.RemoveAt(i);
                        __instance.eventTimes.RemoveAt(i);
                    }
                }
                catch (IndexOutOfRangeException) { }
                catch (ArgumentOutOfRangeException) { }
            }
        }
        [HarmonyPatch(typeof(BaldiTV), "ShowLevelTimeWarning")]
        [HarmonyPrefix]
        private static bool DisableTVWarning() => false;

        [HarmonyPatch(typeof(Elevator), "Update")]
        [HarmonyPostfix]
        private static void ShowPlayerTime(Elevator __instance)
        {
            string currentTime = DateTime.Now.ToString("HH:mm").Replace(":", "");
            for (int i = 0; i < currentTime.Length; i++) 
            {
                foreach (DigitalClock clock in __instance.clock)
                {
                    clock.SetColor(Color.green);
                    clock.digit[i].sprite = clock.sprite[int.Parse(currentTime[i].ToString())];
                }
                __instance.ec.map.clock.SetColor(Color.green);
                __instance.ec.map.clock.uiDigit[i].sprite = __instance.ec.map.clock.sprite[int.Parse(currentTime[i].ToString())];

            }
        }

        /*
        [HarmonyPatch(typeof(Map), "Update")]
        [HarmonyPostfix]
        private static void ShowPlayerTime(Map __instance)
        {
            string currentTime = DateTime.Now.ToString("HH:mm").Replace(":", "");
            for (int i = 0; i < currentTime.Length; i++)
            {
                __instance.clock.SetColor(Color.green);
                try
                {
                    __instance.clock.digit[i].sprite = __instance.Ec.elevators.First().clock.First().sprite[int.Parse(currentTime[i].ToString())];
                }
                catch
                {
                    Debug.Log(i);
                }
            }
        }*/
    }
}
