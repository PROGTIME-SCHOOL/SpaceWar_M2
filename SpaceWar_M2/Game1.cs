using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;    // for list
using System;

using SpaceWar_M2.Classes;
using SpaceWar_M2.Classes.Components;

namespace SpaceWar_M2;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public static GameMode gameMode = GameMode.Playing;

    private int screenWidth = 800;
    private int screenHeight = 600;

    private Player player;
    private Space space;

    private List<Asteroid> asteroids;
    private List<Explosion> explosions;

    private Label label;

    private MainMenu mainMenu = new MainMenu();

    private GameOver gameOver = new GameOver();

    private HUD hud = new HUD();

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

        label = new Label("Hello, World!!!", Vector2.Zero, Color.White);

        player.TakeDamage += hud.OnPlayerTakeDamage;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        player.LoadContent(Content);
        space.LoadContent(Content);

        label.LoadContent(Content);

        mainMenu.LoadContent(Content);

        gameOver.LoadContent(Content);

        hud.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        // TODO: Add your update logic here

        switch (gameMode)
        {
            case GameMode.Playing:

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    gameMode = GameMode.Menu;
                }

                space.Speed = 1;
                player.Update(Content);
                space.Update();
                UpdateAsteroids();
                UpdateExplosions(gameTime);
                CheckCollision();
                hud.Update();
                break;

            case GameMode.Menu:
                mainMenu.Update();
                space.Speed = 0.5f;
                space.Update();
                break;

            case GameMode.GameOver:
                gameOver.Update();
                space.Speed = 0.5f;
                space.Update();
                break;

            case GameMode.Exit:
                Exit();
                break;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        switch (gameMode)
        {
            case GameMode.Playing:
                space.Draw(_spriteBatch);
                player.Draw(_spriteBatch);

                foreach (Asteroid asteroid in asteroids)
                {
                    asteroid.Draw(_spriteBatch);
                }

                foreach (var explosion in explosions)
                {
                    explosion.Draw(_spriteBatch);
                }

                //label.Draw(_spriteBatch);

                hud.Draw(_spriteBatch);

                break;

            case GameMode.Menu:
                space.Draw(_spriteBatch);
                mainMenu.Draw(_spriteBatch);
                break;

            case GameMode.GameOver:
                space.Draw(_spriteBatch);
                gameOver.Draw(_spriteBatch);
                break;
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

                player.Damage();

                Explosion explosion = new Explosion(asteroid.Position);
                explosion.LoadContent(Content);
                explosions.Add(explosion);
            }

            // each asteroid and each bullet
            foreach (var bullet in player.Bullets)
            {
                if (asteroid.Collision.Intersects(bullet.Collision))
                {
                    asteroid.IsAlive = false;
                    bullet.IsAlive = false;

                    Explosion explosion = new Explosion(asteroid.Position);
                    explosion.LoadContent(Content);
                    explosions.Add(explosion);
                }
            }
        }
    }

    private void UpdateExplosions(GameTime gameTime)
    {
        for (int i = 0; i < explosions.Count; i++)
        {
            explosions[i].Update(gameTime);

            if (!explosions[i].IsAlive)
            {
                explosions.RemoveAt(i);
                i--;
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

