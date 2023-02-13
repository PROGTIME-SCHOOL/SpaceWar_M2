using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace SpaceWar_M2.Classes
{
    public class Asteroid
    {
        private Texture2D texture;
        private Vector2 position;

        public Asteroid()
        {
            texture = null;
            position = Vector2.Zero;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("asteroid");
        }

        public void Update() { }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}

