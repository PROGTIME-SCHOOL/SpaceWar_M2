using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace SpaceWar_M2.Classes
{
	public class Weapon
	{
        private Texture2D textureBullet;
        private Vector2 position;
        private Vector2 vectorDirection = new Vector2(-1, 0);

        private List<Bullet> listBullet = new List<Bullet>();

        private int time = 0;

        private int maxTime = 30;  // ms

        public Weapon()
        {
            position = new Vector2(200, 200);
        }

        public Weapon(Vector2 position, Vector2 vectorDirection)
        {
            this.position = position;
            this.vectorDirection = vectorDirection;
        }

        public void LoadContent(ContentManager content)
        {
            textureBullet = content.Load<Texture2D>("asteroid");
        }

        public void Update(GameTime gameTime)
        {
            // weapon
            time++;

            if (time > maxTime)
            {
                // generate bullet
                Bullet bullet = new Bullet(textureBullet, vectorDirection);

                bullet.Position = position;

                listBullet.Add(bullet);

                time = 0;
            }

            // работа со всеми пульками в игре
            for (int i = 0; i < listBullet.Count; i++)
            {
                Bullet bullet = listBullet[i];
                bullet.Update(gameTime);
            }

            // зачистка листа
            for (int i = 0; i < listBullet.Count; i++)
            {
                if (!listBullet[i].IsAlive)
                {
                    listBullet.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var bullet in listBullet)
            {
                bullet.Draw(spriteBatch);
            }
        }
    }
}

