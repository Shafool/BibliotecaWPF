using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BibliotecaWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btClientes_Click(object sender, RoutedEventArgs e)
        {
            Libros interfaz = new Libros();
            interfaz.Show();
        }

        private void btVendedores_Click(object sender, RoutedEventArgs e)
        {
            Autores interfaz = new Autores();
            interfaz.Show();
        }

        private void btPrestamos_Click(object sender, RoutedEventArgs e)
        {
            Prestamos interfaz = new Prestamos();
            interfaz.Show();
        }
    }
}
