using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClothForHands
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void materialsTryBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.materialsTryBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.bDexportScriptDataSet1);

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bDexportScriptDataSet1.MaterialsTry". При необходимости она может быть перемещена или удалена.
            this.materialsTryTableAdapter.Fill(this.bDexportScriptDataSet1.MaterialsTry);

        }
    }
}
