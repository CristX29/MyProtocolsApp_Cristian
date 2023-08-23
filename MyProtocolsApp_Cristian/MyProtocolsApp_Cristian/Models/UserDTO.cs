using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net;

namespace MyProtocolsApp_Cristian.Models
{
    public class UserDTO
    {

        [JsonIgnore]
        public RestRequest Request { get; set; }

        public int IDUsuario { get; set; }
        public string Correo { get; set; } = null!;
        public string Contrasennia { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string CorreoRespaldo { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string? Direccion { get; set; }
        public bool? Activo { get; set; }
        public bool? EstaBloqueado { get; set; }
        public int IDRol { get; set; }
        public string DescripcionRol { get; set; } = null!;

        //FUNCIOMES

        public async Task<UserDTO> GetUserInfo (string PEmail)
        {
            try
            {

                string RouteSufix = string.Format("Users/GetUserInfoByEmail?Pemail={0}", PEmail);

                //armamos la ruta completa al endpoint en el API
                string URL = Services.APIConnection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                //mecanismo de seguridad en este caso API Key

                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);
                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);
                //ejecutar la llamada al API
                RestResponse response = await client.ExecuteAsync(Request);

                //saber si las cosas salieron bien

                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.OK)
                {
                    //en el API diseñamos el end point de forma que retorne un list de tipo USERDTO
                    //pero esta funcion retorna solo un objeto de userDTO por lo tanto
                    //debemos seleccionar de la lista el ITEM con el index 0

                    //esto puede llegar a servir para muchos propositos donde un API retorna uno o muchos registros
                    //pero necesitamos solo uno de ellos

                    var list = JsonConvert.DeserializeObject<List<UserDTO>>(response.Content);

                    var item = list[0];
                    return item;
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
        }


        public async Task<bool> UpdateUserAsync()
        {
            try
            {


                string RouteSufix = string.Format("Users/{0}", this.IDUsuario);

                //armamos la ruta completa al endpoint en el API
                string URL = Services.APIConnection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Put);

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

                if (statusCode == HttpStatusCode.OK)
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
