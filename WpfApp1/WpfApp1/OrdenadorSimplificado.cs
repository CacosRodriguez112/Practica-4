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