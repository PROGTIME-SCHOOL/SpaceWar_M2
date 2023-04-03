using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using SpaceWar_M2.Classes.Components;

namespace SpaceWar_M2.Classes
{
	public class GameOver
	{
		private Label label;

		public GameOver()
		{
			label = new Label("GAME OVER", new Vector2(250, 200), Color.White);
		}

		public void LoadContent(ContentManager manager)
		{
			label.LoadContent(manager);
		}

		public void Update()
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			label.Draw(spriteBatch);
		}
	}
}

