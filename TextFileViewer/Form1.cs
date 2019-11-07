using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TextFileViewer {
    public partial class MainForm : Form {
        // Keep track of whether the currently-open file contains a UTF-8 BOM
        private bool bHasBOM = false;
        public MainForm() {
            InitializeComponent();
        }

        private void miQuit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void miOpen_Click(object sender, EventArgs e)
        {

            DialogResult drOpenResult = ofdOpenFile.ShowDialog();
            //Only Proceed if the dialog result is OK.
            if (drOpenResult == DialogResult.OK) 
            {
                // Get the path of the file sleteced by the user and open it.
                string strFilePath = ofdOpenFile.FileName;
                FileStream fsFile = File.Open(strFilePath, FileMode.Open, FileAccess.Read);
                // Read the data in the file, convert to text, and display in
                // the rich text box.
                // Set up bugger to hold data read from file.
                const int iBufferSize = 4096;
                byte[] byBuffer = new byte[iBufferSize];
                //Clear any text that is currently appearing in the rich text box
                rtbText.Clear();
                // Initial read to start the process. Then loop as long as we read in more bytes.
                bHasBOM = false;
                int iBytesRead = fsFile.Read(byBuffer, 0, iBufferSize);
                // Inital read to start eh process. Check for UTF-8 BOM at beggining of the file.
                if (iBytesRead > 0 && byBuffer[0] == 0xEF && byBuffer[1] == 0xBB && byBuffer[2] == 0xBF)
                {
                    // There is a UTF-8 BOM at the beginning. Keep track of this, tne read first byte of text to start loop.
                    bHasBOM = true;
                    iBytesRead = fsFile.Read(byBuffer, 3, iBufferSize);
                }
                while (iBytesRead > 0) 
                {
                    // Convert the bytes into a string of characters and append ot the rich text box.
                    string strNewText = Encoding.ASCII.GetString(byBuffer, 0, iBytesRead);
                    rtbText.AppendText(strNewText);
                    // Read a new block of bytes from file
                    iBytesRead = fsFile.Read(byBuffer, 0, iBufferSize);
                }
                // Finished. Close the File.
                fsFile.Close();

            }
        }
    }
}
