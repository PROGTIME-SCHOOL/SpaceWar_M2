using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace SpaceWar_M2.Classes
{
	public class Explosion
	{
		private Texture2D texture;  // 1983  x  117
        private Vector2 position;

		private Rectangle sourceRectangle;   // нужно для рисования области текстуры

		private int frameNumber = 0;
		private int frameWidth = 117;
		private int frameHeight = 117;

		private double timeTotalSeconds = 0;
		private double duration = 0.2;

		private bool isLoop = true;

		public Explosion(Vector2 position)
		{
			texture = null;

			this.position = position;
		}

		public void LoadContent(ContentManager manager)
		{
			texture = manager.Load<Texture2D>("explosion3");
		}

		public void Update(GameTime gameTime)
		{
			timeTotalSeconds += gameTime.ElapsedGameTime.TotalSeconds;

			if (timeTotalSeconds > duration)
			{
				frameNumber++;

				timeTotalSeconds = 0;
            }

			// loop
			if (frameNumber == 17 && isLoop)
			{
				frameNumber = 0;
			}

            sourceRectangle = new Rectangle(frameNumber * frameWidth, 0, frameWidth, frameHeight);

            Debug.WriteLine("Time: " + gameTime.ElapsedGameTime.Seconds);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, position, sourceRectangle, Color.White);
		}
	}
}