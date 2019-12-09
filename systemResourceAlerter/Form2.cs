using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace systemResourceAlerter
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public void setTitle(string title)
        {
            this.Text = title;
        }

        public void loadList(List<string> vs)
        {
            lsvBlackWhiteList.Items.Clear();
            foreach (var item in vs)
            {
                lsvBlackWhiteList.Items.Add(item);
            }
        }

        public List<string> saveList()
        {
            List<string> output = new List<string>();
            for (int i = 0; i < lsvBlackWhiteList.Items.Count; i++)
            {
                output.Add(lsvBlackWhiteList.Items[i].Text);
            }
            return output;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string newLine = Interaction.InputBox("Enter a new filter string", "Add filter", "");
            lsvBlackWhiteList.Items.Add(newLine);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem eachItem in lsvBlackWhiteList.SelectedItems)
            {
                lsvBlackWhiteList.Items.Remove(eachItem);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
