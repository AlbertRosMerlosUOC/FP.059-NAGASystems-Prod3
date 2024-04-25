using System;
using System.Data.Entity;
using System.Windows.Forms;
using CapaModelo;
using HotelSol.Data;

public partial class FormBorrarCliente : Form
{
    // Crear ComboBox para la lista de clientes
    private ComboBox comboBoxClientes = new ComboBox();

    // Crear botón para confirmar la eliminación
    private Button buttonEliminar = new Button();

    public FormBorrarCliente()
    {
        // Configurar el formulario
        this.Text = "Borrar cliente";
        this.StartPosition = FormStartPosition.CenterScreen;

        // Configurar y añadir ComboBox para la lista de clientes
        comboBoxClientes.Location = new System.Drawing.Point(100, 50);
        this.Controls.Add(comboBoxClientes);

        // Configurar y añadir botón para confirmar la eliminación
        buttonEliminar.Text = "Eliminar";
        buttonEliminar.Location = new System.Drawing.Point(100, 100);
        buttonEliminar.Click += ButtonEliminar_Click;
        this.Controls.Add(buttonEliminar);

        // Cargar la lista de clientes
        LoadClientes();
    }

    private async void LoadClientes()
    {
        using (var context = new FP_059_NAGASystems_Prod2Context())
        {
            var clientes = await context.Cliente.ToListAsync();
            comboBoxClientes.DataSource = clientes;
            comboBoxClientes.DisplayMember = "Nombre"; // Ajusta esto para que coincida con tu modelo Cliente
            comboBoxClientes.ValueMember = "DNI"; // Ajusta esto para que coincida con tu modelo Cliente
        }
    }

    private async void ButtonEliminar_Click(object sender, EventArgs e)
    {
        // Recoger el DNI del cliente que quieres eliminar
        string clienteDNI = comboBoxClientes.SelectedValue.ToString();

        // Eliminar el cliente de la base de datos
        using (var context = new FP_059_NAGASystems_Prod2Context())
        {
            var cliente = await context.Cliente.FindAsync(clienteDNI);
            if (cliente != null)
            {
                context.Cliente.Remove(cliente);
                await context.SaveChangesAsync();
            }
        }

        // Mostrar un mensaje al usuario
        MessageBox.Show("Cliente eliminado con éxito");

        // Recargar la lista de clientes
        LoadClientes();
    }
}
