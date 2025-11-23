
//Actividad 1


public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string CodigoBarras { get; set; } // Campo unico
    public string Categoria { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }

    public string Imagen { get; set; }


    public override string ToString()
    {
        return $"[{Id}] {CodigoBarras} - {Nombre}| {Precio:C} | Stock: {Stock}|{Categoria}";
    }
}





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


//Actividad 2 Ordenamiento


public class OrdenadorSimplificado
{


    //Ordanemitno por QuickSort
    public static void QuickSortPorId(List<Producto> productos)
    {
        if (productos.Count <= 1)
            return;


        // 1- Seleccionar un elemento como pivote (ultimo elemento de la lista)
        Producto pivote = productos[productos.Count - 1];


        // 2- Reorganizar lista con elementos menores a la izquierda y elementos mayores a la derecha
        var menores = new List<Producto>();
        var mayores = new List<Producto>();


        for (int i = 0; i < productos.Count - 1; i++)
        {
            if (productos[i].Id < pivote.Id)
            {
                menores.Add(productos[i]);
            }
            else
            {
                mayores.Add(productos[i]);
            }
        }


        // 3- Recursivamente, aplica el algoritmo en las sublistas formadas
        QuickSortPorId(menores);
        QuickSortPorId(mayores);


        // 4- Combinar las soblistas para optener la lista ordenada
        productos.Clear();
        productos.AddRange(menores);
        productos.Add(pivote);
        productos.AddRange(mayores);
    }


    public static void QuickSortPorPrecio(List<Producto> productos)
    {
        if (productos.Count <= 1)
            return;


        // 1- Seleccionar un elemento como pivote (ultimo elemento de la lista)
        Producto pivote = productos[productos.Count - 1];


        // 2- Reorganizar lista con elementos menores a la izquierda y elementos mayores a la derecha
        var menores = new List<Producto>();
        var mayores = new List<Producto>();


        for (int i = 0; i < productos.Count - 1; i++)
        {
            if (productos[i].Precio < pivote.Precio)
            {
                menores.Add(productos[i]);
            }
            else
            {
                mayores.Add(productos[i]);
            }
        }


        // 3- Recursivamente, aplica el algoritmo en las sublistas formadas
        QuickSortPorPrecio(menores);
        QuickSortPorPrecio(mayores);


        // 4- Combinar las soblistas para optener la lista ordenada
        productos.Clear();
        productos.AddRange(menores);
        productos.Add(pivote);
        productos.AddRange(mayores);
    }


    //Ordanemiento por MergeSort


    public static List<Producto> MergeSortPorNombre(List<Producto> productos)
    {
        if (productos.Count <= 1)
            return productos;


        //1- Divir lista en dos partes (mitad)
        int mitad = productos.Count / 2;
        var izquierda = productos.GetRange(0, mitad);
        var derecha = productos.GetRange(mitad, productos.Count - mitad);


        // 2- Repite proceso para cada mitad (hasta tener solo un elemento)


        izquierda = MergeSortPorNombre(izquierda);
        derecha = MergeSortPorNombre(derecha);


        //3- Mezclar las mitades ordenadas
        return Mezclar(izquierda, derecha);
    }


    private static List<Producto> Mezclar(List<Producto> izquierda, List<Producto> derecha)
    {
        //Comparar el primer elemento de cada mitad:
        // * El menor se coloca primero en la nueva lista 


        var resultado = new List<Producto>();
        int i = 0, j = 0;


        //Comparamos y agregamos orden
        while (i < izquierda.Count && j < derecha.Count)
        {
            if (string.Compare(izquierda[i].Nombre, derecha[j].Nombre) < 0)
            {
                resultado.Add(izquierda[i++]);
            }
            else
            {
                resultado.Add(derecha[j++]);
            }
        }
        //Agregar elementos restantes
        while (i < izquierda.Count)
            resultado.Add(izquierda[i++]);
        while (j < derecha.Count)
            resultado.Add(derecha[j++]);
        return resultado;
    }
}


//Actividad 3 Busqueda


public class BuscadorSimplificado
{
    //Busqueda binaria (lista ordenada por Id)
    public static (Producto, int) BusquedaBinaria(List<Producto> productos, int idBuscado)
    {
        int inicio = 0;
        int fin = productos.Count - 1;
        int iteraciones = 0;


        //Parte de una lista ordenada
        //Divida en la mitad la lista
        //1- Calcular el punto medio


        while (inicio <= fin)
        {
            iteraciones++;


            //1- La mitad de la lista ordenada


            int medio = (inicio + fin) / 2;


            // 2- Comprobar si encontramos el ID


            if (productos[medio].Id == idBuscado)
            {
                return (productos[medio], iteraciones);
            }


            // 3- Ajustar los limites de busqueda


            if (productos[medio].Id < idBuscado)
            {
                inicio = medio + 1; // Buscar en la mitad derecha
            }
            else
            {
                fin = medio - 1; // Buscar en la mitad izquierda
            }
        }


        return (null, iteraciones); //No encontrado


    }


    // Busqueda secuencial


    public static (Producto, int) BusquedaSecuencialNombre(List<Producto> productos, string nombreBuscado)
    {
        int iteraciones = 0;


        // 1- Recorre la lista uno por uno


        foreach (Producto producto in productos)
        {
            iteraciones++;
            // 2- Comparamos el nombre (sin distinguir mayusculas/minusculas)


            if (producto.Nombre.Equals(nombreBuscado, StringComparison.OrdinalIgnoreCase))
            {
                return (producto, iteraciones);
            }
        }
        return (null, iteraciones); // No encontrado
    }
}