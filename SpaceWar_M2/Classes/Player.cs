using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

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

        private Rectangle collision;

        // weapon
        private List<Bullet> bulletList = new List<Bullet>();  // магазин пятерочка

        // time
        private int time = 0;
        private int maxTime = 30;

        // properties

        public Rectangle Collision { get { return collision; } }

        public List<Bullet> Bullets
        {
            get { return bulletList; }
        }

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

        public void Update(ContentManager content)
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

            // collision
            collision = new Rectangle((int)position.X, (int)position.Y,
                texture.Width, texture.Height);

            // weapon

            time++;

            if (time > maxTime)
            {
                // generate bullet
                Bullet bullet = new Bullet();

                bullet.Position = new Vector2(position.X + texture.Width / 2 - bullet.Width / 2,
                    position.Y - bullet.Height/2);

                bullet.LoadContent(content);

                bulletList.Add(bullet);

                time = 0;
            }

            // работа со всеми пульками в игре
            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i];
                bullet.Update();
            }

            // зачистка листа
            for (int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].IsAlive)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);

            foreach (var bullet in bulletList)
            {
                bullet.Draw(spriteBatch);
            }
        }
    }
}

