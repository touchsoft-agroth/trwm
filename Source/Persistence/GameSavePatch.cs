using HarmonyLib;

namespace trwm.Source.Persistence
{
    [HarmonyPatch(typeof(Saver), "Save")]
    public static class GameSavePatch
    {
        private static void Prefix()
        {
            ModDataStorage.Persist();
        }

        private static void Postfix()
        {
            
        }
    }
}