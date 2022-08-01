namespace PhotoGallery
{
    partial class ImageData
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageData));
            this.Title = new System.Windows.Forms.Label();
            this.Selected = new System.Windows.Forms.CheckBox();
            this.DateModified = new System.Windows.Forms.Label();
            this.IsFavorite = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.IsFavorite)).BeginInit();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Location = new System.Drawing.Point(0, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(100, 23);
            this.Title.TabIndex = 0;
            // 
            // Selected
            // 
            this.Selected.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Selected.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Selected.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Selected.Location = new System.Drawing.Point(0, 20);
            this.Selected.Name = "Selected";
            this.Selected.Size = new System.Drawing.Size(160, 80);
            this.Selected.TabIndex = 0;
            this.Selected.UseVisualStyleBackColor = true;
            // 
            // DateModified
            // 
            this.DateModified.AutoSize = true;
            this.DateModified.BackColor = System.Drawing.SystemColors.Control;
            this.DateModified.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DateModified.Location = new System.Drawing.Point(0, 0);
            this.DateModified.Name = "DateModified";
            this.DateModified.Size = new System.Drawing.Size(100, 23);
            this.DateModified.TabIndex = 0;
            // 
            // IsFavorite
            // 
            this.IsFavorite.BackColor = System.Drawing.Color.Transparent;
            this.IsFavorite.Cursor = System.Windows.Forms.Cursors.Default;
            this.IsFavorite.Image = ((System.Drawing.Image)(resources.GetObject("IsFavorite.Image")));
            this.IsFavorite.InitialImage = ((System.Drawing.Image)(resources.GetObject("IsFavorite.InitialImage")));
            this.IsFavorite.Location = new System.Drawing.Point(0, 0);
            this.IsFavorite.Name = "IsFavorite";
            this.IsFavorite.Size = new System.Drawing.Size(32, 32);
            this.IsFavorite.TabIndex = 0;
            this.IsFavorite.TabStop = false;
            // 
            // ImageData
            // 
            this.Controls.Add(this.Selected);
            this.Controls.Add(this.IsFavorite);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            ((System.ComponentModel.ISupportInitialize)(this.IsFavorite)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        
        public System.Windows.Forms.Label Title;
        public System.Windows.Forms.CheckBox Selected;
        public System.Windows.Forms.Label DateModified;
        public System.Windows.Forms.PictureBox IsFavorite;
        private System.Drawing.Image favorite = Properties.Resources.favorite;
        private System.Drawing.Image not_favorite = Properties.Resources.not_favorite;

        private System.Drawing.Rectangle titleSpace;
        private System.Drawing.Rectangle dateSpace;
        private System.Drawing.Rectangle favIconSpace;
        
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
        {
            titleSpace = new System.Drawing.Rectangle(0, 0, ClientRectangle.Width, 20);
            dateSpace = new System.Drawing.Rectangle(0, ClientRectangle.Bottom - 40, ClientRectangle.Width - 40, 40);
            favIconSpace = new System.Drawing.Rectangle(ClientRectangle.Width - 40, ClientRectangle.Bottom - 40, IsFavorite.Width, IsFavorite.Height);
            System.Drawing.Graphics graphics = pe.Graphics;
            System.Drawing.Rectangle controlContainer = new System.Drawing.Rectangle(ClientRectangle.Location, ClientSize);

            #region Draw Title
            System.Drawing.StringFormat titleFormat = new System.Drawing.StringFormat();
            titleFormat.Alignment = System.Drawing.StringAlignment.Center;
            titleFormat.LineAlignment = System.Drawing.StringAlignment.Center;
            graphics.DrawString(Title.Text, new System.Drawing.Font("Serif", 9f), System.Drawing.Brushes.Black, titleSpace, titleFormat);
            controlContainer.Inflate(titleSpace.Size);
            controlContainer.Intersect(titleSpace);
            #endregion Draw Title

            #region Draw Date Added
            System.Drawing.StringFormat dateFormat = new System.Drawing.StringFormat();
            dateFormat.Alignment = System.Drawing.StringAlignment.Center;
            dateFormat.LineAlignment = System.Drawing.StringAlignment.Center;
            graphics.DrawString(DateModified.Text, new System.Drawing.Font("Serif", 9f), System.Drawing.Brushes.Black, dateSpace, dateFormat);
            controlContainer.Inflate(dateSpace.Size);
            controlContainer.Intersect(dateSpace);
            #endregion Draw Date Added

            #region Draw Favorite Icon
            IsFavorite.Location = favIconSpace.Location;
            controlContainer.Inflate(favIconSpace.Size);
            controlContainer.Intersect(favIconSpace);
            #endregion Draw Favorite Icon

            base.OnPaint(pe);
        }

        #region *** DO NOT TOUCH ***
        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                System.Windows.Forms.CreateParams parms = base.CreateParams;
                parms.Style &= ~0x02000000;
                return parms;
            }
        }
        #endregion *** DO NOT TOUCH ***
    }
}
