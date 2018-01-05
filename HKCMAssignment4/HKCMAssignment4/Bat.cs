using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCMAssignment4
{
    /// <summary>
    /// Bat Class
    /// This class is used for bat object
    /// </summary>
    class Bat : DrawableGameComponent
    {
        GraphicsDeviceManager g;
        SpriteBatch spriteBatch;
        ContentManager content;
        public Rectangle bat;
        Texture2D batTex;
        string imageSources;
        int leftOrRight;
        int graphicWidth;
        int graphicHeight;

        public Bat(Game game, SpriteBatch spriteBatch, ContentManager content, Rectangle bat, string imageSources, int leftOrRight, int graphicHeight, int graphicWidth) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.content = content;
            this.bat = bat;
            this.imageSources = imageSources;
            this.leftOrRight = leftOrRight;
            this.graphicHeight = graphicHeight;
            this.graphicWidth = graphicWidth;
            LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(batTex, bat, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardS = Keyboard.GetState();

            // left Bar 
            if (leftOrRight == 1)
            {
                if (keyboardS.IsKeyDown(Keys.A) && bat.Y > 0)
                {
                    bat.Y -= 10;
                }
                if (keyboardS.IsKeyDown(Keys.Z) && bat.Y <= graphicHeight - bat.Height)
                {
                    bat.Y += 10;
                }
            }

            // right Bar
            if (leftOrRight == 2)
            {
                if (keyboardS.IsKeyDown(Keys.Up) && bat.Y > 0)
                {
                    bat.Y -= 10;
                }
                if (keyboardS.IsKeyDown(Keys.Down) && bat.Y <= graphicHeight - bat.Height)
                {
                    bat.Y += 10;
                }
            }
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
           batTex = content.Load<Texture2D>(imageSources);
            base.LoadContent();
        }
        /// <summary>
        ///  initialize(int leftOrRight) Method :
        ///  This method initialize the position of the bat
        /// </summary>
        /// <param name="leftOrRight"></param>
        public void initialize(int leftOrRight)
        {
            if (leftOrRight == 1)
            {
                bat.X = 0;
                bat.Y = 0;
            }
            else if (leftOrRight == 2)
            {
                bat.X = graphicWidth -30;
                bat.Y = 0;
            }
        }
    }
}
