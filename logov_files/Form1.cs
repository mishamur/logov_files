using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using logov_files.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace logov_files
{
    public partial class Form1 : Form
    {

        public string fileName { get; set; }
        public List<Worker> workers { get; set; }
        public Form1()
        {
            InitializeComponent();
            buttonEnterRecord.Hide();
            workers = new List<Worker>();
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(this.fileName, FileMode.OpenOrCreate))
            {
                // сериализуем весь массив people
                formatter.Serialize(fs, workers);
                
                //Console.WriteLine("Объект сериализован");
            }


        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            workers.Clear(); 
            openFileDialog1.Title = "Создать новый файл";
            openFileDialog1.ShowDialog();
            if(openFileDialog1.CheckFileExists & openFileDialog1.CheckPathExists)
            {
                this.fileName = openFileDialog1.FileName;
                
            }


            buttonEnterRecord.Show();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            workers.Clear();
            openFileDialog1.Title = "Открыть файл";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.CheckFileExists & openFileDialog1.CheckPathExists)
            {
                this.fileName = openFileDialog1.FileName;

            }

            BinaryFormatter formatter = new BinaryFormatter();

            using(FileStream fs = new FileStream(this.fileName, FileMode.OpenOrCreate))
            {
                if(fs.Length != 0)
                {
                    this.workers = (List<Worker>)formatter.Deserialize(fs);
                    foreach (var value in workers)
                    {
                        comboBox1.Items.Add(value);
                    }
                }
                else
                {
                    MessageBox.Show("Файл пуст, создайте новый!");
                    return;
                }
               
            }

            buttonEnterRecord.Show();
        }

        private void buttonEnterRecord_Click(object sender, EventArgs e)
        {
            Worker worker = new Worker(textBoxFio.Text, textBoxTabNumber.Text,
                            int.Parse(textBoxCountHour.Text), int.Parse(textBoxRate.Text));

            workers.Add(worker);

            comboBox1.Items.Add(worker.ToString());
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
