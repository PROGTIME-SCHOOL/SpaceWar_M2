using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;    // for list
using System;

using SpaceWar_M2.Classes;

namespace SpaceWar_M2;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private int screenWidth = 800;
    private int screenHeight = 600;

    private Player player;
    private Space space;

    private List<Asteroid> asteroids;
    private List<Explosion> explosions;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        // Config
        _graphics.PreferredBackBufferWidth = screenWidth;
        _graphics.PreferredBackBufferHeight = screenHeight;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        player = new Player();
        space = new Space();
        //asteroid = new Asteroid();

        asteroids = new List<Asteroid>();
        explosions = new List<Explosion>();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        player.LoadContent(Content);
        space.LoadContent(Content);

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        player.Update(Content);
        space.Update();

        UpdateAsteroids();

        CheckCollision();


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        space.Draw(_spriteBatch);
        player.Draw(_spriteBatch);
        //asteroid.Draw(_spriteBatch);

        foreach (Asteroid asteroid in asteroids)
        {
            asteroid.Draw(_spriteBatch);
        }


        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void CheckCollision()
    {
        foreach (var asteroid in asteroids)
        {
            // each asteroid and player
            if (player.Collision.Intersects(asteroid.Collision))
            {
                asteroid.IsAlive = false;
            }

            // each asteroid and each bullet
            foreach (var bullet in player.Bullets)
            {
                if (asteroid.Collision.Intersects(bullet.Collision))
                {
                    asteroid.IsAlive = false;
                    bullet.IsAlive = false;
                }
            }
        }
    }


    private void UpdateAsteroids()
    {
        // работа с астеройдами, которые уже есть в игре
        for (int i = 0; i < asteroids.Count; i++)
        {
            Asteroid asteroid = asteroids[i];

            asteroid.Update();

            // teleport
            if (asteroid.Position.Y > screenHeight)
            {
                Random random = new Random();
                int y = random.Next(-screenHeight, 0 - asteroid.Height);
                int x = random.Next(0, screenWidth - asteroid.Width);

                asteroid.Position = new Vector2(x, y);
            }


            if (!asteroid.IsAlive)
            {
                asteroids.Remove(asteroid);
                i--;
            }
        }

        // загрузка доп астеройдов в игру
        if (asteroids.Count < 10)
        {
            LoadAsteroid();
        }
    }

    private void LoadAsteroid()
    {
        // создаем новый астеройд
        Asteroid asteroid = new Asteroid(Vector2.Zero);
        asteroid.LoadContent(Content);

        // установить астеройд по рандомной позиции
        int rectagleWidth = screenWidth;
        int rectangleHeight = screenHeight;

        Random random = new Random();

        int x = random.Next(0, rectagleWidth - asteroid.Width);
        int y = random.Next(0, rectangleHeight - asteroid.Height);

        asteroid.Position = new Vector2(x, -y);

        // добавляем астеройд в лист
        asteroids.Add(asteroid);
    }
}

