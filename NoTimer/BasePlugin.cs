using BepInEx;
using HarmonyLib;
using MTM101BaldAPI;
using MTM101BaldAPI.OptionsAPI;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NoTimer
{
    [BepInPlugin("rost.moment.baldiplus.notimer", "No Timer", "0.2")]
    public class BasePlugin : BaseUnityPlugin
    {
        public static BasePlugin Instance;
        private void Awake()
        {
            Harmony harmony = new Harmony("rost.moment.baldiplus.notimer");
            harmony.PatchAll();
            Configs.Create();
            if (Instance == null)
            {
                Instance = this;
            }
        }
    }
}