using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TesGestionProyectos.CargarArchivos
{
    public class HerramientasLectorExcel
    {
        /// <summary>
        /// Método que permite comparar dos cadenas string y verificar si son iguales
        /// </summary>
        /// <param name="a">Cadena de string a comparar</param>
        /// <param name="b">Cadena de string a comparar</param>
        /// <returns>Retorna un "true" en caso que las cadenas sean iguales, de lo contrario retorna "false"</returns>
        public static bool Comparar(string a, string b)
        {
            return (string.Compare(a.ToLower().Trim(), b.ToLower().Trim(), CultureInfo.InvariantCulture, CompareOptions.IgnoreNonSpace) == 0);
        }

        /// <summary>
        /// Método que permite verificar si el valor ingresado es del tipo de datos adecuado
        /// </summary>
        /// <param name="valorOriginal">Valor ingresado en el Excel</param>
        /// <param name="tipo">Tipo de datos requerido</param>
        /// <returns>Retorna la variable error en "false" en caso que el valor ingresado sea del tipo requerido, en caso contrario retorna "true"</returns>
        public bool PuedeParserGenerico(ref object valorOriginal, Type tipo)
        {
            bool error = false;

            try
            {
                valorOriginal = Convert.ChangeType(valorOriginal, tipo);
            }
            catch (Exception)
            {
                error = true;
            }

            return error;
        }

        /// <summary>
        /// Método que permite verificar si el valor de la celda ingresado es null o vacio
        /// </summary>
        /// <param name="lista">Parámetros del valor ingresado en el Excel</param>
        /// <returns>Retorna "true" en caso que el valor ingresado sea null o vacio, en caso contrario retorna "false"</returns>
        public bool EsRowVacio(List<ItemEstructura> lista)
        {
            bool vacio = true;

            foreach (ItemEstructura item in lista)
            {
                if (item.Elemento is int || item.Elemento is float || (item.Elemento is string && (string)item.Elemento != string.Empty))
                {
                    return false;
                }
            }

            return vacio;
        }
    }
}
