using PhotoGallery.Properties;
using System.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using PhotoGallery.http.model;
using PhotoGallery.http;

namespace PhotoGallery
{
    
    public partial class PhotoGallery : Form
    {
        #region PARTIALLY PROVIDED
        public PhotoGallery()
        {
            InitializeComponent();
            tableLayoutPanel1.BringToFront();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            #region DO NOT TOUCH
            Settings.Default.Save();
            base.OnFormClosing(e);
            #endregion DO NOT TOUCH
        }
        #endregion PARTIALLY PROVIDED

        
        /// <summary>
        /// Displays a message to the user
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <param name="titleOfInfoDialog">Optional argument to title the message dialog</param>
        private void DisplayInfoMessage(string message, string titleOfInfoDialog = "Info")
        {
            MessageBox.Show(message, titleOfInfoDialog);
        }

        /// <summary>
        /// This updates all TableLayoutPanel control changes visually
        /// </summary>
        private void RenderControls()
        {
            int columnCount = tableLayoutPanel1.ColumnCount;

            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                int columnIndex = i % columnCount;
                int rowIndex = i / columnCount;
                tableLayoutPanel1.SetCellPosition(tableLayoutPanel1.Controls[i], new TableLayoutPanelCellPosition(columnIndex, rowIndex));
            }
        }
    }
}