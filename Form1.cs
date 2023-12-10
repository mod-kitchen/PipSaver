using System.Text;

namespace PSTK
{
    public partial class Form1 : Form
    {
        private Dictionary<string, string> _files = new();
        private string _author = "PipSaver Toolkit";
        private string _pack = "TestPack";
        private string _desc = "Asset pack for PipSaver";

        public Form1()
        {
            InitializeComponent();
            _files.Clear();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newIndex = listBox1.SelectedIndex;
            if (newIndex == -1) button2.Enabled = false;
            else
            {
                button2.Enabled = true;
                string key = (string)listBox1.Items[newIndex];
                if (_files.ContainsKey(key))
                {
                    LoadPreview(key, _files[key]);
                }
            }
        }

        private void LoadPreview(string name, string path)
        {
            if (pictureBox1.Image != null)
            {
                var i = pictureBox1.Image;
                pictureBox1.Image = null;
                i.Dispose();
            }
            pictureBox1.Image = Image.FromFile(path);
            label1.Text = "Previewing: " + name;
        }

        //add file
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (var f in openFileDialog1.FileNames)
                {
                    string key = Path.GetFileName(f);
                    string v = f;
                    while (_files.ContainsKey(key))
                    {
                        key = "_" + key;
                    }
                    _files.Add(key, v);
                    listBox1.Items.Add(key);
                    label2.Text = _files.Count + "/500 slots used";
                    if (_files.Count > 500)
                    {
                        button3.Enabled = false;
                        label2.ForeColor = Color.Red;
                    }
                    else
                    {
                        button3.Enabled = true;
                        label2.ForeColor = Color.White;
                    }
                }
            }
        }

        //remove file
        private void button2_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index >= 0)
            {
                string key = (string)listBox1.Items[index];
                if (_files.ContainsKey(key))
                {
                    listBox1.Items.RemoveAt(index);
                    _files.Remove(key);
                    label2.Text = _files.Count + "/500 slots used";
                    if (_files.Count > 500)
                    {
                        button3.Enabled = false;
                        label2.ForeColor = Color.Red;
                    }
                    else
                    {
                        button3.Enabled = true;
                        label2.ForeColor = Color.White;
                    }
                }
            }
        }

        private string Sanitize(string input, string dfault)
        {
            if (string.IsNullOrWhiteSpace(input)) return dfault;
            string output = input;
            //strip non-ascii
            output = Encoding.ASCII.GetString(
                Encoding.Convert(
                    Encoding.UTF8,
                    Encoding.GetEncoding(
                        Encoding.ASCII.EncodingName,
                        new EncoderReplacementFallback(string.Empty),
                        new DecoderExceptionFallback()
                        ),
                    Encoding.UTF8.GetBytes(output)
                )
            );
            //remove unwanted ascii
            for (int i = output.Length - 1; i >= 0; i--)
            {
                if (output[i] < 32 || output[i] > 126) output.Remove(i);
            }
            //trim whitespace
            output = output.Trim();
            //truncate if necessary
            if (output.Length > 511)
            {
                output = output.Substring(0, 511);
            }
            //done
            if (output.Length == 0) return dfault;
            return output;
        }

        //generate pack
        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            var f2 = new Form2(_author, _pack, _desc);
            if (f2.ShowDialog() == DialogResult.OK)
            {
                _author = Sanitize(f2.Author, "PipSaver Toolkit");
                _pack = Sanitize(f2.Pack, "TestPack");
                _desc = Sanitize(f2.Desc, "Asset pack for PipSaver");
                Generator.Generate(_author, _pack, _desc, label1, button3, _files.Values.ToList());
            } else
            {
                button3.Enabled = true;
            }
        }
    }
}