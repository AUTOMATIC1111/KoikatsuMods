using System;
using BepInEx;
using Harmony;

namespace Demosaic
{
    public static class Hooks
    {
        public static void InstallHooks()
        {
            try
            {
                var harmony = HarmonyInstance.Create("info.jbcs.koikatsu.demosaic");
                harmony.PatchAll(typeof(Hooks));
            }
            catch (Exception )
            {
                //BepInLogger.Log(e.ToString(), false);
            }
        }

        [HarmonyPrefix, HarmonyPatch(typeof(ChaControl), "LateUpdateForce")]
        public static void LateUpdateForce(ChaControl __instance)
        {
            __instance.hideMoz = true;
        }

    }

    [BepInPlugin(GUID: "info.jbcs.koikatsu.demosaic", Name: "Demosaic", Version: "1.0.0")]
    public class Demosaic : BaseUnityPlugin
    {
        void Awake()
        {
            Hooks.InstallHooks();
        }
    }
}
