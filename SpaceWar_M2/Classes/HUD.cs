using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using SpaceWar_M2.Classes.Components;

namespace SpaceWar_M2.Classes
{
	public class HUD
	{
		private HealthBar healthBar;

		public HUD()
		{
			healthBar = new HealthBar(new Vector2(20, 20), 10, 200, 25);
		}

		public void LoadContent(ContentManager content)
		{
			healthBar.LoadContent(content);
		}

		public void Update()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			healthBar.Draw(spriteBatch);
		}


		public void OnPlayerTakeDamage()
		{
			healthBar.Width = 10;
		}
	}
}

