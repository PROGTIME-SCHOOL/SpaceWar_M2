using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SpaceWar_M2.Classes
{
    public enum TypePlayer { Beginner, Intermediate, Advanced, Pro, God }

    public class Player
    {
        // fields
        private Texture2D texture;
        private Vector2 position;
        private TypePlayer typePlayer;
        private float speed;

        // constructor
        public Player()
        {
            texture = null;
            position = new Vector2(50, 50);
            typePlayer = TypePlayer.Beginner;
            speed = 10;
        }

        // methods
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("player");
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            #region Movement
            if (keyboardState.IsKeyDown(Keys.D))
            {
                position.X += speed;
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                position.X -= speed;
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                position.Y -= speed;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                position.Y += speed;
            }
            #endregion

            #region Bounds
            if (position.X < 0)
            {
                position.X = 0;
            }

            if (position.Y < 0)
            {
                position.Y = 0;
            }

            if (position.X + texture.Width > 800)
            {
                position.X = 800 - texture.Width;
            }

            if (position.Y + texture.Height > 600)
            {
                position.Y = 600 - texture.Height;
            }
            #endregion

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}

