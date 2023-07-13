using System;
using System.Collections.Generic;
using System.Text;

namespace MyProtocolsApp_Cristian.Services
{
    public static class APIConnection
    {
        //aca definimos la direccion URL (ya sea una IP o un nombre de dominio) a la que el app
        //debe apuntar. Por comodidad la ruta completa para consumir los recursos del API se hará
        //en formato "prefijo + sufijo"
        //donde el prefijo será la parte del URL que nunca cambiara y el sufijo la parte variable
        //(nombre del controlador y sus parametros)

        public static string ProductionPrefixURL = "http://192.168.0.2:45455/api/";
        public static string TestingPrefixURL = "http://192.168.0.2:45455/api/";

    

        public static string ApiKeyName = "Progra6ApiKey";

        public static string ApiKeyValue = "CristianProgra6cst29";













    }
}
