using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using SpaceWar_M2.Classes.Components;

namespace SpaceWar_M2.Classes
{
    public class MainMenu
    {
        private List<Label> buttonList = new List<Label>();
        private int selected;
        private KeyboardState keyboardState;        // нынешнее состояние клавиатуры
        private KeyboardState prevKeyboardState;    // предыдущее состояние клавиатуры

        private Vector2 position = new Vector2(400, 200);

        public MainMenu()
        {
            selected = 1;

            buttonList.Add(new Label("Play", position, Color.White));
            buttonList.Add(new Label("Exit Exit Exit", new Vector2(position.X, position.Y + 40),
                Color.White));
        }

        public void LoadContent(ContentManager content)
        {
            foreach (var item in buttonList)
            {
                item.LoadContent(content);
            }

            // magic line = position

            foreach (var label in buttonList)
            {
                label.Position = new Vector2(label.Position.X - label.Width / 2,
                    label.Position.Y);
            }
        }

        public void Update()
        {
            keyboardState = Keyboard.GetState();

            // отпустил клавишу S
            if (prevKeyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyUp(Keys.S))
            {
                if (selected < buttonList.Count - 1)
                {
                    selected++;
                }
            }

            // нажал клавишу W
            if (prevKeyboardState.IsKeyUp(Keys.W) && keyboardState.IsKeyDown(Keys.W))
            {
                if (selected > 0)
                {
                    selected--;
                }
            }

            prevKeyboardState = keyboardState;


            // Event Click
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                if (selected == 0)
                {
                    Game1.gameMode = GameMode.Playing;
                }
                else if (selected == 1)
                {
                    Game1.gameMode = GameMode.Exit;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color colorSelected;
            for (int i = 0; i < buttonList.Count; i++)
            {
                if (selected == i)
                {
                    colorSelected = Color.Yellow;
                }
                else
                {
                    colorSelected = Color.White;
                }

                buttonList[i].Color = colorSelected;
                buttonList[i].Draw(spriteBatch);
            }
        }
    }
}

