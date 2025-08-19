using System;
 using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        
        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople frm2 = new frmListPeople();

            frm2.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmUsers();

            frm.ShowDialog();
        }
    }
}
