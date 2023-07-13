using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyProtocolsApp_Cristian.ViewModels;
using Acr.UserDialogs;

namespace MyProtocolsApp_Cristian.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppLoginPage : ContentPage
    {

        //se realiza el anclaje entre esta vista y el VM que le da la funcionalidad
        UserViewModel viewModel;


        public AppLoginPage()
        {
            InitializeComponent();


            //esto vincula la vista con el viewmodel y ademas crea la instancia del obj
            this.BindingContext = viewModel = new UserViewModel();
        }

        private void SwShowPassword_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwShowPassword.IsToggled)
            {
                TxtPassword.IsPassword = false;

            }
            else
            {
                TxtPassword.IsPassword = true;
            }
        }
       
       

        private async void BtnLogin_Clicked_1(object sender, EventArgs e)
        {

            //validacion del ingreso del usuario a la app
            if (TxtUserName.Text != null && !string.IsNullOrEmpty(TxtUserName.Text.Trim()) &&
                TxtPassword.Text != null && !string.IsNullOrEmpty(TxtPassword.Text.Trim())    )
            {
                //SI HAY INFO EN LOS CUADROS DE TEXTO DE EMAIL Y PASS SE PROCEDE

                try
                {

                    //hacemos una animacion de espera

                    UserDialogs.Instance.ShowLoading("Veryfing User...");
                    await Task.Delay(2000);

                    string username = TxtUserName.Text.Trim();
                    string password = TxtPassword.Text.Trim();

                    bool R = await viewModel.UserAccessValidation(username, password);

                    if (R)
                    {
                        //si la validacion es correcta se permite el ingreso al systema
                        //igual que en progra 5 vamos a tener un usuario global

                        //TODO: crear el objeto de usuario global

                        await Navigation.PushAsync(new Startpage());
                        return;
                    }
                    else
                    {
                        //indicar que algo salio mal

                        await DisplayAlert("User Access Denied", "There was an issue with the email or password, please review", "OK");
                        return;
                    }


                }
                catch (Exception)
                {

                    throw;
                }
                finally 
                { 
                UserDialogs.Instance.HideLoading();
                }

            }
            else
            {
                await DisplayAlert("Data required", "Username and password must contain information... ", "OK");
                return;

            }


            


        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserSignUpPage());
        }
    }
}