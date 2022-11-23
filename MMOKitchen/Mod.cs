using KitchenLib;
using System.Reflection;
using BepInEx;
namespace MMOKitchen
{
	[BepInProcess("PlateUp.exe")]
	[BepInPlugin("starfluxgames.mmokitchen", "MMO Kitchen", "0.1.3")]
	public class Mod : BaseMod
	{
		public Mod() : base("1.1.0", Assembly.GetExecutingAssembly()) { }
	}
}