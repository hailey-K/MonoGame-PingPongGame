using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCMAssignment4
{
    /// <summary>
    /// Ball Class:
    /// This class is used for ball object
    /// </summary>
    class Ball : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        ContentManager content;
        public Rectangle ball;
        string imageSources;
        public bool isEnterPressed;
        Texture2D ballTex;
        List<Rectangle> bats;
        public SoundEffect dingSound;
        public SoundEffect clickSound;
        public int ballSpeedX, ballSpeedY;
        int graphicWidth;
        int graphicHeight;

        public Ball(Game game, SpriteBatch spriteBatch, ContentManager content, Rectangle ball, string imageSources, List<Rectangle> bats, int graphicHeight , int graphicWidth) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.content = content;
            this.ball = ball;
            this.imageSources = imageSources;
            this.graphicHeight = graphicHeight;
            this.graphicWidth = graphicWidth;
            this.bats = bats;

            Random ballSpeedRnd = new Random();

            ballSpeedX = ballSpeedRnd.Next(3, 9);
            ballSpeedY = ballSpeedRnd.Next(3, 9);
            //ballSpeedX = 1;
            //ballSpeedY = 1;


            if (ballSpeedX % 2 == 0)
            {
                ballSpeedX *= -1;
            }
            LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ballTex, ball, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (isEnterPressed)
            {
                collisionGraphicCheck();
            }
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            ballTex = content.Load<Texture2D>(imageSources);
            dingSound = content.Load<SoundEffect>("sounds/ding");
            clickSound = content.Load<SoundEffect>("sounds/click");
            base.LoadContent();
        }
        /// <summary>
        /// InitializePosition() Method :
        /// This method initialize the position of the ball
        /// </summary>
        public void InitializePosition()
        {
            ball.X = (graphicWidth - 20) / 2;
            ball.Y = (graphicHeight - 20) / 2;
            isEnterPressed = false;
        }
        /// <summary>
        /// collisionGraphicCheck() Method :
        /// This method check the collision between the ball and wall
        /// </summary>
        public void collisionGraphicCheck()
        {
            ball.X += ballSpeedX;
            ball.Y += ballSpeedY;

            if(ball.Y <= 0 || ball.Y >= graphicHeight - ball.Height)
            {
                ballSpeedY *= -1;
                dingSound.Play();
            }
        }
    }
}
