namespace SNotepad
{
    public partial class MainForm : Form
    {
        private string currentFilePath = "";
        private bool isTextChanged = false;
        public MainForm()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ConfirmSaveIfNeeded()) return;
            editor.Clear();
            currentFilePath = "";
            this.Text = "Untitled - Notepad";
            isTextChanged = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ConfirmSaveIfNeeded()) return;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text Files (*.txt)|*.txt|All Files(*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = dlg.FileName;
                editor.Text = File.ReadAllText(currentFilePath);
                this.Text = Path.GetFileName(currentFilePath) + " - Notepad";
            }
            isTextChanged = false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                saveToolStripMenuItem1_Click(sender, e);
            }
            else
            {
                File.WriteAllText(currentFilePath, editor.Text);
                isTextChanged = false;
            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Text Files (*.txt)|*.txt|All Files(*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = dlg.FileName;
                File.WriteAllText(currentFilePath, editor.Text);
                this.Text = Path.GetFileName(currentFilePath) + " - Notepad";
                isTextChanged = false;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ConfirmSaveIfNeeded()) return;
            Application.Exit();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.SelectAll();
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.WordWrap = !editor.WordWrap;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = editor.Font;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                editor.Font = fd.Font;
            }
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip1.Visible = !statusStrip1.Visible;
            statusBarToolStripMenuItem.Checked = statusStrip1.Visible;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Simple Notepad\nBuilt with C# WinForms", "About Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void editor_TextChanged(object sender, EventArgs e)
        {
            isTextChanged = true;
        }
        private bool ConfirmSaveIfNeeded()
        {
            if (!isTextChanged)
                return true;

            DialogResult result = MessageBox.Show(
                "Do you want to save changes?",
                "Notepad",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                saveToolStripMenuItem_Click(this, EventArgs.Empty);
                return !isTextChanged;
            }

            if (result == DialogResult.No)
                return true;

            return false; // Cancel
        }

    }
}
