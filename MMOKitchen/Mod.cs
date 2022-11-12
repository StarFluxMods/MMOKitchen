using KitchenLib;
using System.Reflection;
#if BepInEx
using BepInEx;
#endif
#if MelonLoader
using MelonLoader;
#endif

#if MelonLoader
[assembly: MelonInfo(typeof(MMOKitchen.Mod), "MMO Kitchen", "0.1.3", "StarFluxGames")]
[assembly: MelonGame("It's Happening", "PlateUp")]
#endif
namespace MMOKitchen
{	
    #if BepInEx
    [BepInProcess("PlateUp.exe")]
	[BepInPlugin("starfluxgames.mmokitchen", "MMO Kitchen", "0.1.3")]
	#endif
    public class Mod : BaseMod
    {
		#if MelonLoader
		public Mod() : base("mmokitchen", "1.1.0") { }
		#endif
		#if BepInEx
		public Mod() : base("1.1.0", Assembly.GetExecutingAssembly()) {}
		#endif
    }
}