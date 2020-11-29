using BDS.Framework;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime.DataBase
{
    /// <summary>
    /// Provide value convert betwwen EF Core model and DataBase
    /// </summary>
    public class ValueTypeConverter
    {
        #region DateTime convert String
        public static ValueConverter<DateTime, string> DateTimeConvertString 
        { 
            get => new ValueConverter<DateTime, string> (
                v => v.ToString(GlobalVariables.ConvertDateTimeToStringFormat),
                v => DateTime.Parse(v)
            );
        }
        #endregion
        #region Enum convert String
        public static ValueConverter<WorkPipelineStatus, string> WorkPipelineStatusConvertString 
        { 
            get => new ValueConverter<WorkPipelineStatus, string> (
                v => v.ToString(),
                v => (WorkPipelineStatus)Enum.Parse(typeof(WorkPipelineStatus), v)
            );
        }

        public static ValueConverter<PipelineInvokeStatus, string> PipelineInvokeStatusConvertString
        {
            get => new ValueConverter<PipelineInvokeStatus, string> (
                v => v.ToString(),
                v => (PipelineInvokeStatus)Enum.Parse(typeof(PipelineInvokeStatus), v)
            );
        }

        public static ValueConverter EnumConvertString<T>() where T: Enum => new ValueConverter<T, string>(
                v => v.ToString(),
                v => (T)Enum.Parse(typeof(T), v)
            );
        #endregion

        #region StringBuilder convert String
        public static ValueConverter<StringBuilder, string> StringBuilderConvertString
        {
            get => new ValueConverter<StringBuilder, string>(
                    v => v.ToString(),
                    v => new StringBuilder(v)
            );
        }
        #endregion
    }
}
