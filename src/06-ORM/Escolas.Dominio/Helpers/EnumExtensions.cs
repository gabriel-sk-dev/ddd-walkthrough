namespace System
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Converter uma string para um enum especificado
        /// </summary>
        /// <typeparam name="T">Tipo do enum desejado para conversão</typeparam>
        /// <param name="valor">Valor a ser convertido para o enum</param>
        /// <returns>Enum conforme string informada</returns>
        public static T ToEnum<T>(this string valor) where T : struct, IConvertible
        {
            if (string.IsNullOrEmpty(valor))
                throw new InvalidOperationException($"O parâmentro valor(string) está vazio ou nulo");
            else if (!Enum.TryParse(valor, true, out T resultado))
                throw new InvalidOperationException($@"O valor ""{valor}"" não é reconhecido para enum ({resultado.GetType().Name})");
            else
                return resultado;
        }
    }
}
