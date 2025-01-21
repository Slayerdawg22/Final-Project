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
            Betting,
            Blue,
            Green,
            Yellow,
            BlueWin,
            GreenWin,
            YellowWin,
            Game
        }
        MouseState mouseState, premouseState;
        Random random;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Screen screen;
        KeyboardState keyboardState;
        Texture2D track, carBlue, carGreen, introScreen, carYellow, bettingScreen, BlueScreen, greenScreen, yellowScreen, backBtn;
        Texture2D startBtn, fiveTkn, twentyFiveTkn, fiftyTkn, hundredTkn, blueWin, greenWin, yellowWin, allInTkn;
        Rectangle trackRect1, trackRect2, carBlueRect, carGreenRect, carYellowRect, startBtnRect, fiveTknRect, twentyFiveTknRect, hundredTknRect, fiftyTknRect;
        Rectangle carBlueRectBet, carGreenRectbet, carYellowRectBet, backBtnRect, allInTknRect;
        int scrollSpeed = 1;
        int maxSpeed = 20;
        int acceleration = 1;
        float seconds;
        int money, amountWon;
        int betBlue, betGreen, betYellow;
        SpriteFont amount;

        
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
            carBlueRect = new Rectangle(90, 290, 110, 160);
            carGreenRect = new Rectangle(340, 290, 110, 160);
            carYellowRect = new Rectangle(580, 290, 110, 160);

            //Betting Rects
            carBlueRectBet = new Rectangle(110, 200, 140, 190);
            carGreenRectbet = new Rectangle(340, 200, 140, 190);
            carYellowRectBet = new Rectangle(540, 200, 140, 190);
            startBtnRect = new Rectangle(270,-135,250,250);
            money = 10000;
            amountWon = 0;
            betBlue = 0;
            betGreen = 0;
            betYellow = 0;
            //Tkns
            fiftyTknRect = new Rectangle(370, 300, 70, 70);
            twentyFiveTknRect = new Rectangle(440, 300, 70, 70);
            fiveTknRect = new Rectangle(510, 300, 70, 70);
            hundredTknRect = new Rectangle(300, 300, 70, 70);
            allInTknRect = new Rectangle(580,300,70,70);


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
            carBlue = Content.Load<Texture2D>("carBlue2");
            carGreen = Content.Load<Texture2D>("carGreen2");
            carYellow = Content.Load<Texture2D>("yellowCar1");
            introScreen = Content.Load<Texture2D>("RouletteBack");
            bettingScreen = Content.Load<Texture2D>("Betground");
            BlueScreen = Content.Load<Texture2D>("BlueBoy");
            backBtn = Content.Load<Texture2D>("BackBtn");
            greenScreen = Content.Load<Texture2D>("GreenGoblin");
            yellowScreen = Content.Load<Texture2D>("YellowBird1");
            amount = Content.Load<SpriteFont>("Amount");
            startBtn = Content.Load<Texture2D>("Start1");
            fiveTkn = Content.Load<Texture2D>("5$");
            twentyFiveTkn = Content.Load<Texture2D>("25$");
            fiftyTkn = Content.Load<Texture2D>("50$");
            hundredTkn = Content.Load<Texture2D>("100$");
            allInTkn = Content.Load<Texture2D>("All in");
            blueWin = Content.Load<Texture2D>("BlueWin");
            greenWin = Content.Load<Texture2D>("GreenWin");
            yellowWin = Content.Load<Texture2D>("YellowWin");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";
            keyboardState = Keyboard.GetState();
            premouseState = mouseState;
            mouseState = Mouse.GetState();

            //Intro
            if (screen == Screen.Intro)
            {

                if (keyboardState.GetPressedKeyCount() > 0)
                    screen = Screen.Betting;

            }
            //Betting
            if (screen == Screen.Betting)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && premouseState.LeftButton == ButtonState.Released)
                {
                    if (carGreenRectbet.Contains(mouseState.Position))
                    {
                        
                        screen = Screen.Green;
                    }
                    if (carBlueRectBet.Contains(mouseState.Position))
                    {
                        
                        screen = Screen.Blue;
                    }    
                    if (carYellowRectBet.Contains(mouseState.Position))
                    {
                        
                        screen = Screen.Yellow;
                    }
                    if (startBtnRect.Contains(mouseState.Position))
                        screen = Screen.Game;
                    if (betBlue < 0 || betGreen < 0 || betYellow < 0)
                        Exit();

                }
            }
            //Betting
            if (screen == Screen.Blue)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    if (backBtnRect.Contains(mouseState.Position))
                        screen = Screen.Betting;
                if (mouseState.LeftButton == ButtonState.Pressed && premouseState.LeftButton == ButtonState.Released)
                {
                    if (fiftyTknRect.Contains(mouseState.Position))
                    {
                        money -= 50;
                        betBlue += 50;
                    }
                    if (hundredTknRect.Contains(mouseState.Position))
                    {
                        money -= 100;
                        betBlue += 100;
                    }
                    if (twentyFiveTknRect.Contains(mouseState.Position))
                    {
                        money -= 25;
                        betBlue += 25;
                    }
                    if (fiveTknRect.Contains(mouseState.Position))
                    {
                        money -= 5;
                        betBlue += 5;
                    }
                    if (allInTknRect.Contains(mouseState.Position))
                    {
                        betBlue += money;
                        money -= money;
                    }
                }
            }
            if (screen == Screen.Green)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    if (backBtnRect.Contains(mouseState.Position))
                        screen = Screen.Betting;
                if (mouseState.LeftButton == ButtonState.Pressed && premouseState.LeftButton == ButtonState.Released)
                {
                    if (fiftyTknRect.Contains(mouseState.Position))
                    {
                        money -= 50;
                        betGreen += 50;
                    }
                    if (hundredTknRect.Contains(mouseState.Position))
                    {
                        money -= 100;
                        betGreen += 100;
                    }
                    if (twentyFiveTknRect.Contains(mouseState.Position))
                    {
                        money -= 25;
                        betGreen += 25;
                    }
                    if (fiveTknRect.Contains(mouseState.Position))
                    {
                        money -= 5;
                        betGreen += 5;
                    }
                    if (allInTknRect.Contains(mouseState.Position))
                    {
                        betGreen += money;
                        money -= money;
                    }
                }

            }
            if (screen == Screen.Yellow)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    if (backBtnRect.Contains(mouseState.Position))
                        screen = Screen.Betting;
                if (mouseState.LeftButton == ButtonState.Pressed && premouseState.LeftButton == ButtonState.Released)
                    {
                        if (fiftyTknRect.Contains(mouseState.Position))
                        {
                            money -= 50;
                            betYellow += 50;
                        }
                        if (hundredTknRect.Contains(mouseState.Position))
                        {
                            money -= 100;
                            betYellow += 100;
                        }
                        if (twentyFiveTknRect.Contains(mouseState.Position))
                        {
                            money -= 25;
                            betYellow += 25;
                        }
                        if (fiveTknRect.Contains(mouseState.Position))
                        {
                            money -= 5;
                            betYellow += 5;
                        }
                        if (allInTknRect.Contains(mouseState.Position))
                        {
                        betYellow += money;
                        money -= money;
                        }
                }

            }
            //Restart
            if (screen == Screen.Betting)
            {
                amountWon = 0;
                carBlueRect.Y = 290;
                carGreenRect.Y = 290;
                carYellowRect.Y = 290;
                scrollSpeed = 0;
                seconds = 0;

            }
            //Wins
            if (screen == Screen.YellowWin || screen == Screen.BlueWin || screen == Screen.GreenWin)
                if (mouseState.LeftButton == ButtonState.Pressed && premouseState.LeftButton == ButtonState.Released)
                    if (backBtnRect.Contains(mouseState.Position))
                        screen = Screen.Betting;
            
            
            

            //Game
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
                    carBlueRect.Y -= random.Next(-2, 5);
                    carGreenRect.Y -= random.Next(-2, 5);
                    carYellowRect.Y -= random.Next(-2, 5);
                }
                if (carBlueRect.Y <= -80 || carGreenRect.Y <= -80 || carYellowRect.Y <= -80)
                {
                    carBlueRect.Y -= 2;
                    carGreenRect.Y -= 2;
                    carYellowRect.Y -= 2;
                }
                if (carBlueRect.Y <= -120)
                    if (betBlue >= 0)
                    {
                        amountWon += betBlue * 2;
                        money += amountWon;
                        betBlue = 0;
                        betYellow = 0;
                        betGreen = 0;
                        screen = Screen.BlueWin;
                    }
                if (carGreenRect.Y <= -120)
                    if (betGreen >= 0)
                    {
                        amountWon += betGreen * 2;
                        money += amountWon;
                        betGreen = 0;
                        betBlue = 0;
                        betYellow = 0;
                        screen = Screen.GreenWin;
                    }
                if (carYellowRect.Y <= -120)
                    if (betYellow >= 0)
                    {
                        amountWon += betYellow * 2;
                        money += amountWon;
                        betYellow = 0;
                        betGreen = 0;
                        betBlue = 0;
                        screen = Screen.YellowWin;
                    }


                if (carBlueRect.Y <= -160 || carGreenRect.Y <= -160 || carYellowRect.Y <= -160)
                {
                    scrollSpeed = 0;
                    seconds = 0;
                    
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
                _spriteBatch.Draw(startBtn, startBtnRect, Color.White);
                _spriteBatch.DrawString(amount, money.ToString("Money:00$"), new Vector2(30, 30), Color.White);
            }
            if (screen == Screen.Blue)
            {
                _spriteBatch.Draw(BlueScreen, new Rectangle(0, 0, 800, 600), Color.White);
                _spriteBatch.Draw(carBlue, new Rectangle(70,180,200,290), Color.White);
                _spriteBatch.Draw(backBtn,backBtnRect, Color.White);
                _spriteBatch.Draw(fiftyTkn,fiftyTknRect, Color.White);
                _spriteBatch.Draw(twentyFiveTkn,twentyFiveTknRect,Color.White);
                _spriteBatch.Draw(hundredTkn, hundredTknRect, Color.White);
                _spriteBatch.Draw(fiveTkn, fiveTknRect, Color.White);
                _spriteBatch.DrawString(amount, money.ToString("Money:00$"), new Vector2(90, 20), Color.White);
                _spriteBatch.DrawString(amount, betBlue.ToString("Blue Boy Bet: 00$"), new Vector2(210, 20), Color.White);
                _spriteBatch.Draw(allInTkn,allInTknRect, Color.White);

            }
            if (screen == Screen.Green)
            {
                _spriteBatch.Draw(greenScreen, new Rectangle(0, 0, 800, 600), Color.White);
                _spriteBatch.Draw(carGreen, new Rectangle(70,140,200,290), Color.White);
                _spriteBatch.Draw(backBtn,backBtnRect, Color.White);
                _spriteBatch.Draw(fiftyTkn, fiftyTknRect, Color.White);
                _spriteBatch.Draw(twentyFiveTkn, twentyFiveTknRect, Color.White);
                _spriteBatch.Draw(hundredTkn, hundredTknRect, Color.White);
                _spriteBatch.Draw(fiveTkn, fiveTknRect, Color.White);
                _spriteBatch.DrawString(amount, money.ToString("Money:00$"), new Vector2(90, 20), Color.White);
                _spriteBatch.DrawString(amount, betGreen.ToString("Green Goblin Bet: 00$"), new Vector2(210, 20), Color.White);
                _spriteBatch.Draw(allInTkn, allInTknRect, Color.White);
            }
            if (screen == Screen.Yellow)
            {
                _spriteBatch.Draw(yellowScreen, new Rectangle(0,0,800, 600), Color.White);
                _spriteBatch.Draw(carYellow, new Rectangle(40,160,200,290), Color.White);
                _spriteBatch.Draw(backBtn, backBtnRect, Color.White);
                _spriteBatch.Draw(fiftyTkn, fiftyTknRect, Color.White);
                _spriteBatch.Draw(twentyFiveTkn, twentyFiveTknRect, Color.White);
                _spriteBatch.Draw(hundredTkn, hundredTknRect, Color.White);
                _spriteBatch.Draw(fiveTkn, fiveTknRect, Color.White);
                _spriteBatch.DrawString(amount, money.ToString("Money:00$"), new Vector2(90, 20), Color.White);
                _spriteBatch.DrawString(amount, betYellow.ToString("Yellow Bird Bet: 00$"), new Vector2(210, 20), Color.White);
                _spriteBatch.Draw(allInTkn, allInTknRect, Color.White);
            }
            if (screen == Screen.BlueWin)
            {
                _spriteBatch.Draw(blueWin, new Rectangle(0, 0, 800, 600), Color.White);
                _spriteBatch.DrawString(amount, amountWon.ToString("You Won: 00$"), new Vector2(300, 300), Color.White);
                _spriteBatch.Draw(backBtn, backBtnRect, Color.White);
            }
            if (screen == Screen.GreenWin)
            {
                _spriteBatch.Draw(greenWin, new Rectangle(0, 0, 800, 600), Color.White);
                _spriteBatch.DrawString(amount, amountWon.ToString("You Won: 00$"), new Vector2(300, 300), Color.White);
                _spriteBatch.Draw(backBtn, backBtnRect, Color.White);
            }
            if (screen == Screen.YellowWin)
            {
                _spriteBatch.Draw(yellowWin, new Rectangle(0, 0, 800, 600), Color.White);
                _spriteBatch.DrawString(amount, amountWon.ToString("You Won: 00$"), new Vector2(300, 300), Color.White);
                _spriteBatch.Draw(backBtn, backBtnRect, Color.White);
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
