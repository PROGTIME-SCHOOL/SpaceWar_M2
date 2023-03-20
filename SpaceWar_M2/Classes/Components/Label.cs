﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SpaceWar_M2.Classes.Components
{
	public class Label
	{
		private SpriteFont spriteFont;
		private Vector2 position;
		private Color color;
		private string text;

		public Label(string text, Vector2 position, Color color)
		{
			spriteFont = null;

			this.color = color;
			this.position = position;
			this.text = text;
		}

		public void LoadContent(ContentManager manager)
		{
			spriteFont = manager.Load<SpriteFont>("GameFont");
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(spriteFont, text, position, color);
		}
	}
}

