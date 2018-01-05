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
    /// ScoreBar Class :
    /// This class is used for ScoreBar object
    /// </summary>
    class ScoreBar : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        ContentManager content;
        public Rectangle scoreBar;
        string imageSources;

        public int leftScore = 0;
        public int rightScore = 0;
        string winnerS = "";
        string winnerName = "";
        public SpriteFont winner;
        public SpriteFont leftScoreLabel;
        public SpriteFont rightScoreLabel;
        private SpriteFont leftNameLabel;
        private SpriteFont rightNameLabel;
        SoundEffect applause;
        const int maxScore = 2;
        Texture2D ScoreBaTex;
        public ScoreBar(Game game, SpriteBatch spriteBatch, ContentManager content, Rectangle scoreBar, string imageSources) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.content = content;
            this.scoreBar = scoreBar;
            this.imageSources = imageSources;
            LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ScoreBaTex, scoreBar, Color.White);
            spriteBatch.DrawString(leftScoreLabel, Convert.ToString(leftScore), new Vector2(325, 50), Color.White);
            spriteBatch.DrawString(rightScoreLabel, Convert.ToString(rightScore), new Vector2(450, 50), Color.White);
            spriteBatch.DrawString(leftNameLabel, "Chevy", new Vector2(315, 20), Color.White);
            spriteBatch.DrawString(rightNameLabel, "Hyerim", new Vector2(435, 20), Color.White);
            spriteBatch.DrawString(winner, winnerS, new Vector2(120, 400), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            ScoreBaTex = content.Load<Texture2D>(imageSources);
            leftScoreLabel = content.Load<SpriteFont>("leftScoreLabel");
            rightScoreLabel = content.Load<SpriteFont>("rightScoreLabel");
            leftNameLabel = content.Load<SpriteFont>("leftNameLabel");
            rightNameLabel = content.Load<SpriteFont>("rightNameLabel");
            winner = content.Load<SpriteFont>("winner");
            applause = content.Load<SoundEffect>("sounds/applause1");
            base.LoadContent();
        }
        /// <summary>
        /// initialize() Method :
        /// This method initiate the scores for left and right players and winner's name
        /// </summary>
        public void initialize()
        {
            this.rightScore = 0;
            this.leftScore = 0;
            if (winnerS != "")
            {
                winnerS = "Previous winner name : "+ winnerName;
            }
        }
        /// <summary>
        /// addScore(int leftScore, int rightScore) Method :
        /// This method calculate the score
        /// </summary>
        /// <param name="leftScore"></param>
        /// <param name="rightScore"></param>
        public void addScore(int leftScore, int rightScore)
        {
            this.leftScore = leftScore;
            this.rightScore = rightScore;

            spriteBatch.Begin();
            spriteBatch.DrawString(leftScoreLabel, Convert.ToString(leftScore), new Vector2(100, 100), Color.Black);
            spriteBatch.DrawString(rightScoreLabel, Convert.ToString(rightScore), new Vector2(150, 100), Color.Black);
            spriteBatch.End();
        }
        /// <summary>
        /// checkWinner() Method :
        /// This method check if anyone gets maxScore (2)
        /// </summary>
        /// <returns></returns>
        public bool checkWinner()
        {
            spriteBatch.Begin();
            bool checkWinnerBool = false;
            if (this.leftScore == maxScore)
            {
                winnerName = "Chevy";
                winnerS = "Game Over : Chevy is Winner !\nPlease hit space bar if you wish to start new game.";
                applause.Play();
                checkWinnerBool= true;
            }
            else if (this.rightScore == maxScore)
            {
                winnerName = "Hyerim";
                winnerS = "Game Over : Hyerim is Winner !\nPlease hit space bar if you wish to start new game.";
                applause.Play();
                checkWinnerBool= true;
            }
            spriteBatch.DrawString(winner, winnerS, new Vector2(150, 100), Color.Black);
            spriteBatch.End();
            return checkWinnerBool;
        }
    }
}
