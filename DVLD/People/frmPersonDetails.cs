using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussenessAccesses;

namespace DVLD
{
    public partial class frmPersonDetails : Form
    {
        public frmPersonDetails(int Id)
        {
            InitializeComponent();
            ucInfo2.LoadPersonInfo(Id);
        }

        public frmPersonDetails(string NationalNo)
        {
            InitializeComponent();
            ucInfo2.LoadPersonInfo(NationalNo);
        }
 
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
           
    }    
}
