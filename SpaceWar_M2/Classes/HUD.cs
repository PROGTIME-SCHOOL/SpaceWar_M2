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
		private Label labelScore;

		public HUD()
		{
			Vector2 position = new Vector2(20, 20);

            healthBar = new HealthBar(position, 10, 200, 25);

			labelScore = new Label("Score: 0",
				new Vector2(position.X + healthBar.DestinationRectangle.Width + 20, position.Y),
				Color.White);
		}

		public void LoadContent(ContentManager content)
		{
			healthBar.LoadContent(content);
			labelScore.LoadContent(content);
		}

		public void Update()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			healthBar.Draw(spriteBatch);
			labelScore.Draw(spriteBatch);
		}


		public void OnPlayerTakeDamage()
		{
			healthBar.NumParts--;
		}
	}
}

