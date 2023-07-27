using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyProtocolsApp_Cristian.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Startpage : ContentPage
    {
        public Startpage()
        {
            InitializeComponent();
            LoadUsername();

        }
        private void LoadUsername()
        {
            LblUserName.Text = GlobalObjects.MyLocalUser.Nombre;
        }
    }
}