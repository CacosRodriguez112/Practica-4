using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace GestorProductosWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GestorProductos gestor = new GestorProductos();
        public MainWindow()
        {
            InitializeComponent();
            CargarDatosIniciales();


            dataGridProductos.ItemsSource = gestor.ObtenerListaProductos();


            comboTipoBusqueda.Items.Add("ID");
            comboTipoBusqueda.Items.Add("Nombre");
            comboTipoBusqueda.SelectedIndex = 0;


            comboCirterioOrden.Items.Add("ID");
            comboCirterioOrden.Items.Add("Precio");
            comboCirterioOrden.Items.Add("Nombre");
            comboCirterioOrden.SelectedIndex = 0;
        }


        private void CargarDatosIniciales()
        {
            gestor.AgregarProducto(new Producto
            {
                Id = 3,
                CodigoBarras = "789456",
                Nombre = "Teclado",
                Categoria = "Electronica",
                Precio = 300,
                Stock = 20
            }
            );
            gestor.AgregarProducto(new Producto
            {
                Id = 15,
                CodigoBarras = "123456",
                Nombre = "Computadora",
                Categoria = "Electronica",
                Precio = 12000,
                Stock = 10
            }
            );
            gestor.AgregarProducto(new Producto
            {
                Id = 1,
                CodigoBarras = "147852",
                Nombre = "Bocina",
                Categoria = "Electronica",
                Precio = 4000,
                Stock = 2
            }
            );
            gestor.AgregarProducto(new Producto
            {
                Id = 5,
                CodigoBarras = "258963",
                Nombre = "USB",
                Categoria = "Electronica",
                Precio = 150,
                Stock = 30
            }
            );


            gestor.AgregarProducto(new Producto
            {
                Id = 9,
                CodigoBarras = "369258",
                Nombre = "Sudadera",
                Categoria = "Ropa",
                Precio = 300,
                Stock = 15
            }
            );
        }


        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string criterio = comboTipoBusqueda.SelectedItem.ToString();
            string valor = txtBusqueda.Text;


            //La lista ordenada


            List<Producto> productosParaBusqueda = new List<Producto>(gestor.ObtenerListaProductos());
            OrdenadorSimplificado.QuickSortPorId(productosParaBusqueda);


            switch (criterio)
            {
                case "ID":
                    if (int.TryParse(valor, out int id))
                    {
                        var (producto, iteraciones) = BuscadorSimplificado.BusquedaBinaria(productosParaBusqueda, id);
                        MostrarResultado(producto, id);
                    }
                    break;
                case "Nombre":
                    var (productoNombre, iteracionesNombre) = BuscadorSimplificado.BusquedaSecuencialNombre(productosParaBusqueda, valor);
                    MostrarResultado(productoNombre, iteracionesNombre);
                    break;


            }
        }


        private void MostrarResultado(Producto producto, int iteraciones)
        {
            txtResultadoBusqueda.Text = producto?.ToString() ?? "No encontrado";
            progressIteraciones.Value = iteraciones * 5;
        }
        private void btnOrdenar_Click(object sender, RoutedEventArgs e)
        {
            List<Producto> productos = new List<Producto>(gestor.ObtenerListaProductos());
            string criterio = comboCirterioOrden.SelectedItem.ToString();


            switch (criterio)
            {
                case "ID":
                    OrdenadorSimplificado.QuickSortPorId(productos);
                    break;
                case "Nombre":
                    productos = OrdenadorSimplificado.MergeSortPorNombre(productos);
                    break;
                case "Precio":
                    OrdenadorSimplificado.QuickSortPorPrecio(productos);
                    break;
            }
            listViewOrdenados.ItemsSource = productos;
            DibujarGraficoBarras(productos);
        }


        private void DibujarGraficoBarras(List<Producto> productos)
        {
            canvasGrafico.Children.Clear();
            double maxPrecio = (double)productos.Max(p => p.Precio);
            double escala = canvasGrafico.ActualHeight / maxPrecio;


            for (int i = 0; i < productos.Count; i++)
            {
                Rectangle barra = new Rectangle
                {
                    Width = 30,
                    Height = (double)productos[i].Precio * escala,
                    Fill = Brushes.Purple,
                    Margin = new Thickness(i * 40, canvasGrafico.ActualHeight - ((double)productos[i].Precio * escala), 0, 0)
                };


                canvasGrafico.Children.Add(barra);


                TextBlock etiqueta = new TextBlock
                {
                    Text = productos[i].Nombre,
                    Margin = new Thickness(i * 40, canvasGrafico.ActualHeight - 20, 0, 0)
                };
                canvasGrafico.Children.Add(etiqueta);
            }
        }


        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            var ventanaAgregar = new AgregarProductoWindow();
            if (ventanaAgregar.ShowDialog() == true)
            {
                Producto nuevoProducto = ventanaAgregar.Producto;//Asigna el producto creado
                try
                {
                    gestor.AgregarProducto(nuevoProducto);
                    dataGridProductos.ItemsSource = null;
                    dataGridProductos.ItemsSource = gestor.ObtenerListaProductos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProductos.SelectedItem is Producto productoSeleccionado)
            {
                bool eliminado = gestor.EliminarProducto(productoSeleccionado.CodigoBarras);
                if (eliminado)
                {
                    dataGridProductos.ItemsSource = null;
                    dataGridProductos.ItemsSource = gestor.ObtenerListaProductos();
                    MessageBox.Show("Producto eliminado", "Eliminado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Selecciona un producto a eliminar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}