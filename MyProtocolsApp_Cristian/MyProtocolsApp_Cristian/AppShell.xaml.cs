using MyProtocolsApp_Cristian.ViewModels;
using MyProtocolsApp_Cristian.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyProtocolsApp_Cristian
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
