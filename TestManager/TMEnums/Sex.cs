﻿using System.ComponentModel;

namespace TMEnums
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
        Male = 0,

        /// <summary>
        /// Женский
        /// </summary>
        [Description("Женский")]
        Female = 1
    }

    /// <summary>
    /// Локализация enum
    /// </summary>
    public class Localization
    {
        /// <summary>
        /// Возвращает пол сотрудника на русском языке
        /// </summary>
        /// <param name="sex">Перечисление пола сотрудника</param>
        /// <returns></returns>
        public static string GetSexResource(Sex sex)
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
