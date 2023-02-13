using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SpaceWar_M2.Classes
{
    public class Player
    {
        // fields
        private Texture2D texture;
        private Vector2 position;

        // methods
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("player");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}

