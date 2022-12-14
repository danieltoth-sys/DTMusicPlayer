using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using PlayerUI.Properties;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors;

namespace PlayerUI
{
    public partial class Form1 : Form
    {
        public WMPLib.WindowsMediaPlayer Player;
        public string songPath { get; set; }
        double time = 0;

        public Form1()
        {
            InitializeComponent();
            hideSubMenu();
            Player = new WMPLib.WindowsMediaPlayer();
            trackBarControl2.Value = (int)Player.settings.volume;
        }

        private void hideSubMenu()
        {
            panelMediaSubMenu.Visible = false;            
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void btnMedia_Click(object sender, EventArgs e)
        {
            showSubMenu(panelMediaSubMenu);
        }

        #region MediaSubMenu
        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new Form2());            
            hideSubMenu();
        }

        private void button3_Click(object sender, EventArgs e)
        {            
            hideSubMenu();
        }

        private void button4_Click(object sender, EventArgs e)
        {            
            hideSubMenu();
        }

        private void button5_Click(object sender, EventArgs e)
        {            
            hideSubMenu();
        }
        #endregion

        private void btnPlaylist_Click(object sender, EventArgs e)
        {
            //showSubMenu(panelPlaylistSubMenu);
        }

        #region PlayListManagemetSubMenu
        private void button8_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }
        #endregion

        private void btnTools_Click(object sender, EventArgs e)
        {
            //showSubMenu(panelToolsSubMenu);
        }
        #region ToolsSubMenu
        private void button13_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }
        #endregion

        private void btnEqualizer_Click(object sender, EventArgs e)
        {
            openChildForm(new Form3());            
            hideSubMenu();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {            
            hideSubMenu();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit the application?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        Timer t;
        bool isPlaying;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playState == true)
            {
                isPlaying = false;
                Player.controls.pause();
                pictureBox2.Image = Resources.icons8_play_30;
                Properties.Settings.Default.playState = false;
                time = (double)Player.controls.currentPosition;
            }
            else
            {
                if (!(Properties.Settings.Default.songPath.ToString() == ""))
                {
                    isPlaying = true;
                    SelectSong(Properties.Settings.Default.songPath);
                    PlayFile(sender, e);
                    pictureBox2.Image = Resources.icons8_pause_30;
                    Player.controls.play();
                    Properties.Settings.Default.playState = true;
                }
                else
                {
                    Program.ErrorMessage("No file is selected!");
                    openChildForm(new Form2());
                }
            }
        }

        public void SelectSong(String url)
        {
            Player.URL = url;
        }
        public void PlayFile(object sender, EventArgs e)
        {            
            if (time != (double)Player.currentMedia.duration)
            {
                Player.controls.currentPosition = time;
            }
            Volume();

            t = new Timer();
            t.Interval = 1000;
            t.Tick += new EventHandler(t_Tick);
            t.Start();
        }
        private void Volume()
        {
            label3.Text = Player.settings.volume.ToString() + "%";
        }
        private void Player_MediaError(object pMediaObject)
        {
            MessageBox.Show("Cannot play media file.");
            this.Close();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (Player.settings.mute == false)
            {
                Player.settings.mute = true;
                Volume();
            }
            else
            {
                Player.settings.mute = false;
                Volume();
            }
        }
        void t_Tick(object sender, EventArgs e)
        {
            label1.Text = Player.currentMedia.durationString;
            trackBarControl1.Properties.Maximum = (int)Player.currentMedia.duration;
            trackBarControl2.Value = (int)Player.settings.volume;
            trackBarControl1.Value = 0;

            trackBarControl1.Value = (int)Player.controls.currentPosition;
            label2.Text = Player.controls.currentPositionString;
        }

        private void trackBarControl1_EditValueChanged(object sender, EventArgs e)
        {
            if (Player.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                trackBarControl1.Value = Convert.ToInt32(Player.controls.currentPosition);
            }
        }

        private void trackBarControl2_EditValueChanged(object sender, EventArgs e)
        {
            Player.settings.volume = trackBarControl2.Value;
            label3.Text = Player.settings.volume.ToString() + "%";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            time = 0;
            Player.controls.currentPosition = time;            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            time = Player.currentMedia.duration;
            Player.controls.pause();
        }
        bool isDown;

        private void trackBarControl1_MouseDown(object sender, MouseEventArgs e)
        {
            isDown = true;
            Player.controls.pause();
            t.Stop();
        }

        private void trackBarControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown)
            {
                TrackBarControl trackBar = sender as TrackBarControl;
                TrackBarViewInfo info = trackBar.GetViewInfo() as TrackBarViewInfo;
                trackBar.EditValue = info.ValueFromPoint(e.Location);

                Player.controls.currentPosition = trackBarControl1.Value;
            }
        }

        private void trackBarControl1_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
            t.Start();
            if (isPlaying == true)
            {
                Player.controls.play();
            }
        }
    }
}
