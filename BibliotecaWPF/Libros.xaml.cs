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
    /// Lógica de interacción para Libros.xaml
    /// </summary>
    public partial class Libros : Window
    {
        private ServicioBiblioteca.wsInvestigacionSoapClient servicio;
        /* Variables globales */
        string modo = "crear";
        public Libros()
        {
            InitializeComponent();
            Listar();
        }

        protected void Listar()
        {
            servicio = new ServicioBiblioteca.wsInvestigacionSoapClient();
            DataSet datos = servicio.ListarLibro();
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
            lbFormulario.Content = "Editando Libro";
            tbCodigo.IsEnabled = false;

            // Seleccionar el item en la tabla
            DataRowView fila = dataGrid.SelectedItem as DataRowView;

            // Pintar los datos en el formulario
            tbCodigo.Text = fila["CodLibro"].ToString();
            tbTitulo.Text = fila["Titulo"].ToString();
            tbEditorial.Text = fila["Editorial"].ToString();
        }

        private void btEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Condiciones
            if (dataGrid.SelectedItems.Count != 1) return;

            servicio = new ServicioBiblioteca.wsInvestigacionSoapClient();
            // Seleccionar el item en la tabla
            DataRowView fila = dataGrid.SelectedItem as DataRowView;
            String cod = fila["CodLibro"].ToString();

            servicio.EliminarLibro(cod);
            Listar();
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (tbBusqueda.Text == null || tbBusqueda.Text == "") return;

            String busqueda = tbBusqueda.Text.Trim();

            servicio = new ServicioBiblioteca.wsInvestigacionSoapClient();
            DataSet datos = servicio.BuscarLibro(busqueda);
            dataGrid.DataContext = datos.Tables[0];

        }

        private void button_Copy3_Click(object sender, RoutedEventArgs e)
        {
            if (!formularioValido()) // Validacion
            {
                MessageBox.Show("Llene toda la información");
                return;
            }
            servicio = new ServicioBiblioteca.wsInvestigacionSoapClient();

            // Tomar todo el texto de los campos -> en strings
            string sCodigo = tbCodigo.Text.Trim();
            string sTitulo= tbTitulo.Text.Trim();
            string sEditorial = tbEditorial.Text.Trim();

            if (modo == "editar")
            {
                servicio.ActualizarLibro(sCodigo, sTitulo, sEditorial);
            }
            else if (modo == "crear")
            {
                servicio.AgregarLibro(sCodigo, sTitulo, sEditorial);
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
            tbTitulo.Text = "";
            tbEditorial.Text = "";
        }


        private bool formularioValido()
        {
            // Validaciones
            if (string.IsNullOrEmpty(tbCodigo.Text) ||
                string.IsNullOrEmpty(tbTitulo.Text) ||
                string.IsNullOrEmpty(tbEditorial.Text))
            {
                return false;
            }
            return true;
        }
    }
}
