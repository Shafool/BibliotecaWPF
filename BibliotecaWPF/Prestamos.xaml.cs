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
    /// Lógica de interacción para Prestamos.xaml
    /// </summary>
    public partial class Prestamos : Window
    {
        private ServicioBiblioteca.wsInvestigacionSoapClient servicio;
        public Prestamos()
        {
            InitializeComponent();

            ListarLibro();
            ListarAutor();
            ListarPrestamo();
        }

        protected void ListarLibro()
        {
            servicio = new ServicioBiblioteca.wsInvestigacionSoapClient();
            DataSet datos = servicio.ListarLibro();
            dgLibro.DataContext = datos.Tables[0];
        }
        protected void ListarAutor()
        {
            servicio = new ServicioBiblioteca.wsInvestigacionSoapClient();
            DataSet datos = servicio.ListarAutor();
            dgAutor.DataContext = datos.Tables[0];
        }
        protected void ListarPrestamo()
        {
           servicio = new ServicioBiblioteca.wsInvestigacionSoapClient();
            DataSet datos = servicio.ListarPrestamo();
            dgPrestamo.DataContext = datos.Tables[0];
        }

        protected void btListarAutor_Click(object sender, RoutedEventArgs e)
        {
            ListarAutor();
        }

        protected void btListarLibro_Click(object sender, RoutedEventArgs e)
        {
            ListarLibro();
        }

        protected void btAutor_Click(object sender, RoutedEventArgs e)
        {
            if (tbAutor.Text == null || tbAutor.Text == "") return;

            String busqueda = tbAutor.Text.Trim();

            servicio = new ServicioBiblioteca.wsInvestigacionSoapClient();
            DataSet datos = servicio.BuscarAutor(busqueda);
            dgAutor.DataContext = datos.Tables[0];
        }

        protected void btLibro_Click(object sender, RoutedEventArgs e)
        {
            if (tbLibro.Text == null || tbLibro.Text == "") return;

            String busqueda = tbLibro.Text.Trim();

            servicio = new ServicioBiblioteca.wsInvestigacionSoapClient();
            DataSet datos = servicio.BuscarLibro(busqueda);
            dgLibro.DataContext = datos.Tables[0];
        }

        protected void btPrestar_Click(object sender, RoutedEventArgs e)
        {
            // Condiciones
            if (dgLibro.SelectedItems.Count != 1) return;
            if (dgAutor.SelectedItems.Count != 1) return;

            servicio = new ServicioBiblioteca.wsInvestigacionSoapClient();
            // Seleccionar el item en la tabla
            DataRowView libro = dgLibro.SelectedItem as DataRowView;
            DataRowView autor = dgAutor.SelectedItem as DataRowView;

            servicio.AgregarPrestamo(autor["CodAutor"].ToString(), libro["CodLibro"].ToString());
            ListarPrestamo();
        }

        private void btRecepcionar_Click(object sender, RoutedEventArgs e)
        {
            // Condiciones
            if (dgPrestamo.SelectedItems.Count != 1) return;

            servicio = new ServicioBiblioteca.wsInvestigacionSoapClient();
            // Seleccionar el item en la tabla
            DataRowView prestamo = dgPrestamo.SelectedItem as DataRowView;

            servicio.EliminarPrestamo(prestamo["CodAutor"].ToString(), prestamo["CodLibro"].ToString());
            ListarPrestamo();
        }
    }
}
