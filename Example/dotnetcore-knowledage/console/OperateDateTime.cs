using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.DotNetCoreKnowledage
{
    internal enum ExecuetePiplelineTimeType
    {
        FC = 1, // Fix cycle time.
        EC, // Every cycle time.
        AC // Apart cycle time.
    }
    [Serializable]
    internal class ExecuetePiplelineTime
    {
        private ExecuetePiplelineTimeType _model;
        private System.UInt32 _year;
        private System.UInt32 _month;
        private System.UInt32 _week;
        private System.UInt32 _day;
        private System.UInt32 _hour;
        private System.UInt32 _mintue;
        private System.UInt32 _second;

        public ExecuetePiplelineTime(ExecuetePiplelineTimeType type)
        {
            _model = type;
        }
        public ExecuetePiplelineTimeType Model { get { return _model; } }
        public System.UInt32 this[System.UInt32 index]
        {
            set
            {
                switch (index)
                {
                    case 0: _year = value;
                        break;
                    case 1: _month = value;
                        break;
                    case 2: _week = value;
                        break;
                    case 3: _day = value;
                        break;
                    case 4: _hour = value;
                        break;
                    case 5: _mintue = value;
                        break;
                    case 6: _second = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Execute pipeline time index");
                }
            }
        }
        public System.UInt32 Year
        {
            get { return _year; }
            set { _year = value;}
        }
        public System.UInt32 Month
        {
            get { return _month; }
            set
            {
                _month = value;
            }
        }
        public System.UInt32 Week
        {
            get { return _week; }
            set
            {
                _week = value;
            }
        }
        public System.UInt32 Day
        {
            get { return _day; }
            set
            {
                _day = value;
            }
        }
        public System.UInt32 Hour
        {
            get { return _hour; }
            set
            {
                _hour = value;
            }
        }
        public System.UInt32 Minute
        {
            get { return _mintue; }
            set
            {
                _mintue = value;
            }
        }
        public System.UInt32 Second
        {
            get { return _second; }
            set
            {
                _second = value;
            }
        }

    }
    class OperateDateTime
    {
        string fixCycleDt = "FC:Y0:M0:W0:D1:h8:m0:s0";
        string everyCycleDt = "EC:Y0:M0:W0:D1:h8:m0:s0";
        string apartCycleDt = "AC:Y0:M0:W0:D0:h0:m0:s30";
        static string cycleDt = "AC:Y0:M0:W0:D0:h0:m0:s30";
        private DateTime _executeTime;
        private DateTime _lastExecuteTime;
        private DateTime _nextExecuteTime;
        string dateFormat = "yyyy/MM/dd hh:mm:ss.fffffff";
        static string CompareDateFormat { get { return "yyyy:MM:dd:hh:mm:ss"; } }
        static ExecuetePiplelineTime exePipelineTime;

        static void Execute(string msg)
        {
            var dtArray = cycleDt.Split(':');
            if (dtArray[0].ToUpper() == ExecuetePiplelineTimeType.FC.ToString().ToUpper())
            {
                exePipelineTime = new ExecuetePiplelineTime(ExecuetePiplelineTimeType.FC);
            }
            if (dtArray[0].ToUpper() == ExecuetePiplelineTimeType.EC.ToString().ToUpper())
            {
                exePipelineTime = new ExecuetePiplelineTime(ExecuetePiplelineTimeType.EC);
            }
            if (dtArray[0].ToUpper() == ExecuetePiplelineTimeType.AC.ToString().ToUpper())
            {
                exePipelineTime = new ExecuetePiplelineTime(ExecuetePiplelineTimeType.AC);
            }
            for (System.UInt32 i = 1; i < dtArray.Length; i++)
            {
                exePipelineTime[i - 1] = Convert.ToUInt32(dtArray[i].Substring(1));
            }
        }
        static void Main_Stop()
        {
            //Format: prefix:Y:M:W:D:h:m:s
            Process.StartTag();
            DateTime dt = Convert.ToDateTime("2020/08/09 23:05:00");
            DateTime dt1 = DateTime.Now;
            //DateTime dt = DateTime.ParseExact("2020/08/08 23:05:00","yyyy/MM/dd hh:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            Console.WriteLine(dt < dt1);
            Process.EndTag();
        }
    }
}
