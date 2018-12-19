using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Sokoban
{
    public class SokobanWindow : Form
    {
        private readonly Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();
        private readonly GameState gameState;
        private readonly HashSet<Keys> pressedKeys = new HashSet<Keys>();
        private int tickCount;

        public SokobanWindow(DirectoryInfo imagesDirectory = null)
        {
            gameState = new GameState();

            ClientSize = new Size(
                GameState.ElementSize * Game.MapWidth,
                GameState.ElementSize * Game.MapHeight + GameState.ElementSize);

            FormBorderStyle = FormBorderStyle.FixedDialog;

            if (imagesDirectory == null)
            {
                imagesDirectory = new DirectoryInfo("Images");
            }

            foreach (var e in imagesDirectory.GetFiles("*.png"))
            {
                bitmaps[e.Name] = (Bitmap)Image.FromFile(e.FullName);
            }

            var timer = new Timer();
            timer.Interval = 5;
            timer.Tick += TimerTick;
            timer.Start();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            BackColor = Color.Black;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            pressedKeys.Add(e.KeyCode);
            Game.KeyPressed = e.KeyCode;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyCode);
            Game.KeyPressed = pressedKeys.Any() ? pressedKeys.Min() : Keys.None;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(0, GameState.ElementSize);
            e.Graphics.FillRectangle(
                Brushes.Black, 0, 0, GameState.ElementSize * Game.MapWidth,
                GameState.ElementSize * Game.MapHeight);

            foreach (var a in gameState.Animations)
            {
                e.Graphics.DrawImage(bitmaps[a.Creature.GetImageFileName()], a.Location);
            }

            e.Graphics.ResetTransform();

            if (gameState.countStock == 0)
            {
                Registration.stopWatch.Stop();
                e.Graphics.DrawString("You winner!!", new Font("Arial", 16), Brushes.LimeGreen, 300, 0);
            }
            e.Graphics.DrawString("Score: " + Game.Scores.ToString(), new Font("Arial", 16), Brushes.LimeGreen, 0, 0);
            e.Graphics.DrawString(String.Format("Timer: {0:00}:{1:00}",
            Registration.stopWatch.Elapsed.Minutes, Registration.stopWatch.Elapsed.Seconds), new Font("Arial", 16), Brushes.LimeGreen, 120, 0);
        }

        private void TimerTick(object sender, EventArgs args)
        {
            if (tickCount == 0)
            {
                gameState.BeginAct();
            }

            foreach (var e in gameState.Animations)
            {
                e.Location = new Point(e.Location.X + 4 * e.Command.DeltaX, e.Location.Y + 4 * e.Command.DeltaY);
            }

            if (tickCount == 7)
            {
                gameState.EndAct();
            }
            tickCount++;

            if (tickCount == 8)
            {
                tickCount = 0;
            }
            Invalidate();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SokobanWindow
            // 
            this.ClientSize = new System.Drawing.Size(767, 462);
            this.Name = "SokobanWindow";
            this.Text = "Sokoban";
            this.Activated += new System.EventHandler(this.TimerTick);
            this.Load += new System.EventHandler(this.TimerTick);
            this.ResumeLayout(false);
        }
    }
}