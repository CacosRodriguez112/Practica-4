public class GestorProductos
{
    //LISTA: para mantener orden de insercion 
    private List<Producto> listaProductos = new List<Producto>();


    //DICCIONARIO: Busquedas 
    private Dictionary<string, Producto> diccionarioPorCodigo = new Dictionary<string, Producto>();


    public List<Producto> ObtenerListaProductos()
    {
        return new List<Producto>(listaProductos);
    }


    //Operaciones LISTA
    public void AgregarProducto(Producto p)
    {
        //Validad codigo de barras unico
        if (diccionarioPorCodigo.ContainsKey(p.CodigoBarras))
        {
            throw new Exception("El codigo de barras ya existe");
        }
        listaProductos.Add(p);
        diccionarioPorCodigo[p.CodigoBarras] = p;
    }



    public void MostrarProductosPorCategoria(string categoria)
    {
        //Usamos lista para recorridos secuenciales
        Console.WriteLine($"Productos en categoria {categoria}:");
        foreach (var item in listaProductos)
        {
            if (item.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
    public void MostrarInventario()
    {
        Console.WriteLine("Inventario en orden de ingreso:");
        foreach (var item in listaProductos)
        {
            Console.WriteLine(item.ToString());
        }
    }


    //Operaciones con DICCIONARIO (busquedas especificas)
    public Producto BuscarPorCodigo(string codigoBarras)
    {
        return diccionarioPorCodigo.TryGetValue(codigoBarras, out var producto) ? producto : null;
    }
    public bool ExisteProducto(string codigoBarras)
    {
        return diccionarioPorCodigo.ContainsKey(codigoBarras);
    }
    public bool EliminarProducto(string codigoBarras)
    {
        if (diccionarioPorCodigo.TryGetValue(codigoBarras, out var producto))
        {
            listaProductos.Remove(producto);
            diccionarioPorCodigo.Remove(codigoBarras);
            return true;
        }
        return false;
    }
}