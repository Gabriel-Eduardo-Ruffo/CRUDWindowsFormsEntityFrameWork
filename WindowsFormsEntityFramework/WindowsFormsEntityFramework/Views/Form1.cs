using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsEntityFramework.Models;
using WindowsFormsEntityFramework.Views;

namespace WindowsFormsEntityFramework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        #region HELPER
        private void RefreshGrid()
        {
            using (TestCRUDEntities db = new TestCRUDEntities())
            {
                var list = 
                (
                    from d in db.Persons
                    select d
                );
                dataGridView1.DataSource = list.ToList();               
            }
        }

        private int? GetId()
        {
            try
            {
                //      parsear a int    Del DataGrid  La Fila seleccionada (el index) el valor de la celda 0 convertido a string
                return int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch
            {
                return null;
            }
        }

        #endregion

        private void btn_Create_Click(object sender, EventArgs e)
        {
            FormTable formTable = new FormTable();
            formTable.ShowDialog();
            RefreshGrid();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            int? id = GetId();
            if (id != null)
            {
                FormTable formTable = new FormTable(id);
                formTable.ShowDialog();
                RefreshGrid();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            int? id = GetId();
            if (id != null)
            {
                using (TestCRUDEntities db = new TestCRUDEntities())
                {
                    Persons searchIdPersons = db.Persons.Find(id);
                    db.Persons.Remove(searchIdPersons);
                    db.SaveChanges();
                }
                RefreshGrid();
            }
        }
    }
}
