using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Sokoban
{
    public partial class Registration : Form
    {
        public static Stopwatch stopWatch = new Stopwatch();

        public Registration()
        {
            InitializeComponent();
            for (int i = 0; i < Game.CountLevel; i++)
            {
                comboBox1.Items.Insert(i, "Level " + (i + 1));
            }
        }

        private void button_play_Click(object sender, EventArgs e)
        {
            Game.CreateMap(comboBox1.SelectedIndex + 1);

            stopWatch.Start();

            Hide();

            SokobanWindow formGame = new SokobanWindow();

            formGame.Text = "Player: " + textBox1.Text;

            formGame.ShowDialog();

            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
