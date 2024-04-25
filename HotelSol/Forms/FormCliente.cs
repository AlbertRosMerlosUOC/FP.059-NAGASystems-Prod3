using System;
using System.Windows.Forms;
using CapaModelo;
public partial class FormCliente : Form
{
    public FormCliente()
    {
        // Configurar el formulario
        this.Text = "Cliente";
        this.StartPosition = FormStartPosition.CenterScreen;

        // Crear botones para cada operación
        Button buttonCrear = new Button
        {
            Text = "Crear cliente",
            Location = new System.Drawing.Point(100, 50),
        };
        buttonCrear.Click += (sender, e) => new FormCrearCliente().Show();
        this.Controls.Add(buttonCrear);

        Button buttonBorrar = new Button
        {
            Text = "Borrar cliente",
            Location = new System.Drawing.Point(100, 100),
        };
        buttonBorrar.Click += (sender, e) => new FormBorrarCliente().Show();
        this.Controls.Add(buttonBorrar);

        Button buttonDetalles = new Button
        {
            Text = "Detalles de cliente",
            Location = new System.Drawing.Point(100, 150),
        };
        buttonDetalles.Click += (sender, e) => new FormDetallesCliente().Show();
        this.Controls.Add(buttonDetalles);

        Button buttonEditar = new Button
        {
            Text = "Editar cliente",
            Location = new System.Drawing.Point(100, 200),
        };
        buttonEditar.Click += (sender, e) => new FormEditarCliente().Show();
        this.Controls.Add(buttonEditar);

        Button buttonImportar = new Button
        {
            Text = "Importar cliente",
            Location = new System.Drawing.Point(100, 250),
        };
        buttonImportar.Click += (sender, e) => new FormImportarCliente().Show();
        this.Controls.Add(buttonImportar);

        Button buttonAtras = new Button
        {
            Text = "Atrás",
            Location = new System.Drawing.Point(100, 300),
        };
        buttonAtras.Click += (sender, e) => this.Close();
        this.Controls.Add(buttonAtras);
    }
}

