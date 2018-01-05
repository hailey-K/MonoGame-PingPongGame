
/***************************************************************************
 *  NAME : HYERIM KIM , CHEVY MCMARTIN
 *  STUDENT NUMBER : 7518301, 7657968 
 *  REVISION HISTORY : Dec 6TH 2017
 *  PROJECT : ASSIGNMENT 4
 *  
 *  
 *  DOCUMENTATION COMMENT :
 *  THIS IS PING PONG MONO GAME.
 *  AFTER THE GAME IS OVER AND YOU PRESS THE SPACE BAR, THE GAME WILL RESTART.
 ***************************************************************************/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace HKCMAssignment4
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Rectangle> bats;
        Rectangle batLeftRec, batRightRec, ballRec, scoreBarRec;
        Ball ball;
        Bat batLeft, batRight;
        ScoreBar scoreBar;
        bool GameOver = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            batLeftRec = new Rectangle(0, 0, 30, 150);
            batRightRec = new Rectangle(graphics.PreferredBackBufferWidth - 30, 0, 30, 150);
            bats = new List<Rectangle>();
            bats.Add(batLeftRec);
            bats.Add(batRightRec);
            ballRec = new Rectangle((graphics.PreferredBackBufferWidth - 20) / 2, (graphics.PreferredBackBufferHeight - 20) / 2, 20, 20);
            scoreBarRec = new Rectangle((graphics.PreferredBackBufferWidth - 300) / 2, 100, 300, 20);
            base.Initialize();
        }
        /// <summary>
        /// initializeAfterGameOver Method :
        /// This method will run to initialize the game 
        /// when the user wants to restart the game.
        /// </summary>
        protected void initializeAfterGameOver()
        {
            GameOver = false;
            scoreBar.initialize();
            batLeft.initialize(1);
            batRight.initialize(2);
            ball.InitializePosition();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //Bat for left
            batLeft = new Bat(this, spriteBatch, Content, batLeftRec, "images/BatLeft", 1, graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
            Components.Add(batLeft);

            //Bat for right
            batRight = new Bat(this, spriteBatch, Content, batRightRec, "images/BatRight", 2, graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
            Components.Add(batRight);

            //Ball
            ball = new Ball(this, spriteBatch, Content, ballRec, "images/Ball", bats, graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
            Components.Add(ball);
            ball.InitializePosition();

            //ScoreBar
            scoreBar = new ScoreBar(this, spriteBatch, Content, scoreBarRec, "images/Scorebar");
            Components.Add(scoreBar);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KeyboardState keyboardS = Keyboard.GetState();
            if (!GameOver)
            {
                if (keyboardS.IsKeyDown(Keys.Enter))
                {
                    ball.isEnterPressed = true;
                }
            }
            else
            {
                if (keyboardS.IsKeyDown(Keys.Space))
                {
                    initializeAfterGameOver();
                }
            }
            if (keyboardS.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (ball.isEnterPressed)
            {
                collisionBatsCheck();
            }

            base.Update(gameTime);
        }
        /// <summary>
        /// collisionBatsCheck() Method :
        /// This method checks the collision between the ball and the bars 
        /// </summary>
        public void collisionBatsCheck()
        {
            ball.ball.X += ball.ballSpeedX;
            ball.ball.Y += ball.ballSpeedY;
  
            //Left
           if(ball.ball.X <= batLeft.bat.Width)
            {
                if (ball.ball.Y >= batLeft.bat.Y && ball.ball.Y <= batLeft.bat.Y + batLeft.bat.Height)
                { 
                    ball.ballSpeedX *= -1;
                    ball.dingSound.Play();
                }
                else
                {
                    ball.clickSound.Play();
                    ball.InitializePosition();
                    scoreBar.addScore(scoreBar.leftScore, ++scoreBar.rightScore);
                    if (scoreBar.checkWinner())
                    {
                        GameOver = true;
                    }
                    ball.InitializePosition();
                }      
            }

           //right 
            if (ball.ball.X >= graphics.PreferredBackBufferWidth - (batRight.bat.Width + ball.ball.Width))
            {
                if (ball.ball.Y >= batRight.bat.Y && ball.ball.Y <= batRight.bat.Y + batRight.bat.Height)
                {
                    ball.ballSpeedX *= -1;
                    ball.dingSound.Play();

                }
                else
                {
                    ball.clickSound.Play();
                    ball.InitializePosition();
                    scoreBar.addScore(++scoreBar.leftScore, scoreBar.rightScore);
                    if (scoreBar.checkWinner())
                    {
                        GameOver = true;
                    }
                    ball.InitializePosition();
                }
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
