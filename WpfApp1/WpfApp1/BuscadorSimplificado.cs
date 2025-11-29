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