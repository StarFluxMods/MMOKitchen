using KitchenLib;
using System.Reflection;
#if BEPINEX
using BepInEx;
#endif
#if MELONLOADER
using MelonLoader;
#endif

#if MELONLOADER
[assembly: MelonInfo(typeof(MMOKitchen.Mod), "MMO Kitchen", "0.1.4", "StarFluxGames")]
[assembly: MelonGame("It's Happening", "PlateUp")]
#endif
namespace MMOKitchen
{
#if BEPINEX
	[BepInProcess("PlateUp.exe")]
	[BepInPlugin("starfluxgames.mmokitchen", "MMO Kitchen", "0.1.4")]
	[BepInDependency(KitchenLib.Mod.GUID)]
#endif
	public class Mod : BaseMod
	{
#if MELONLOADER
		public Mod() : base("mmokitchen", "1.1.1") { }
#endif
#if BEPINEX
		public Mod() : base("1.1.1", Assembly.GetExecutingAssembly()) { }
#endif
	}
}