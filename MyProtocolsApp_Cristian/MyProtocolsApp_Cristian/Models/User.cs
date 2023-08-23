using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace MyProtocolsApp_Cristian.Models
{
    public class User
    {
        //es mala idea tener un solo objeto de configuracion contra el API.
        //recomiendo tener uno por cada clase 
        public RestRequest Request { get; set; }




        //en este ejemplo usaré los mismos atributos que en el modelo del API.
        //Posteriormente en otra clase usaré el DTO del usuario para simplificar
        //el json que se envia y recibe desde el API
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string BackUpEmail { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Address { get; set; }
        public bool? Active { get; set; }
        public bool? IsBlocked { get; set; }
        public int UserRoleId { get; set; }

        public virtual UserRole? UserRole { get; set; } = null!;

        public User() { 
            Active = true;
            IsBlocked = false;
        
        }

        //Funciones especificas de llamada a end points del API

        //Funcion de validación del login
        public async Task<bool> ValidateUserLogin()
        {
            try
            {
                //usaremos el prefinjo de la ruta URL del API que se indica en
                //services\APIConnection para agregar el sufijo y lograr la ruta
                //completa de consumo en el end point

                string RouteSufix = string.Format("Users/ValidateLogin?username={0}&password={1}", this.Email, this.Password);

                //armamos la ruta completa al endpoint en el API
                string URL = Services.APIConnection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

               //mecanismo de seguridad en este caso API Key

                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);
                //ejecutar la llamada al API
                RestResponse response = await client.ExecuteAsync(Request);

                //saber si las cosas salieron bien

                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.OK)
                {
                    return true;
                }else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }

        }


        public async Task<bool> AddUserAsync()
        {
            try
            {
                

                string RouteSufix = string.Format("Users");

                //armamos la ruta completa al endpoint en el API
                string URL = Services.APIConnection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Post);

                //mecanismo de seguridad en este caso API Key

                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);

                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                //EN EL CASO DE UNA OPERACION POST DEBEMOS serializar el objeto para pasarlo
                //como Json al API

                string SerializedModelObject = JsonConvert.SerializeObject(this);
                //agregamos el objeto serializado en el cuerpo del request
                Request.AddBody(SerializedModelObject, GlobalObjects.MimeType);






                //ejecutar la llamada al API
                RestResponse response = await client.ExecuteAsync(Request);

                //saber si las cosas salieron bien

                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.Created)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }

        }














    }
}
