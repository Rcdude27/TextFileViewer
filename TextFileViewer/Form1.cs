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
                // Finished. Close the File.

            }
        }
    }
}
