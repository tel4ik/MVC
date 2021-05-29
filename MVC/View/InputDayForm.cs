using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVC.View
{
    public partial class InputDayForm : Form
    {
        public InputDayForm()
        {
            InitializeComponent();
        }

        public string date;

        private void button1_Click(object sender, EventArgs e)
        {
            monthCalendar1.MaxSelectionCount = 1;
            var Date = monthCalendar1.SelectionStart;
            date = Date.ToString("yyyy-MM-dd");
            MessageBox.Show(date);
            this.Close();
        }
    }
}
