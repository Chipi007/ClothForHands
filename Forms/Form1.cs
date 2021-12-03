using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

using MyLib;
namespace ClothForHands
{
    public partial class Form1 : Form
    {
        public static int X = 30;
        public static int Y = -40;

        List<string> typesMaterial = new List<string>();

        String TYPE = "All types";
        String SORT;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bDexportScriptDataSet1.MaterialsTry". При необходимости она может быть перемещена или удалена.
            this.materialsTryTableAdapter.Fill(this.bDexportScriptDataSet1.MaterialsTry);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bDexportScriptDataSet.Material". При необходимости она может быть перемещена или удалена.
            this.materialTableAdapter.Fill(this.bDexportScriptDataSet.Material);

            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; //невозможность переписывать вручную значения в комбобоксе
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; //невозможность переписывать вручную значения в комбобоксе

            comboBox1.Items.Add("Sort ascending"); //сортировка по возрастанию
            comboBox1.Items.Add("Sort descending"); //сортировка по убыванию
            comboBox1.SelectedIndex = 0;


            typesMaterial.Add("All types");

            using (Model2 db = new Model2())
            {
                var materials = db.MaterialsTry.OrderBy(item => item.Material);
                int count = 1;

                foreach (MaterialsTry userObj in materials)
                {
                    typesMaterial.Add(userObj.Material);
                    InitializeMyGroupBox(X, Y + 100 * count, userObj.Name, userObj.Material, userObj.WayImage, userObj.Cost, userObj.Quantity, userObj.InStock);
                    count++;
                }

            }

            //добавление значений в список выборки по материалам
            typesMaterial = typesMaterial.Distinct().ToList(); //удаление повторяющихся значений в комбобоксе
            foreach (String types in typesMaterial)
            {
                comboBox2.Items.Add(types);
            }
            comboBox2.SelectedIndex = 0;
        }

        private void getData()
        {
            using (Model2 db = new Model2())
            {
                int count = 1;

                if (TYPE.Equals("All types")) //если показаны вся продукция
                {
                    if (SORT.Equals("Sort descending")) //сортировка по убыванию
                    {
                        var materials = db.MaterialsTry.OrderByDescending(item => item.Quantity);

                        foreach (MaterialsTry userObj in materials)
                        {
                            InitializeMyGroupBox(X, Y + 100 * count, userObj.Name, userObj.Material, userObj.WayImage, userObj.Cost, userObj.Quantity, userObj.InStock);
                            count++;
                        }
                    }

                    if (SORT.Equals("Sort ascending")) //сортировка по возрастанию
                    {
                        var materials = db.MaterialsTry.OrderBy(item => item.Quantity);

                        foreach (MaterialsTry userObj in materials)
                        {
                            InitializeMyGroupBox(X, Y + 100 * count, userObj.Name, userObj.Material, userObj.WayImage, userObj.Cost, userObj.Quantity, userObj.InStock);
                            count++;
                        }
                    }
                }
                else if(TYPE != null || !TYPE.Equals("")) {

                    if (SORT.Equals("Sort ascending"))
                    {
                        var materials = db.MaterialsTry.Where(mater => mater.Material == TYPE).OrderBy(item => item.Quantity);

                        foreach (MaterialsTry userObj in materials)
                        {
                            InitializeMyGroupBox(X, Y + 100 * count, userObj.Name, userObj.Material, userObj.WayImage, userObj.Cost, userObj.Quantity, userObj.InStock);
                            count++;
                        }
                    }

                    else if (SORT.Equals("Sort descending"))
                    {
                        var materials = db.MaterialsTry.Where(mater => mater.Material == TYPE).OrderByDescending(item => item.Quantity);

                        foreach (MaterialsTry userObj in materials)
                        {
                            InitializeMyGroupBox(X, Y + 100 * count, userObj.Name, userObj.Material, userObj.WayImage, userObj.Cost, userObj.Quantity, userObj.InStock);
                            count++;
                        }
                    }

                }
            }
        }

        private void InitializeMyGroupBox(int x, int y, String name, String type, String way, String price, String kolvo, String stock)
        {

            // Create and initialize a GroupBox and a Button control.
            GroupBox groupBox1 = new GroupBox();
            groupBox1.Size = new Size(500, 100);

            way = "D:/3-ИС/C#/ClothForHands/materials/" + way.Substring(11);
            PictureBox pb1 = new PictureBox();
            pb1.Image = Image.FromFile(way);
            pb1.Location = new Point(5, 10);
            pb1.Size = new Size(100, 100);
            pb1.SizeMode = PictureBoxSizeMode.StretchImage;

            Label lb1 = new Label();
            lb1.Text = type;
            lb1.Font = new Font("Candara", 10);
            lb1.Location = new Point(110, 10);

            Label lb2 = new Label();
            lb2.Text = name;
            lb2.Size = new Size(180, 20);
            lb2.Font = new Font("Candara", 10);
            lb2.Location = new Point(220, 10);


            Label lb7 = new Label();
            lb7.Text = "Количество:";
            lb7.Size = new Size(180, 20);
            lb7.Font = new Font("Candara", 10);
            lb7.Location = new Point(110, 30);

            Label lb3 = new Label();
            lb3.Text = kolvo;
            lb3.Size = new Size(180, 20);
            lb3.Font = new Font("Candara", 10);
            lb3.Location = new Point(200, 30);

            Label lb8 = new Label();
            lb8.Text = "Количество на складе:";
            lb8.Size = new Size(180, 20);
            lb8.Font = new Font("Candara", 10);
            lb8.Location = new Point(110, 50);

            Label lb4 = new Label();
            lb4.Text = stock;
            lb4.Font = new Font("Candara", 10);
            lb4.Location = new Point(250, 50);

            Label lb5 = new Label();
            lb5.Text = "Поставщики:";
            lb5.Font = new Font("Candara", 10);
            lb5.Location = new Point(110, 70);

            Label lb6 = new Label();
            lb6.Text = "ProviderGroup";
            lb6.Font = new Font("Candara", 10);
            lb6.Location = new Point(210, 70);

            groupBox1.Controls.Add(pb1);
            groupBox1.Controls.Add(lb1);
            groupBox1.Controls.Add(lb2);
            groupBox1.Controls.Add(lb3);
            groupBox1.Controls.Add(lb4);
            groupBox1.Controls.Add(lb5);
            groupBox1.Controls.Add(lb6);
            groupBox1.Controls.Add(lb7);
            groupBox1.Controls.Add(lb8);

            groupBox1.Location = new Point(x, y);
            //Controls.Add(groupBox1);
            flowLayoutPanel1.Controls.Add(groupBox1);

        }
       
        private void button1_Click(object sender, EventArgs e)
        {
           

        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            SORT = comboBox1.Text;
            TYPE = comboBox2.Text;
            getData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 Form2;
            Form2 = new Form2();
            Form2.Show();

            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (Model2 model = new Model2())
            {
                var materials = model.MaterialsTry;
                int total = 0;

                foreach (MaterialsTry userObj in materials)
                {
                    total = total + MyLib.Class1.getSumElems(Convert.ToInt32(userObj.InStock), Convert.ToInt32(userObj.Quantity), Convert.ToInt32(userObj.Cost));
                }
                label4.Text = "На складе товаров на "+(total.ToString())+" рублей";
            }

        }
    }
}
