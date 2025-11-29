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
        return $"{Nombre} - {Categoria} | {Precio:C} | Stock: {Stock}";
    }

}

