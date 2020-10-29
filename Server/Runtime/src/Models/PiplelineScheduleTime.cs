using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime
{
    internal enum PiplelineScheduleApartTimeType
    {
        Y = 1, // Year.
        M, // Month.
        W, // Week.
        D, // Day
        HH, // Hour
        MM, //Minute
        SS, //Second
    }
    /// <summary>
    /// Pipeline execute schedule time
    /// </summary>
    [Serializable]
    internal class PiplelineScheduleTime
    {
        private string _startTime;
        private string _model;
        private int _apartTime;
        public string StartTime { get { return _startTime; } set { _startTime = value; } }
        public string Model { get { return _model; } set { _model = value; } }
        public int ApartTime { get { return _apartTime; } set { _apartTime = value; } }

        public static void SetScheduleTime(string scheduleString, out PiplelineScheduleTime scheduleTime, out DateTime lastExecuteTime, ref DateTime nextExecuteTime)
        {
            string[] scheduleArray = scheduleString.Split('|');
            if (scheduleArray.Length != 3)
            {
                throw new Exception("schedule format is incorrect");
            }
            scheduleTime = new PiplelineScheduleTime() { StartTime = scheduleArray[0], Model = scheduleArray[1], ApartTime = Convert.ToInt32(scheduleArray[2]) };
            bool parseResult = DateTime.TryParse(scheduleTime.StartTime, out nextExecuteTime);
            if (!parseResult)
            {
                throw new Exception("The Schedule start time format of schedule is incorrect");
            }
            lastExecuteTime = nextExecuteTime;
        }
        public static void SetNextExecuteTime(PiplelineScheduleTime scheduleTime, out DateTime lastExecuteTime, ref DateTime nextExecuteTime)
        {
            lastExecuteTime = nextExecuteTime;
            if (scheduleTime.Model.ToUpper() == PiplelineScheduleApartTimeType.Y.ToString())
            {
                nextExecuteTime = lastExecuteTime.AddMonths(12);
                return;
            }
            if (scheduleTime.Model.ToUpper() == PiplelineScheduleApartTimeType.M.ToString())
            {
                nextExecuteTime = lastExecuteTime.AddMonths(scheduleTime.ApartTime);
                return;
            }
            if (scheduleTime.Model.ToUpper() == PiplelineScheduleApartTimeType.W.ToString())
            {
                nextExecuteTime = lastExecuteTime.AddDays(7);
                return;
            }
            if (scheduleTime.Model.ToUpper() == PiplelineScheduleApartTimeType.D.ToString())
            {
                nextExecuteTime = lastExecuteTime.AddDays(scheduleTime.ApartTime);
                return;
            }
            if (scheduleTime.Model.ToUpper() == PiplelineScheduleApartTimeType.HH.ToString())
            {
                nextExecuteTime = lastExecuteTime.AddHours(scheduleTime.ApartTime);
                return;
            }
            if (scheduleTime.Model.ToUpper() == PiplelineScheduleApartTimeType.MM.ToString())
            {
                nextExecuteTime = lastExecuteTime.AddMinutes(scheduleTime.ApartTime);
                return;
            }
            if (scheduleTime.Model.ToUpper() == PiplelineScheduleApartTimeType.SS.ToString())
            {
                nextExecuteTime = lastExecuteTime.AddSeconds(scheduleTime.ApartTime);
                return;
            }
        }
    }
}
