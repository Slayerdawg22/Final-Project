using Microsoft.Xna.Framework;
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
            Game
        }
        MouseState mouseState;
        Random random;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Screen screen;
        KeyboardState keyboardState;
        Texture2D track, carBlue, carGreen, introScreen;
        Rectangle trackRect1, trackRect2, carBlueRect, carGreenRect;
        int scrollSpeed = 1;
        int maxSpeed = 20;
        int acceleration = 1;
        float seconds;
        Vector2 carGreenSpeed, carBlueSpeed;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            

            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screen = Screen.Intro;
            
            
            int screenWidth = GraphicsDevice.Viewport.Width;
            int trackHeight = 1000;
            trackRect1 = new Rectangle(0, 0, screenWidth, trackHeight);
            trackRect2 = new Rectangle(0, -trackHeight, screenWidth, trackHeight);
            carBlueRect = new Rectangle(90, 230, 350, 233);
            carGreenRect = new Rectangle(340, 210, 350, 255);
            carBlueSpeed = new Vector2(0,2);
            carGreenSpeed = new Vector2(0,2);
            random = new Random();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            track = Content.Load<Texture2D>("Track");
            carBlue = Content.Load<Texture2D>("carBlue1");
            carGreen = Content.Load<Texture2D>("carGreen1");
            introScreen = Content.Load<Texture2D>("RouletteBack");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";
            keyboardState = Keyboard.GetState();


            if (screen == Screen.Intro)
            {

                if (keyboardState.GetPressedKeyCount() > 0)
                    screen = Screen.Game;

            }
            else if (screen == Screen.Game)
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
                if (screen == Screen.Game);
                carBlueRect.Y -= random.Next(-2, 4);
                carGreenRect.Y -= random.Next(-2, 4);





                //if (carBlueRect.Y < 300) ;
                //carBlueRect.Y = 230;
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

           
            else if (screen == Screen.Game)
            {
                
                _spriteBatch.Draw(track, trackRect1, Color.White);
                _spriteBatch.Draw(track, trackRect2, Color.White);
                _spriteBatch.Draw(carBlue, carBlueRect, Color.White);
                _spriteBatch.Draw(carGreen, carGreenRect, Color.White);
                
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
