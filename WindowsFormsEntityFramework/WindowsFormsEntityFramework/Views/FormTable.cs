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

namespace WindowsFormsEntityFramework.Views
{
    public partial class FormTable : Form
    {
        private int? id;
        Persons persons = null;//iniciamos en null por que depende de si es crear o editar, vamos a inicializar o modificar esta variable objeto
        public FormTable(int? _id =null)
        {
            InitializeComponent();
            //Si recive un id vamos a cargar los componentes textBox del formularios con los
            //datos que nos devuelva la busqueda en la DB
            this.id = _id;
            if(this.id != null)
            {
                FillDataFormUpdate();//buscamos los datos en la DB y los cargamos en el form
            }
        }

        private void FillDataFormUpdate()
        {
            using (TestCRUDEntities db = new TestCRUDEntities())
            {
                
                persons = db.Persons.Find(id);
                txt_firstName.Text = persons.FirstName;
                txt_lastName.Text = persons.LastName;
                txt_age.Text = persons.Age.ToString();
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            //TestCRUDEntities hace referencia al ORM entity framework que esta en Models que se seteo
            //con la DB y su tabla Persons 
            using (TestCRUDEntities db = new TestCRUDEntities())
            {
                if (id == null)
                {
                    //Este objeto tiene el mismo nombre que la tabla en la DB que vamos a acceder.
                    persons = new Persons();
                }
                
                //aca accedemos a las columnas de la tabla
                persons.FirstName = txt_firstName.Text;
                persons.LastName = txt_lastName.Text;
                persons.Age = Convert.ToInt32(txt_age.Text);

                if(id == null)
                {
                    db.Persons.Add(persons);
                }
                else
                {
                    db.Entry(persons).State = System.Data.Entity.EntityState.Modified;
                }
                
                db.SaveChanges();
                this.Close();
            }
        }
    }
}
