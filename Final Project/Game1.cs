using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Project
{
    public class Game1 : Game
    {
        enum Screen
        {
            //Intro,
            Game
        }
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Screen screen;
        KeyboardState keyboardState;
        Texture2D track;
        Rectangle trackRect1, trackRect2;
        int scrollSpeed = 1;
        int maxSpeed = 20;
        int acceleration = 1;
        float seconds;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            

            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //screen = Screen.Intro;
            keyboardState = Keyboard.GetState();
            
            int screenWidth = GraphicsDevice.Viewport.Width;
            int trackHeight = 1000;
            trackRect1 = new Rectangle(0, 0, screenWidth, trackHeight);
            trackRect2 = new Rectangle(0, -trackHeight, screenWidth, trackHeight);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            track = Content.Load<Texture2D>("Track");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //if (keyboardState.GetPressedKeyCount() > 0)
            //screen = Screen.Game;
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            scrollSpeed += (acceleration * (int)seconds);
            if (scrollSpeed > maxSpeed)
                scrollSpeed = maxSpeed; //max
            
            trackRect1.Y += scrollSpeed;
            trackRect2.Y += scrollSpeed;

            int screenHeight = GraphicsDevice.Viewport.Height;

            if (trackRect1.Y >= screenHeight)
                trackRect1.Y = trackRect2.Y - trackRect1.Height;
            if (trackRect2.Y >= screenHeight)
                trackRect2.Y = trackRect1.Y - trackRect2.Height;

            
            
                
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //if (screen == Screen.Intro)
            

           
            if (screen == Screen.Game)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(track, trackRect1, Color.White);
                _spriteBatch.Draw(track, trackRect2, Color.White);
                _spriteBatch.End();
            }


            base.Draw(gameTime);
        }
    }
}
