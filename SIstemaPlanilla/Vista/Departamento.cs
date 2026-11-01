using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class Departamento : Form
    {
        public Departamento()
        {
            InitializeComponent();
        }

        private void Departamento_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Departamento_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }
    }
}
