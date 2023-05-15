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

        private bool isAlive = true;

        private int width = 20;
        private int height = 20;

        private int speed = 200;

        private Vector2 vectorDirection = new Vector2(1, 0);

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public bool IsAlive { get { return isAlive; } set { isAlive = value; } }

        public Vector2 Position
        {
            get
            {
                return new Vector2(destinationRectangle.X, destinationRectangle.Y);
            }
            set
            {
                destinationRectangle.X = (int)value.X;
                destinationRectangle.Y = (int)value.Y;
            }
        }

        public Rectangle Collision
        {
            get
            {
                return destinationRectangle;
            }
        }

        public Bullet(Vector2 vectorDirection)
        {
            texture = null;
            this.vectorDirection = vectorDirection;
            destinationRectangle = new Rectangle(0, 0, width, height);
        }

        public Bullet(Texture2D texture, Vector2 vectorDirection)
        {
            this.texture = texture;
            this.vectorDirection = vectorDirection;
            destinationRectangle = new Rectangle(0, 0, width, height);
        }

        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("asteroid");
        }

        public void Update(GameTime gameTime)
        {
            //destinationRectangle.Y -= speed;

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 vectorVelocity = vectorDirection * speed;

            Position = Position + vectorVelocity * delta;

            if (Position.Y < - height || Position.X < -width ||
                Position.Y > 600 || Position.X > 800)
            {
                isAlive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, Color.Blue);
        }
    }
}

