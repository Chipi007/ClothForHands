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
    public partial class Form2 : Form
    {
        List<string> typesMaterial = new List<string>();
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Model2 db = new Model2())
            {
                MaterialsTry userObj = new MaterialsTry();
                userObj.Name = textBox1.Text;
                userObj.Material = comboBox1.Text;
                userObj.WayImage = textBox3.Text;
                userObj.Cost = textBox4.Text;
                userObj.Quantity = textBox5.Text;
                userObj.InStock = textBox6.Text;
                db.MaterialsTry.Add(userObj);
                db.SaveChanges();
                if (userObj != null)
                {
                    MessageBox.Show("Данные добавлены успаешно!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 Form1;
            Form1 = new Form1();
            Form1.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bDexportScriptDataSet1.MaterialsTry". При необходимости она может быть перемещена или удалена.
            this.materialsTryTableAdapter.Fill(this.bDexportScriptDataSet1.MaterialsTry);

            using (Model2 db = new Model2())
            {
                var materials = db.MaterialsTry.OrderBy(item => item.Material);
                int count = 1;
                foreach (MaterialsTry userObj in materials)
                {
                    typesMaterial.Add(userObj.Material);
                    count++;
                }
                typesMaterial = typesMaterial.Distinct().ToList(); //удаление повторяющихся значений в комбобоксе
                foreach (String types in typesMaterial)
                {
                    comboBox1.Items.Add(types);
                }
                comboBox1.SelectedIndex = 0;
            
            }   
        }
    }
}
