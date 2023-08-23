using MyProtocolsApp_Cristian.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyProtocolsApp_Cristian.Models;

namespace MyProtocolsApp_Cristian.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserManagementPage : ContentPage
    {

        UserViewModel viewModel;
        public UserManagementPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new UserViewModel();
            LoadUserInfo();
            ValidateFields();


        }

        private void LoadUserInfo()
        {
            TxtID.Text = GlobalObjects.MyLocalUser.IDUsuario.ToString();
            TxtEmail.Text = GlobalObjects.MyLocalUser.Correo;
            TxtName.Text = GlobalObjects.MyLocalUser.Nombre;
            TxtPhoneNumber.Text = GlobalObjects.MyLocalUser.Telefono.ToString();
            TxtBackUpEmail.Text = GlobalObjects.MyLocalUser.CorreoRespaldo;
            TxtAddress.Text = GlobalObjects.MyLocalUser.Direccion;


        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            //primero hacemos validacion de campos requeridos

            if (ValidateFields())
            {

                //SACAR UN RESPALDO DEL USUARIO GLOBAL TAL Y COMO ESTA EN EL MOMENTO 
                //POR SI ALGO SALE MAL EN EL PROCESO DE UPDATE, REVERSAR LOS CAMBIOS

                UserDTO BackupLocaluser = new UserDTO();
                BackupLocaluser = GlobalObjects.MyLocalUser;


                try
                {

                    GlobalObjects.MyLocalUser.Nombre = TxtName.Text.Trim();
                    GlobalObjects.MyLocalUser.CorreoRespaldo = TxtBackUpEmail.Text.Trim();
                    GlobalObjects.MyLocalUser.Telefono = TxtPhoneNumber.Text.Trim();
                    GlobalObjects.MyLocalUser.Direccion = TxtAddress.Text.Trim();

                    var answer = await DisplayAlert("???", "Are you sure you want to continue updating the info?", "Yes", "No");

                    if (answer)
                    {
                        bool R = await viewModel.UpdateUser(GlobalObjects.MyLocalUser);

                        if (R)
                        {
                            await DisplayAlert(":)", "User Updated", "OK");
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert(":(", "Something went wrong", "OK");
                            await Navigation.PopAsync();
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }


        }
        private bool ValidateFields()
        {
            bool R = false;
            if (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim()) &&
                TxtBackUpEmail.Text != null && !string.IsNullOrEmpty(TxtBackUpEmail.Text.Trim()) &&
                TxtPhoneNumber.Text != null && !string.IsNullOrEmpty(TxtBackUpEmail.Text.Trim()))
            {
                //en este caso estan todos los datos requeridos
                R = true;
            }
            else
            {
                //si falta algun dato
                if (TxtName.Text == null || string.IsNullOrEmpty(TxtName.Text.Trim()) )
                {
                    DisplayAlert("Validation failed", "The name is required", "OK");
                    TxtName.Focus();
                    return false;
                }

                if (TxtBackUpEmail.Text == null || string.IsNullOrEmpty(TxtBackUpEmail.Text.Trim()))
                {
                    DisplayAlert("Validation failed", "The backup email is required", "OK");
                    TxtBackUpEmail.Focus();
                    return false;
                }

                if (TxtPhoneNumber.Text == null || string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()))
                {
                    DisplayAlert("Validation failed", "The phone number is required", "OK");
                    TxtPhoneNumber.Focus();
                    return false;
                }
            }





            return R;
        }
        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}