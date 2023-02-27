using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SpaceWar_M2.Classes
{
    public class Bullet
    {
        private Texture2D texture;
        private Rectangle destinationRectangle;

        public Bullet()
        {
            texture = null;
            destinationRectangle = new Rectangle(0, 0, 20, 20);
        }

        public Bullet(Vector2 position)
        {
            texture = null;
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 20, 20);
        }

        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("asteroid");
        }

        public void Update()
        {
            destinationRectangle.Y -= 3;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, Color.Blue);
        }
    }
}

