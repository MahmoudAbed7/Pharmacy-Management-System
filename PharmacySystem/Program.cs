using PharmacySystem.Category;
using PharmacySystem.Login;
using PharmacySystem.People;
using PharmacySystem.Products;
using PharmacySystem.UserJoiningApplication;
using PharmacySystem.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySystem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmListAllProducts());
        }
    }
}
