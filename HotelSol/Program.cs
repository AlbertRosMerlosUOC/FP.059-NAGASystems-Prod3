using System;
using System.Windows.Forms;

namespace HotelSol
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<FP_059_NAGASystems_Prod2Context>());

            using (var context = new FP_059_NAGASystems_Prod2Context())
            {
                // Aquí puedes usar tu contexto para interactuar con la base de datos
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
