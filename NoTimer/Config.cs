using BepInEx;
using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoTimer
{
    class Configs
    {
        public static ConfigEntry<bool> loopEvents;
        public static ConfigFile config;
        public static void Create()
        {
            config = new ConfigFile(Paths.ConfigPath + "/rost.moment.baldiplus.notimer.cfg", true);
            loopEvents = config.Bind<bool>("General", "LoopEvents", false, "If true, events will be looped at the floor");
        }
    }
}
