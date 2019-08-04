using System.ComponentModel;

namespace TMEnums
{
    /// <summary>
    /// Перечисления для таблицы Worker
    /// </summary>
    public class WorkerEnums
    {
        /// <summary>
        /// Пол сотрудника
        /// </summary>
        public enum Sex
        {
            /// <summary>
            /// Мужской
            /// </summary>
            [Description("Мужской")]
            Male,

            /// <summary>
            /// Женский
            /// </summary>
            [Description("Женский")]
            Female
        }

        /// <summary>
        /// Возвращает пол сотрудника на русском языке
        /// </summary>
        /// <param name="sex">Перечисление пола сотрудника</param>
        /// <returns></returns>
        public static string GetSexRUS(Sex sex)
        {
            var type = sex.GetType();
            var memberInfo = type.GetMember(sex.ToString());

            //Проверяем существует ли такой элемент в перечислении
            if (memberInfo == null || memberInfo.Length <= 0)
                return sex.ToString();

            var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            //Проверяем существует ли атрибут описания
            if (attributes == null || attributes.Length <= 0)
                return sex.ToString();

            return ((DescriptionAttribute)attributes[0]).Description;
        }
    }
}
