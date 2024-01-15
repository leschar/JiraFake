using System.ComponentModel;

namespace JiraFake.Domain.Utils
{
    public static class EnumUtils
    {
        public static string ObterDescricaoEnum(System.Enum valorEnum)
        {
            var tipoEnum = valorEnum.GetType();
            var membroEnum = tipoEnum.GetMember(valorEnum.ToString());

            if (membroEnum.Length > 0)
            {
                var atributo = membroEnum[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (atributo.Length > 0)
                {
                    return ((DescriptionAttribute)atributo[0]).Description;
                }
            }

            return valorEnum.ToString();
        }

    }
}
