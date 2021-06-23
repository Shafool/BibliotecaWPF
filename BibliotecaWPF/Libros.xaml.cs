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
        public Libros()
        {
            InitializeComponent();
            Listar();
        }

        protected void Listar()
        {
            servicio = new ServicioBiblioteca.wsInvestigacionSoapClient();

            string consulta = tbBusqueda.Text;
            DataSet datos = servicio.ListarLibro();

            dataGrid.ItemsSource = datos.Tables;
        }
    }
}

