using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyProtocolsApp_Cristian.Models;

namespace MyProtocolsApp_Cristian.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        //El VM funciona como puente entre el modelo y la vista
        //En un sentido teorico, el VM "siente" los cambios de la vista y los pasa al modelo
        //de forma automatica o viceversa segun se use en uno o dos sentidos.

        //tambien se puede usar como en este caso particular, simplemente como mediador de procesos
        //Mas adelante se usara commands y bindings en dos sentidos

        // primero en formato de funciones clasicas

        public User MyUser { get; set; }

        public UserRole MyUserRole { get; set; }
        
        public UserDTO MyUserDTO { get; set; }

      
        public UserViewModel()
        {
                MyUser = new User();
                MyUserRole = new UserRole();
                MyUserDTO = new UserDTO();
        }

        //funciones 


        //funcion que carga los datos del objeto de usuario global

        public async Task<UserDTO> GetUserDataAsync(string pEmail)
        {
            if (IsBusy) return null;
            IsBusy = true;

            try
            {

                UserDTO userDTO = new UserDTO();

                userDTO = await MyUserDTO.GetUserInfo(pEmail);

                if (userDTO == null) return null;
                return userDTO;
              
            }
            catch (Exception)
            {
                return null;
                throw;
            }
            finally { IsBusy = false; }

        }

        public async Task<bool> UpdateUser(UserDTO pUser)
        {
            if (IsBusy) return false;
            IsBusy = true;

            try
            {

                MyUserDTO = pUser;

                bool R = await MyUserDTO.UpdateUserAsync();

                return R;        

            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally { IsBusy = false; }

        }

        //funcion para validar el ingreso del usuario al app por medio del login
        public async Task<bool> UserAccessValidation(string pEmail, string pPassword)
        {

            //debemos poder controlar que no se ejecute la operación mas de una vez
            //en este caso hay una funcionalidad  pensada para eso en BaseViewModel que
            //fue heredada al definir esta clase
            //Se usará una propiedad llamada "IsBusy" para indicar que esta en proceso de ejecución
            //mientras su valor será verdadero

            //control de bloqueo de funcionabilidad
            if (IsBusy) return false;
            IsBusy = true;

            try
            {
                MyUser.Email = pEmail;
                MyUser.Password = pPassword;
                bool R = await MyUser.ValidateUserLogin();

                return R;




            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
                throw;
            }
            finally {
            IsBusy = false; 
            }
        }

        //bloque de codigo que carga la lista de roles que se usaran por ejemplo en el picker
        //de los roles en la parte de sign up

        public async Task<List<UserRole>> GetUserRolesAsync()
        {
            try
            {

                List<UserRole> roles = new List<UserRole>();

                roles = await MyUserRole.GetAllUserRolesAsync();


                if (roles == null)
                {
                    return null;
                }
                return roles;


            }
            catch (Exception)
            {

                throw;
            }





        }

        //funcion de creacion de usuario nuevo

        public async Task<bool> AddUserAsync(string pEmail,
                                            string pPassword, 
                                            string pName, 
                                            string pBackUpEmail,
                                            string pPhoneNumber,
                                            string pAddress,
                                            int pUserRoleID)
        {
            if (IsBusy) return false;
            IsBusy = true;
            {
                try
                {
                    MyUser = new User();

                    MyUser.Email = pEmail;
                    MyUser.Password = pPassword;
                    MyUser.Name = pName;
                    MyUser.BackUpEmail = pBackUpEmail;
                    MyUser.PhoneNumber = pPhoneNumber;
                    MyUser.Address = pAddress;
                    MyUser.UserRoleId = pUserRoleID;

                    bool r = await MyUser.AddUserAsync();

                    return r;

                }
                catch (Exception)
                {

                    throw;

                }
                finally { IsBusy = false; }

            }


        }













    }
}
