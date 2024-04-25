using System;
using System.Windows.Forms;

public partial class FormInicio : Form
{
    public FormInicio()
    {
        InitializeComponent();

        // Configurar el formulario
        this.Text = "Página de inicio";
        this.StartPosition = FormStartPosition.CenterScreen;

        // Crear botones para cada sección
        Button buttonCliente = new Button
        {
            Text = "Cliente",
            Location = new System.Drawing.Point(100, 50),
        };
        buttonCliente.Click += (sender, e) => new FormCliente().Show();
        this.Controls.Add(buttonCliente);

        Button buttonHabitaciones = new Button
        {
            Text = "Habitaciones",
            Location = new System.Drawing.Point(100, 100),
        };
        buttonHabitaciones.Click += (sender, e) => new FormHabitaciones().Show();
        this.Controls.Add(buttonHabitaciones);

        Button buttonReservas = new Button
        {
            Text = "Reservas",
            Location = new System.Drawing.Point(100, 150),
        };
        buttonReservas.Click += (sender, e) => new FormReservas().Show();
        this.Controls.Add(buttonReservas);

        Button buttonPrivacidad = new Button
        {
            Text = "Privacidad",
            Location = new System.Drawing.Point(100, 200),
        };
        buttonPrivacidad.Click += (sender, e) => new FormPrivacidad().Show();
        this.Controls.Add(buttonPrivacidad);
    }

    private void InitializeComponent()
    {
            this.SuspendLayout();
            // 
            // FormInicio
            // 
            this.ClientSize = new System.Drawing.Size(704, 480);
            this.Name = "FormInicio";
            this.Load += new System.EventHandler(this.FormInicio_Load);
            this.ResumeLayout(false);

    }

    private void FormInicio_Load(object sender, EventArgs e)
    {

    }
}
