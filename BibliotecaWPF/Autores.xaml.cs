using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BibliotecaWPF
{
    /// <summary>
    /// Lógica de interacción para Autores.xaml
    /// </summary>
    public partial class Autores : Window
    {
        private ServicioBiblioteca.wsInvestigacionSoapClient servicio;
        /* Variables globales */
        string modo = "crear";
        public Autores()
        {
            InitializeComponent();
            Listar();
        }

        protected void Listar()
        {
            servicio = new ServicioBiblioteca.wsInvestigacionSoapClient();
            DataSet datos = servicio.ListarAutor();
            dataGrid.DataContext = datos.Tables[0];
        }

        private void btListarTodo_Click(object sender, RoutedEventArgs e)
        {
            Listar();
        }

        private void btCrearOpcion_Click(object sender, RoutedEventArgs e)
        {
            modo = "crear";
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            // Condiciones
            if (dataGrid.SelectedItems.Count != 1) return;

            modo = "editar";
            lbFormulario.Content = "Editando Autor";
            tbCodigo.IsEnabled = false;

            // Seleccionar el item en la tabla
            DataRowView fila = dataGrid.SelectedItem as DataRowView;

            // Pintar los datos en el formulario
            tbCodigo.Text = fila["CodAutor"].ToString();
            tbNombres.Text = fila["Nombres"].ToString();
            tbApellidos.Text = fila["Apellidos"].ToString();
            tbNacionalidad.Text = fila["Nacionalidad"].ToString();
        }

        private void btEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Condiciones
            if (dataGrid.SelectedItems.Count != 1) return;

            servicio = new ServicioBiblioteca.wsInvestigacionSoapClient();
            // Seleccionar el item en la tabla
            DataRowView fila = dataGrid.SelectedItem as DataRowView;
            String cod = fila["CodAutor"].ToString();
            
            servicio.EliminarAutor(cod);
            Listar();
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (tbBusqueda.Text == null || tbBusqueda.Text == "") return;

            String busqueda = tbBusqueda.Text.Trim();

            servicio = new ServicioBiblioteca.wsInvestigacionSoapClient();
            DataSet datos = servicio.BuscarAutor(busqueda);
            dataGrid.DataContext = datos.Tables[0];

        }

        private void button_Copy3_Click(object sender, RoutedEventArgs e)
        {
            if (!formularioValido()) // Validacion
            {
                MessageBox.Show("Llene toda su información");
                return;
            }
            servicio = new ServicioBiblioteca.wsInvestigacionSoapClient();

            // Tomar todo el texto de los campos -> en strings
            string sCodigo = tbCodigo.Text.Trim();
            string sNombre = tbNombres.Text.Trim();
            string sApellidos = tbApellidos.Text.Trim();
            string sNacionalidad = tbNacionalidad.Text.Trim();

            if (modo == "editar")
            {
                servicio.ActualizarAutor(sCodigo, sNombre, sApellidos, sNacionalidad);
            }
            else if (modo == "crear")
            {
                servicio.AgregarAutor(sCodigo, sNombre, sApellidos, sNacionalidad);
            }
            Listar();
            cancelar();
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e) { cancelar(); }

        private void cancelar()
        {
            modo = "crear";
            lbFormulario.Content = "Añadir un Libro ";
            tbCodigo.IsEnabled = true;

            // Limpia todos los campos
            tbCodigo.Text = "";
            tbNombres.Text = "";
            tbApellidos.Text = "";
            tbNacionalidad.Text = "";
        }


        private bool formularioValido()
        {
            // Validaciones
            if (string.IsNullOrEmpty(tbApellidos.Text) ||
                string.IsNullOrEmpty(tbCodigo.Text) ||
                string.IsNullOrEmpty(tbNombres.Text) ||
                string.IsNullOrEmpty(tbNacionalidad.Text))
            {
                return false;
            }
            return true;
        }
    }
}
