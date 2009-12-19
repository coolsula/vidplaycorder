﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VideoPlayer.State;

namespace VideoPlayer
{
    public partial class DisplayVideo : Form, IFrameDisplay
    {

        private delegate void RefreshImageDelegate(Bitmap image);

        private State.PlayerStateController _controller;

        public DisplayVideo()
        {
            InitializeComponent();
        }

        private void paramètresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new EditionParametreTraitement().ShowDialog();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

            }                
        }

        private void RefreshImage(Bitmap image)
        {
            framePictureBox.Image = image;
        }

        
        

        public void UpdateFrame(Bitmap frame)
        {
            RefreshImageDelegate del = RefreshImage;

            this.BeginInvoke(del, new[] {frame});
        }

        private void DisplayVideo_Shown(object sender, EventArgs e)
        {
            _controller = new PlayerStateController(this);
        }


    }
}