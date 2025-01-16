﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Final_Project
{
    public class Game1 : Game
    {
        enum Screen
        {
            Intro,
            Betting,
            Blue,
            Green,
            Game
        }
        MouseState mouseState;
        Random random;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Screen screen;
        KeyboardState keyboardState;
        Texture2D track, carBlue, carGreen, introScreen, carYellow, bettingScreen, BlueScreen, greenScreen, backBtn;
        Rectangle trackRect1, trackRect2, carBlueRect, carGreenRect, carYellowRect;
        Rectangle carBlueRectBet, carGreenRectbet, carYellowRectBet, backBtnRect;
        int scrollSpeed = 1;
        int maxSpeed = 20;
        int acceleration = 1;
        float seconds;
        
        Vector2 carGreenSpeed, carBlueSpeed;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screen = Screen.Intro;
            
            
            int screenWidth = GraphicsDevice.Viewport.Width;
            int trackHeight = 1000;
            
            //Game Rects
            trackRect1 = new Rectangle(0, 0, screenWidth, trackHeight);
            trackRect2 = new Rectangle(0, -trackHeight, screenWidth, trackHeight);
            carBlueRect = new Rectangle(90, 230, 350, 233);
            carGreenRect = new Rectangle(340, 210, 350, 255);
            carYellowRect = new Rectangle(560, 290, 190, 190);

            //Betting Rects
            carBlueRectBet = new Rectangle(90, 170, 350, 233);
            carGreenRectbet = new Rectangle(340, 150, 350, 255);
            carYellowRectBet = new Rectangle(560, 230, 190, 190);

            //Back Button
            backBtnRect = new Rectangle(10, 10, 50, 50);

            //carBlueSpeed = new Vector2(0,2);
            //carGreenSpeed = new Vector2(0,2);
            random = new Random();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            track = Content.Load<Texture2D>("Track");
            carBlue = Content.Load<Texture2D>("carBlue1");
            carGreen = Content.Load<Texture2D>("carGreen1");
            carYellow = Content.Load<Texture2D>("yellowCar");
            introScreen = Content.Load<Texture2D>("RouletteBack");
            bettingScreen = Content.Load<Texture2D>("Betground");
            BlueScreen = Content.Load<Texture2D>("BlueBoy");
            backBtn = Content.Load<Texture2D>("BackBtn");
            greenScreen = Content.Load<Texture2D>("GreenGoblin");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();


            if (screen == Screen.Intro)
            {

                if (keyboardState.GetPressedKeyCount() > 0)
                    screen = Screen.Betting;

            }
            if (screen == Screen.Betting)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (carGreenRectbet.Contains(mouseState.Position))
                    {
                        
                        screen = Screen.Game;
                    }
                    if (carBlueRectBet.Contains(mouseState.Position))
                    {
                        
                        screen = Screen.Blue;
                    }    
                    if (carYellowRectBet.Contains(mouseState.Position))
                    {
                        
                        screen = Screen.Game;
                    }


                }
            }
            if (screen == Screen.Blue)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    if (backBtnRect.Contains(mouseState.Position))
                        screen = Screen.Betting;

            }
            if (screen == Screen.Game)
            {

                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                scrollSpeed += (acceleration * (int)seconds);



                //Track:
                if (scrollSpeed > maxSpeed)
                    scrollSpeed = maxSpeed; //max

                trackRect1.Y += scrollSpeed;
                trackRect2.Y += scrollSpeed;

                int screenHeight = GraphicsDevice.Viewport.Height;

                if (trackRect1.Y >= screenHeight)
                    trackRect1.Y = trackRect2.Y - trackRect1.Height;
                if (trackRect2.Y >= screenHeight)
                    trackRect2.Y = trackRect1.Y - trackRect2.Height;

                //Car speeds:
                if (screen == Screen.Game && seconds >= 2)
                {
                    carBlueRect.Y -= random.Next(-2, 4);
                    carGreenRect.Y -= random.Next(-2, 4);
                    carYellowRect.Y -= random.Next(-2, 4);
                }



                
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                
                _spriteBatch.Draw(introScreen, new Rectangle(0, 0, 800, 500), Color.White);
              
            }
            if (screen == Screen.Betting)
            {
                _spriteBatch.Draw(bettingScreen, new Rectangle(0,0,800,600), Color.White);
                _spriteBatch.Draw(carBlue, carBlueRectBet, Color.White);
                _spriteBatch.Draw(carGreen, carGreenRectbet, Color.White);
                _spriteBatch.Draw(carYellow, carYellowRectBet, Color.White);
            }
            if (screen == Screen.Blue)
            {
                _spriteBatch.Draw(BlueScreen, new Rectangle(0, 0, 800, 600), Color.White);
                _spriteBatch.Draw(carBlue, new Rectangle(100,20,500,400), Color.White);
                _spriteBatch.Draw(backBtn,backBtnRect, Color.White);
            }
            if (screen == Screen.Green)
            {
                    _spriteBatch.Draw(greenScreen, new Rectangle(0, 0, 800, 600), Color.White);
            }
            if (screen == Screen.Game)
            {
                
                _spriteBatch.Draw(track, trackRect1, Color.White);
                _spriteBatch.Draw(track, trackRect2, Color.White);
                _spriteBatch.Draw(carBlue, carBlueRect, Color.White);
                _spriteBatch.Draw(carGreen, carGreenRect, Color.White);
                _spriteBatch.Draw(carYellow, carYellowRect, Color.White);
                
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
