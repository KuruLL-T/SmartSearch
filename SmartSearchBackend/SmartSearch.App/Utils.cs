namespace SmartSearch.App
{
    public static class Utils
    {
        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }
        public static void SaveImage(PictureBox pictureBox)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image files | *.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Text = openFileDialog1.FileName;
                pictureBox.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
            }
        }
    }
}
