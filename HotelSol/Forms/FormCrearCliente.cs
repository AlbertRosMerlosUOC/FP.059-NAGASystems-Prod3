using HotelSol.Data;
using CapaModelo;
using System;
using System.Windows.Forms;

public partial class FormCrearCliente : Form
{
    // Crear TextBox para cada campo
    private TextBox textBoxDNI = new TextBox();
    private TextBox textBoxNombre = new TextBox();
    private TextBox textBoxApellido1 = new TextBox();
    private TextBox textBoxApellido2 = new TextBox();
    private TextBox textBoxTelefono = new TextBox();
    private TextBox textBoxDireccion = new TextBox();
    private TextBox textBoxEmail = new TextBox();
    private TextBox textBoxVIP = new TextBox();
    private TextBox textBoxEstado = new TextBox();

    public FormCrearCliente()
    {
        // Configurar el formulario
        this.Text = "Crear cliente";
        this.StartPosition = FormStartPosition.CenterScreen;

        // Crear Label y TextBox para cada campo
        AddLabelAndTextBox("DNI", textBoxDNI, 50);
        AddLabelAndTextBox("Nombre", textBoxNombre, 100);
        AddLabelAndTextBox("Apellido1", textBoxApellido1, 150);
        AddLabelAndTextBox("Apellido2", textBoxApellido2, 200);
        AddLabelAndTextBox("Telefono", textBoxTelefono, 250);
        AddLabelAndTextBox("Direccion", textBoxDireccion, 300);
        AddLabelAndTextBox("Email", textBoxEmail, 350);
        AddLabelAndTextBox("VIP", textBoxVIP, 400);
        AddLabelAndTextBox("Estado", textBoxEstado, 450);

        // Crear botón para enviar el formulario
        Button buttonCrear = new Button
        {
            Text = "Crear",
            Location = new System.Drawing.Point(100, 500),
        };
        buttonCrear.Click += ButtonCrear_Click;
        this.Controls.Add(buttonCrear);
    }

    private void AddLabelAndTextBox(string labelText, TextBox textBox, int top)
    {
        Label label = new Label
        {
            Text = labelText,
            Location = new System.Drawing.Point(100, top),
            AutoSize = true,
        };
        this.Controls.Add(label);

        textBox.Location = new System.Drawing.Point(200, top);
        this.Controls.Add(textBox);
    }

    private async void ButtonCrear_Click(object sender, EventArgs e)
    {
        // Crear una nueva instancia de Cliente
        var cliente = new Cliente
        {
            DNI = textBoxDNI.Text,
            Nombre = textBoxNombre.Text,
            Apellido1 = textBoxApellido1.Text,
            Apellido2 = textBoxApellido2.Text,
            Telefono = textBoxTelefono.Text,
            Direccion = textBoxDireccion.Text,
            Email = textBoxEmail.Text,
            VIP = int.Parse(textBoxVIP.Text),
            Estado = int.Parse(textBoxEstado.Text),
        };

        // Agregar el cliente a la base de datos
        using (var context = new FP_059_NAGASystems_Prod2Context())
        {
            context.Cliente.Add(cliente);
            await context.SaveChangesAsync();
        }

        // Mostrar un mensaje al usuario
        MessageBox.Show("Cliente creado con éxito");
    }

    private void InitializeComponent()
    {
            this.SuspendLayout();
            // 
            // FormCrearCliente
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "FormCrearCliente";
            this.Load += new System.EventHandler(this.FormCrearCliente_Load);
            this.ResumeLayout(false);

    }

    private void FormCrearCliente_Load(object sender, EventArgs e)
    {

    }
}

