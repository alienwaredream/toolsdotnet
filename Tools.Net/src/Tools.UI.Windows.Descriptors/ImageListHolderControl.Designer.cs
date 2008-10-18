namespace Tools.UI.Windows.Descriptors
{
    partial class ImageListHolderControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageListHolderControl));
            this.locksImageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // locksImageList
            // 
            this.locksImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("locksImageList.ImageStream")));
            this.locksImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.locksImageList.Images.SetKeyName(0, "security.ico");
            this.locksImageList.Images.SetKeyName(1, "UtilityText.ico");
            // 
            // ImageListHolderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ImageListHolderControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList locksImageList;
    }
}
