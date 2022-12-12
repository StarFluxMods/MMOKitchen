using KitchenLib;
using System.Reflection;
using KitchenLib.Utils;
using UnityEngine;
using System;
#if BEPINEX
using BepInEx;
#endif

namespace MMOKitchen
{
#if BEPINEX
	[BepInProcess("PlateUp.exe")]
	[BepInPlugin(MOD_ID, MOD_NAME, MOD_VERSION)]
#endif
	public class Main : BaseMod
	{
		public const string MOD_ID = "mmokitchen";
		public const string MOD_NAME = "MMO Kitchen";
		public const string MOD_AUTHOR = "StarFluxGames";
		public const string MOD_VERSION = "0.1.5";
		public const string MOD_COMPATIBLE_VERSIONS = "1.1.2";
		public Main() : base(MOD_ID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_COMPATIBLE_VERSIONS, Assembly.GetExecutingAssembly()) { }

		public static Texture2D LoadImage(string base64)
		{
			byte[] bytes = Convert.FromBase64String(base64);

			Texture2D image = ResourceUtils.LoadTextureRaw(bytes);

			return image;
		}
	}
}